using UnityEngine;
using System.Collections.Generic;


public class ItemManager : ManagerMonoBase
{
    public enum SpawnMode { ByMapManager, Fixed2D }

    [Header("生成模式")]
    public SpawnMode spawnMode = SpawnMode.ByMapManager;

    [Header("所有道具 Prefabs (含回血寶箱)")]
    public GameObject[] itemPrefabs; // 一般道具＋回血寶箱都放這裡

    
    [Header("ByMapManager 模式設定")]
    [Range(0f, 1f)]
    public float spawnProbability = 0.5f;              // 每個地塊是否生成的機率
    public Vector2 spawnAreaSize = new Vector2(5f, 5f);// 地塊內的隨機偏移

    
    [Header("Fixed2D 模式 - 基本")]
    public Transform player;                           // 玩家 Transform（自動抓 Tag=Player）
    public int maxActiveChests = 5;                    // 場上最多寶箱
    public float chestSpawnRadius = 8f;                // 最大生成距離（相對玩家）
    public float minSpawnDistance = 3f;                // 最小生成距離（避免在腳邊）
    public bool spawnOnlyHealthChest = true;           // 僅生成含 HealthChest 的 Prefab

    [Header("Fixed2D 模式 - 邊界牆（四面）")]
    public Collider2D boundaryTop;                     // Boundary_TOP
    public Collider2D boundaryDown;                    // Boundary_Down
    public Collider2D boundaryLeft;                    // Boundary_Left
    public Collider2D boundaryRight;                   // Boundary_Right
    [Tooltip("生成點與牆保持的安全距離（以世界單位內縮）")]
    public float boundaryPadding = 0.5f;

    [Header("自動尋找邊界牆（依名稱）")]
    public bool autoFindBoundariesByName = true;
    public string nameTop = "Boundary_TOP";
    public string nameDown = "Boundary_Down";
    public string nameLeft = "Boundary_Left";
    public string nameRight = "Boundary_Right";

    [Header("Fixed2D 模式 - 避開攝影機可見區域")]
    public bool avoidCameraView = true;                // 是否避開鏡頭可見矩形
    public Camera targetCamera;                        // ★ 自動抓：Camera.main -> 啟用中的任何相機 -> 場景中第一個
    [Tooltip("在鏡頭可見矩形外圍再加的邊距（世界單位），避免剛好貼邊生成")]
    public float cameraPadding = 0.5f;

    // 內部：追蹤/計算
    private readonly List<GameObject> activeChests = new List<GameObject>();

    // 用四面牆 bounds 計算出的可生成矩形
    private Rect _playRect;             // xMin~xMax, yMin~yMax
    private bool _hasPlayRect = false;  // 是否成功取得矩形

    // --------------------------------------------------
    // Unity
    // --------------------------------------------------
    private void Start()
    {
        if (spawnMode == SpawnMode.Fixed2D)
        {
            AutoAssignPlayerAndCamera();

            // 自動依名稱尋找四面牆（可關）
            if (autoFindBoundariesByName)
            {
                TryAutoFindBoundary(ref boundaryTop,   nameTop);
                TryAutoFindBoundary(ref boundaryDown,  nameDown);
                TryAutoFindBoundary(ref boundaryLeft,  nameLeft);
                TryAutoFindBoundary(ref boundaryRight, nameRight);
            }

            // 用四面牆建立可生成矩形
            RebuildPlayRect();

            // 開場補滿
            CleanupNullChests();
            while (activeChests.Count < maxActiveChests)
                SpawnChestNearPlayer();
        }
    }

    // MapManager 會呼叫的接口（維持相容）
    public GameObject CreateItem(Vector3 tileCenterPosition)
    {
        if (spawnMode == SpawnMode.ByMapManager)
        {
            if (itemPrefabs == null || itemPrefabs.Length == 0) return null;
            if (Random.value > spawnProbability) return null;

            Vector3 offset = new Vector3(
                Random.Range(-spawnAreaSize.x * 0.5f, spawnAreaSize.x * 0.5f),
                Random.Range(-spawnAreaSize.y * 0.5f, spawnAreaSize.y * 0.5f),
                0f
            );

            GameObject prefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
            return Instantiate(prefab, tileCenterPosition + offset, Quaternion.identity);
        }

        // Fixed2D 模式不走 MapManager 呼叫
        return null;
    }

