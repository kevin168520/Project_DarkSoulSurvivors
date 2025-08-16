using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropComponent : MonoBehaviour
{
    public List<GameObject> dropItemPrefab; // 掉落物品
    [Range(0f, 1f)] public float chance; // 掉落率

    // 執行機率掉落物品
    public void HandleDropItem()
    {
        if (Random.value < chance)
        {
            GameObject toDrop = dropItemPrefab[Random.Range(0, dropItemPrefab.Count)];
            Transform t = Instantiate(toDrop).transform;
            t.position = transform.position;
        }
    }
}
