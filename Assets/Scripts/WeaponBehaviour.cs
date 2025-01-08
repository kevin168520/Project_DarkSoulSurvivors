using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : AttackBehaviourBase
{
    [SerializeField] string targetTag; // 目標
    [SerializeField] int damage = 10; // 攻擊傷害
    List<Object> enemiesHit = new List<Object>(); // 避免相同敵人多次攻擊

    // 開啟攻擊
    new public void OnEnable() {
      base.OnEnable();
      enemiesHit.Clear();
    }

    // 執行攻擊
    protected override void ApplyDamage(Collider2D collision)
    {
        // 判定對方 tag
        if(collision.tag != targetTag) return;

        // 判定未被攻擊過
        if(enemiesHit.Contains(collision)) return;

        // 判定可受傷
        IDamageable e = collision.GetComponent<IDamageable>();
        if(e != null) return;
        
        e.TakeDamage(damage); // 申請對象傷害

        enemiesHit.Add(collision); // 加入曾被攻擊過
    }

    public void SetWeaponData(WeaponScriptable weaponData) {
      damage = weaponData.damage;
    }
}
