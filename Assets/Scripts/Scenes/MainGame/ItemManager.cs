using UnityEngine;

/// <summary>
/// 道具管理器：負責按照統一的機率，在指定地塊隨機生成所有道具（包含回血寶箱）
/// </summary>
public class ItemManager : ManagerMonoBase
{
    [Header("所有道具 Prefabs (含回血寶箱)")]
    public GameObject[] itemPrefabs;         // 把一般道具＋回血寶箱 Prefab 全部放在這裡

    [Header("道具生成機率")]
    [Range(0f, 1f)]
    public float spawnProbability = 0.5f;    // 總生成機率

    [Header("物品生成範圍")]
    public Vector2 spawnAreaSize = new Vector2(5f, 5f);

    /// <summary>
    /// 嘗試生成道具 (若隨機機率未通過則回傳 null)
    /// </summary>
    /// <param name="tileCenterPosition">地塊中心點世界座標</param>
    /// <returns>生成的物件或 null</returns>
    public GameObject CreateItem(Vector3 tileCenterPosition)
    {
        // 1. 機率檢查
        if (itemPrefabs == null || itemPrefabs.Length == 0) return null;
        if (Random.value > spawnProbability) return null;

        // 2. 隨機偏移
        Vector3 offset = new Vector3(
            Random.Range(-spawnAreaSize.x * 0.5f, spawnAreaSize.x * 0.5f),
            Random.Range(-spawnAreaSize.y * 0.5f, spawnAreaSize.y * 0.5f),
            0f
        );

        // 3. 隨機選一個 Prefab 實例化
        GameObject prefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
        GameObject spawned = Instantiate(prefab, tileCenterPosition + offset, Quaternion.identity);

       
        return spawned;
    }
}
