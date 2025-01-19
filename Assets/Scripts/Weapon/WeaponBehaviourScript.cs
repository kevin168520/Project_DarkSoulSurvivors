using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : WeaponBehaviourBase
{
    List<Object> enemiesHit = new List<Object>(); // 避免相同敵人多次攻擊

    // 開啟攻擊
    override public void OnEnable() {
      base.OnEnable();
      enemiesHit.Clear();
    }

    // 執行攻擊
    protected override void ApplyDamage(Collider2D collision)
    {
        // 判定未被攻擊過
        if(enemiesHit.Contains(collision)) return;

        // 判定可受傷
        if(collision.GetComponent<IDamageable>() is IDamageable e){
          e.TakeDamage(damage); // 申請對象傷害

          enemiesHit.Add(collision); // 加入曾被攻擊過
        }
    }
}
