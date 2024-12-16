using UnityEngine;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
    [Header("地圖設定")]
    public GameObject[] terrainPrefabs; // 地塊 Prefab 陣列
    public Transform player;            // 玩家位置
    public int tileWidth = 10;          // 單個地塊寬度
    public int tileHeight = 22;         // 單個地塊高度
    public int preloadRange = 2;        // 預加載範圍

    [Header("物品管理器")]
    public ItemManager itemManager;     // 物品管理器

    private Dictionary<Vector2Int, GameObject> activeTiles = new Dictionary<Vector2Int, GameObject>(); // 地塊記錄
    private Dictionary<Vector2Int, List<GameObject>> tileItems = new Dictionary<Vector2Int, List<GameObject>>(); // 每個地塊上的物品記錄
    private Vector2Int playerGrid;      // 玩家所在地塊的座標

    void Start()
    {
        playerGrid = GetPlayerGrid();
        UpdateTiles(playerGrid);
    }

    void Update()
    {
        Vector2Int newPlayerGrid = GetPlayerGrid();
        if (newPlayerGrid != playerGrid)
        {
            playerGrid = newPlayerGrid;
            UpdateTiles(playerGrid);
        }
    }

    // 獲取玩家所在地塊座標
    private Vector2Int GetPlayerGrid()
    {
        return new Vector2Int(
            Mathf.FloorToInt(player.position.x / tileWidth),
            Mathf.FloorToInt(player.position.y / tileHeight)
        );
    }

    // 更新地圖：管理地塊及物品
    private void UpdateTiles(Vector2Int centerGrid)
    {
        for (int x = -preloadRange; x <= preloadRange; x++)
        {
            for (int y = -preloadRange; y <= preloadRange; y++)
            {
                Vector2Int gridPosition = centerGrid + new Vector2Int(x, y);

                if (!activeTiles.ContainsKey(gridPosition))
                {
                    AddTile(gridPosition);
                    SpawnItems(gridPosition); // 生成物品
                }
            }
        }

        // 刪除超出範圍的地塊及物品
        List<Vector2Int> tilesToRemove = new List<Vector2Int>();
        foreach (var tile in activeTiles.Keys)
        {
            if (Mathf.Abs(tile.x - centerGrid.x) > preloadRange || Mathf.Abs(tile.y - centerGrid.y) > preloadRange)
            {
                tilesToRemove.Add(tile);
            }
        }

        foreach (var tile in tilesToRemove)
        {
            RemoveTile(tile);
            RemoveItems(tile); // 同時移除該地塊上的物品
        }
    }

    // 生成地塊
    private void AddTile(Vector2Int gridPosition)
    {
        Vector3 worldPosition = new Vector3(gridPosition.x * tileWidth, gridPosition.y * tileHeight, 0);

        GameObject newTile = Instantiate(
            terrainPrefabs[Random.Range(0, terrainPrefabs.Length)],
            worldPosition,
            Quaternion.identity,
            transform
        );

        activeTiles[gridPosition] = newTile;
    }

    // 在地塊上生成物品並記錄
    private void SpawnItems(Vector2Int gridPosition)
    {
        if (itemManager != null)
        {
            Vector3 tileCenterPosition = new Vector3(gridPosition.x * tileWidth, gridPosition.y * tileHeight, 0);
            GameObject item = itemManager.CreateItem(tileCenterPosition);

            if (item != null)
            {
                if (!tileItems.ContainsKey(gridPosition))
                {
                    tileItems[gridPosition] = new List<GameObject>();
                }
                tileItems[gridPosition].Add(item); // 記錄生成的物品
            }
        }
    }

    // 移除地塊
    private void RemoveTile(Vector2Int gridPosition)
    {
        if (activeTiles.TryGetValue(gridPosition, out GameObject tile))
        {
            Destroy(tile);
            activeTiles.Remove(gridPosition);
        }
    }

    // 移除該地塊上的物品
    private void RemoveItems(Vector2Int gridPosition)
    {
        if (tileItems.TryGetValue(gridPosition, out List<GameObject> items))
        {
            foreach (var item in items)
            {
                if (item != null)
                {
                    Destroy(item);
                }
            }
            tileItems.Remove(gridPosition);
        }
    }
}