    // --------------------------------------------------
    // Fixed2D：生成邏輯
    // --------------------------------------------------

    /// <summary>
    /// 在玩家附近生成一個寶箱（距離限制 + 四面牆矩形 + 可選避開鏡頭）
    /// </summary>
    private void SpawnChestNearPlayer()
    {
        if (player == null || itemPrefabs == null || itemPrefabs.Length == 0) return;

        // 只取 HealthChest 的 Prefab（若找不到就退回第0個）
        GameObject prefab;
        if (spawnOnlyHealthChest)
        {
            prefab = System.Array.Find(itemPrefabs, p => p != null && p.GetComponent<HealthChest>() != null);
            if (prefab == null) prefab = itemPrefabs[0];
        }
        else
        {
            prefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
        }

        Vector2 spawnPos = Vector2.zero;
        bool found = false;

        // 嘗試找符合條件的位置
        const int MAX_TRY = 30;
        for (int i = 0; i < MAX_TRY; i++)
        {
            // 在 min~max 半徑間取隨機方向與距離
            Vector2 dir = Random.insideUnitCircle.normalized;
            float dist = Random.Range(minSpawnDistance, chestSpawnRadius);
            Vector2 candidate = (Vector2)player.position + dir * dist;

            // 若有可生成矩形，必須落在矩形內
            if (_hasPlayRect && !PointInsideRect(candidate, _playRect))
                continue;

            // 距離玩家安全（保險檢查）
            if (Vector2.Distance(candidate, player.position) < minSpawnDistance)
                continue;

            // 避開鏡頭可見矩形（可關閉）
            if (avoidCameraView && IsPointInsideCameraRect(candidate, cameraPadding))
                continue;

            spawnPos = candidate;
            found = true;
            break;
        }

        // 退路：嘗試 8 方向投影到矩形內邊緣，並避開鏡頭；最後一次失敗就放棄
        if (!found && _hasPlayRect)
        {
            Vector2[] dirs =
            {
                Vector2.right, Vector2.left, Vector2.up, Vector2.down,
                new Vector2(1,1).normalized, new Vector2(-1,1).normalized,
                new Vector2(1,-1).normalized, new Vector2(-1,-1).normalized
            };

            foreach (var d in dirs)
            {
                Vector2 projected = (Vector2)player.position + d * chestSpawnRadius;
                projected = ClampToRect(projected, _playRect); // 壓進矩形內

                if (Vector2.Distance(projected, player.position) < (minSpawnDistance - 0.01f))
                    continue;

                if (avoidCameraView && IsPointInsideCameraRect(projected, cameraPadding))
                    continue;

                spawnPos = projected;
                found = true;
                break;
            }
        }

        if (!found)
        {
            // 鏡頭把可玩區域幾乎塞滿時，可能找不到合格點；這次就不生成。
            return;
        }

        GameObject chest = Instantiate(prefab, spawnPos, Quaternion.identity);

        // 訂閱撿取事件（補位）
        var hc = chest.GetComponent<HealthChest>();
        hc.PickedUp += OnChestPickedUp;

        activeChests.Add(chest);
    }

    
    private void OnChestPickedUp()
    {
        CleanupNullChests();

        if (activeChests.Count < maxActiveChests)
            SpawnChestNearPlayer();
    }

    // --------------------------------------------------
    // 邊界牆 / 矩形 / 攝影機工具
    // --------------------------------------------------

