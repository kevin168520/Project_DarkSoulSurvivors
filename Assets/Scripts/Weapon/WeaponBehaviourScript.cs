using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : WeaponBehaviourBase
{
    HashSet<Object> enemiesHit = new HashSet<Object>(); // 避免相同敵人多次攻擊 HashSet 無排序速度較快

    // 開啟攻擊
    protected override void OnAttackStart()
    {
        enemiesHit.Clear();
        PlaySound();
    }

    // 執行攻擊
    protected override void HandleAttack(Collider2D collision)
    {
        if(OnAttackEvent(collision)){
          enemiesHit.Add(collision); // 加入曾被攻擊過
        }
    }

    // 結束攻擊
    protected override void OnAttackEnd()
    {
        gameObject.SetActive(false);
    }
}
