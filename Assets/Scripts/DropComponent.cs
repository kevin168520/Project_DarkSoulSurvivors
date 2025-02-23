using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropComponent : MonoBehaviour
{
  public List<GameObject> dropItemPrefab; // 掉落物品
  [Range(0f, 1f)] public float chance; // 掉落率
  private bool isQuitting;

  // 被移除時掉落 未來優化時修改
  void OnDestroy() {
    CheckDrop();
  }

  // 關閉遊戲 防止已結束遊戲物件破壞仍生成物件
  void OnApplicationQuit() {
    isQuitting = true;
  }

  // 判定是否掉落物品
  void CheckDrop() {
    if(isQuitting) return;
    if(Random.value < chance)
    {
      GameObject toDrop = dropItemPrefab[Random.Range(0, dropItemPrefab.Count)];
      Transform t = Instantiate(toDrop).transform;
      t.position = transform.position;
    }
  }
}