    /// <summary>
    /// 自動抓取 Player 與 Camera（若 Inspector 未指定）
    /// </summary>
    private void AutoAssignPlayerAndCamera()
    {
        // 玩家：先用 Inspector，否則抓 Tag=Player
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        // 相機：Inspector -> Camera.main -> 任何啟用中的相機 -> 場景第一個（含未啟用）
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
            if (targetCamera == null)
            {
                // 啟用中的任何相機
                if (Camera.allCamerasCount > 0)
                {
                    var cams = new Camera[Camera.allCamerasCount];
                    Camera.GetAllCameras(cams);
                    if (cams.Length > 0) targetCamera = cams[0];
                }

                // 仍然沒有就找場景中的第一個（包含未啟用）
                if (targetCamera == null)
                {
                    var any = Object.FindObjectOfType<Camera>(true);
                    if (any != null) targetCamera = any;
                }
            }
        }
    }

    /// <summary>
    /// 依目前四面牆的 Collider2D 重新計算可生成矩形
    /// </summary>
    public void RebuildPlayRect()
    {
        if (boundaryTop && boundaryDown && boundaryLeft && boundaryRight)
        {
            // 取牆內緣，加上 padding 內縮
            float minX = boundaryLeft.bounds.max.x + boundaryPadding;
            float maxX = boundaryRight.bounds.min.x - boundaryPadding;
            float minY = boundaryDown.bounds.max.y + boundaryPadding;
            float maxY = boundaryTop.bounds.min.y - boundaryPadding;

            // 避免極端情況導致寬高負值
            float w = Mathf.Max(0.01f, maxX - minX);
            float h = Mathf.Max(0.01f, maxY - minY);

            _playRect = new Rect(minX, minY, w, h);
            _hasPlayRect = true;
        }
        else
        {
            _hasPlayRect = false; // 未完整指定四面牆時，僅用距離規則
        }
    }

    private static bool PointInsideRect(Vector2 p, Rect r)
        => p.x >= r.xMin && p.x <= r.xMax && p.y >= r.yMin && p.y <= r.yMax;

    private static Vector2 ClampToRect(Vector2 p, Rect r)
        => new Vector2(Mathf.Clamp(p.x, r.xMin, r.xMax), Mathf.Clamp(p.y, r.yMin, r.yMax));

    private void TryAutoFindBoundary(ref Collider2D col, string objName)
    {
        if (col != null) return;
        var go = GameObject.Find(objName);
        if (go != null) col = go.GetComponent<Collider2D>();
    }

    private void CleanupNullChests()
    {
        for (int i = activeChests.Count - 1; i >= 0; i--)
            if (activeChests[i] == null) activeChests.RemoveAt(i);
    }

    /// <summary>
    /// 取得鏡頭的世界矩形（視口 0,0 到 1,1），外擴 padding
    /// </summary>
    private Rect GetCameraWorldRect(float padding)
    {
        var cam = targetCamera != null ? targetCamera : Camera.main;
        if (cam == null)
        {
            // 萬一在執行中相機被替換，嘗試再自動抓一次
            AutoAssignPlayerAndCamera();
            cam = targetCamera;
            if (cam == null) return new Rect(0, 0, 0, 0);
        }

        // 以 ViewportToWorldPoint 取可見區域四角
        Vector3 p0 = cam.ViewportToWorldPoint(new Vector3(0f, 0f, cam.nearClipPlane));
        Vector3 p1 = cam.ViewportToWorldPoint(new Vector3(1f, 1f, cam.nearClipPlane));

        float xMin = Mathf.Min(p0.x, p1.x) - padding;
        float xMax = Mathf.Max(p0.x, p1.x) + padding;
        float yMin = Mathf.Min(p0.y, p1.y) - padding;
        float yMax = Mathf.Max(p0.y, p1.y) + padding;

        return new Rect(xMin, yMin, xMax - xMin, yMax - yMin);
    }

    private bool IsPointInsideCameraRect(Vector2 worldPoint, float padding)
    {
        if (!avoidCameraView) return false; // 不檢查
        Rect camRect = GetCameraWorldRect(padding);
        return PointInsideRect(worldPoint, camRect);
    }
}
