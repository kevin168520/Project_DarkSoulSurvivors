using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

public class MapManager : ManagerMonoBase
{
    [Header("地圖設定")]  
    public GameObject[] terrainPrefabs;       // 地塊 Prefab 陣列
    public Transform player;                  // 玩家 Transform
    public int tileWidth = 10;                // 地塊寬度
    public int tileHeight = 22;               // 地塊高度
    public int preloadRange = 2;              // 預加載範圍
    public float updateInterval = 0.2f;       // 更新間隔（秒）

    [Header("物品管理")]  
    public ItemManager itemManager;           // 物品管理器

    [Header("關卡設定")]  
    public LevelType levelType = LevelType.FourDirection;  // 生成方式

    [Header("無限地圖設定")]
    public bool enableInfiniteMap = true;     // 啟用無限地圖
    public int maxActiveTiles = 100;          // 最大活躍地塊數量
    public bool useRandomGeneration = true;   // 使用隨機生成

    // 內部類別：儲存地塊物件及其對應的 Prefab 索引
    private class TileInfo
    {
        public GameObject tile;
        public int prefabIndex;
    }

    // 活躍地塊與物件池、物品記錄
    private Dictionary<Vector2Int, TileInfo> activeTiles = new Dictionary<Vector2Int, TileInfo>();
    private Dictionary<int, Queue<GameObject>> tilePools = new Dictionary<int, Queue<GameObject>>();
    private Dictionary<Vector2Int, List<GameObject>> tileItems = new Dictionary<Vector2Int, List<GameObject>>();
    private Vector2Int playerGrid;            // 玩家當前格子
    private bool isInitialized = false;       // 初始化狀態
    private Coroutine updateCoroutine;        // 更新協程

    public enum LevelType { FourDirection, Horizontal }

    protected override void Awake()
    {
        base.Awake();
        // 初始化每個 Prefab 的物件池
        InitializeTilePools();
    }

    private void InitializeTilePools()
    {
        if (terrainPrefabs == null || terrainPrefabs.Length == 0)
        {
            Debug.LogError("MapManager: terrainPrefabs 陣列為空！請在Inspector中設定地塊Prefab。");
            return;
        }

        for (int i = 0; i < terrainPrefabs.Length; i++)
        {
            if (terrainPrefabs[i] == null)
            {
                Debug.LogError($"MapManager: terrainPrefabs[{i}] 為null！請檢查Prefab設定。");
                continue;
            }
            tilePools[i] = new Queue<GameObject>();
        }
    }

    private void Start()
    {
        StartCoroutine(InitializeMapSystem());
    }    private IEnumerator InitializeMapSystem()
    {
        // 等待一幀確保其他系統初始化完成
        yield return null;

        // 取得玩家 Transform
        if (!TryGetPlayerTransform())
        {
            // 如果無法立即取得玩家，持續嘗試
            yield return StartCoroutine(WaitForPlayer());
        }

        // 驗證必要組件
        if (!ValidateComponents())
        {
            yield break;
        }

        // 初始生成地圖
        playerGrid = GetPlayerGrid();
        UpdateTiles(playerGrid);
        
        isInitialized = true;
        
        // 啟動定時更新協程
        if (enableInfiniteMap)
        {
            updateCoroutine = StartCoroutine(UpdateMapCoroutine());
        }
    }

    private bool TryGetPlayerTransform()
    {
        if (player != null) return true;

        // 嘗試從GameManager獲取
        if (GameManager != null && PlayerManager.Player != null)
        {
            player = PlayerManager.Player.transform;
            return player != null;
        }

        // 嘗試通過Tag查找
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            return true;
        }

