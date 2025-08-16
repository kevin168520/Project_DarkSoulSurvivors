using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropComponent : MonoBehaviour
{
    public List<GameObject> dropItemPrefab; // 掉落物品
    [Range(0f, 1f)] public float chance; // 掉落率
    public float range = 0.5f; // 掉落範圍

    // 執行機率掉落物品
    public void HandleDropItem()
    {
        if (Random.value < chance)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(-range, range),
                0,
                Random.Range(-range, range)
            );
            GameObject toDrop = dropItemPrefab[Random.Range(0, dropItemPrefab.Count)];
            Transform t = Instantiate(toDrop, transform.position + randomPos, Quaternion.identity).transform;
            t.position = transform.position;
        }
    }
}
