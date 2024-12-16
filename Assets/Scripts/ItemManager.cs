using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [Header("物品 Prefabs")]
    public GameObject[] itemPrefabs; // 可生成的物品 Prefab 陣列

    [Header("生成機率設定")]
    [Range(0f, 1f)]
    public float spawnProbability = 0.5f; // 生成機率 (0.0 ~ 1.0，預設 50%)

    [Header("物品生成範圍")]
    public Vector2 spawnAreaSize = new Vector2(5f, 5f); // 物品生成範圍大小

    /// <summary>
    /// 嘗試在指定地塊中心位置生成物品
    /// </summary>
    /// <param name="tileCenterPosition">地塊的中心位置</param>
    /// <returns>生成的物品物件，若未生成則返回 null</returns>
    public GameObject CreateItem(Vector3 tileCenterPosition)
    {
        // 根據機率判斷是否生成物品
        if (Random.value > spawnProbability)
        {
            return null; // 不生成物品
        }

        // 確保物品 Prefabs 有設定
        if (itemPrefabs == null || itemPrefabs.Length == 0)
        {
            Debug.LogWarning("ItemManager: 沒有設定物品 Prefabs！");
            return null;
        }

        // 計算隨機生成位置（在地塊範圍內）
        Vector3 randomOffset = new Vector3(
            Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f),
            Random.Range(-spawnAreaSize.y / 2f, spawnAreaSize.y / 2f),
            0
        );

        Vector3 spawnPosition = tileCenterPosition + randomOffset;

        // 隨機選擇一個物品 Prefab
        GameObject itemPrefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];

        // 實例化物品
        GameObject spawnedItem = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
        return spawnedItem;
    }
}