        return false;
    }

    private IEnumerator WaitForPlayer()
    {
        float timeout = 10f; // 10秒超時
        float timer = 0f;

        while (!TryGetPlayerTransform() && timer < timeout)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        if (player == null)
        {
            Debug.LogError("MapManager: 無法找到玩家物件！請確認玩家物件存在且有正確的Tag。");
        }
    }

    private bool ValidateComponents()
    {
        if (terrainPrefabs == null || terrainPrefabs.Length == 0)
        {
            Debug.LogError("MapManager: 沒有設定地塊Prefab！");
            return false;
        }

        if (player == null)
        {
            Debug.LogError("MapManager: 玩家Transform為null！");
            return false;
        }

        return true;
    }

    private IEnumerator UpdateMapCoroutine()
    {
        while (isInitialized && player != null)
        {
            Vector2Int newGrid = GetPlayerGrid();
            if (newGrid != playerGrid)
            {
                playerGrid = newGrid;
                UpdateTiles(playerGrid);
            }
            
            yield return new WaitForSeconds(updateInterval);
        }
    }

    private void Update()
    {
        // 如果沒有啟用無限地圖，使用傳統更新方式
        if (!enableInfiniteMap && isInitialized && player != null)
        {
            Vector2Int newGrid = GetPlayerGrid();
            if (newGrid != playerGrid)
            {
                playerGrid = newGrid;
                UpdateTiles(playerGrid);
            }
        }
    }

    // 計算玩家目前的格子座標
    private Vector2Int GetPlayerGrid()
    {
        if (player == null) return Vector2Int.zero;
        
        return new Vector2Int(
            Mathf.FloorToInt(player.position.x / tileWidth),
            Mathf.FloorToInt(player.position.y / tileHeight)
        );
    }

    // 更新地圖：生成新地塊並回收超出範圍的地塊
    private void UpdateTiles(Vector2Int centerGrid)
    {
        // 生成範圍內的新地塊
        for (int x = -preloadRange; x <= preloadRange; x++)
        {
            for (int y = -preloadRange; y <= preloadRange; y++)
            {
                if (levelType == LevelType.Horizontal && y != 0) continue;
                Vector2Int gridPos = centerGrid + new Vector2Int(x, y);
                if (!activeTiles.ContainsKey(gridPos))
                {
                    AddTile(gridPos);
                    SpawnItems(gridPos);
                }
            }
        }

        // 回收超出範圍的地塊與物品
        if (enableInfiniteMap)
        {
            CleanupDistantTiles(centerGrid);
        }
    }

    private void CleanupDistantTiles(Vector2Int centerGrid)
    {
        var removeList = new List<Vector2Int>();
        
        foreach (var kv in activeTiles)
        {
            Vector2Int pos = kv.Key;
            float distance = Vector2Int.Distance(pos, centerGrid);
            
            if (distance > preloadRange + 1 || activeTiles.Count > maxActiveTiles)
            {
                removeList.Add(pos);
            }
        }
        
        // 按距離排序，優先移除最遠的地塊
        removeList.Sort((a, b) => 
            Vector2Int.Distance(b, centerGrid).CompareTo(Vector2Int.Distance(a, centerGrid)));
        
        foreach (var pos in removeList)
        {
            RemoveTile(pos);
            RemoveItems(pos);
            
            // 如果達到目標數量就停止
            if (activeTiles.Count <= maxActiveTiles) break;
        }
    }

    // 生成或重用地塊
    private void AddTile(Vector2Int gridPos)
    {
        if (terrainPrefabs == null || terrainPrefabs.Length == 0) return;

        int prefabIndex = GetTileIndex(gridPos);
        GameObject tileObj;
        
        if (tilePools.ContainsKey(prefabIndex) && tilePools[prefabIndex].Count > 0)
        {
            tileObj = tilePools[prefabIndex].Dequeue();
            tileObj.SetActive(true);
        }
        else
        {
            if (terrainPrefabs[prefabIndex] == null)
            {
                Debug.LogError($"MapManager: terrainPrefabs[{prefabIndex}] 為null！");
                return;
            }
            tileObj = Instantiate(terrainPrefabs[prefabIndex], transform);
        }
        
        tileObj.transform.position = new Vector3(gridPos.x * tileWidth, gridPos.y * tileHeight, 0);
        activeTiles[gridPos] = new TileInfo { tile = tileObj, prefabIndex = prefabIndex };
    }

    private int GetTileIndex(Vector2Int gridPos)
    {
        if (!useRandomGeneration)
        {
            return Random.Range(0, terrainPrefabs.Length);
        }
        
        // 使用位置基礎的偽隨機生成，確保相同位置總是生成相同地塊
        int seed = gridPos.x * 73856093 + gridPos.y * 19349663;
        System.Random random = new System.Random(seed);
        return random.Next(0, terrainPrefabs.Length);
    }

    // 回收地塊至物件池
    private void RemoveTile(Vector2Int gridPos)
    {
        if (activeTiles.TryGetValue(gridPos, out TileInfo info))
        {
            if (info.tile != null)
            {
                info.tile.SetActive(false);
                if (tilePools.ContainsKey(info.prefabIndex))
                {
                    tilePools[info.prefabIndex].Enqueue(info.tile);
                }
            }
            activeTiles.Remove(gridPos);
        }
    }

    // 生成地塊上的物品
    private void SpawnItems(Vector2Int gridPos)
    {
        if (itemManager == null) return;
        
        try
        {
            Vector3 centerPos = new Vector3(gridPos.x * tileWidth, gridPos.y * tileHeight, 0);
            GameObject item = itemManager.CreateItem(centerPos);
            if (item != null)
            {
                if (!tileItems.ContainsKey(gridPos))
                    tileItems[gridPos] = new List<GameObject>();
                tileItems[gridPos].Add(item);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning($"MapManager: 生成物品時發生錯誤: {e.Message}");
        }
    }

    // 移除地塊上的物品
    private void RemoveItems(Vector2Int gridPos)
    {
        if (tileItems.TryGetValue(gridPos, out var items))
        {
            foreach (var itemObj in items)
                if (itemObj != null) Destroy(itemObj);
            tileItems.Remove(gridPos);
        }
    }

    protected override void OnDestroy()
    {
        if (updateCoroutine != null)
        {
            StopCoroutine(updateCoroutine);
        }
        base.OnDestroy();
    }

    // 公共方法：手動強制更新地圖
    public void ForceUpdateMap()
    {
        if (isInitialized && player != null)
        {
            playerGrid = GetPlayerGrid();
            UpdateTiles(playerGrid);
        }
    }

    // 公共方法：清理所有地塊
    public void ClearAllTiles()
    {
        var allPositions = new List<Vector2Int>(activeTiles.Keys);
        foreach (var pos in allPositions)
        {
            RemoveTile(pos);
            RemoveItems(pos);
        }
    }
}
