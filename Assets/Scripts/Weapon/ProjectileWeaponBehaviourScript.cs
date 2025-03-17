using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponBehaviour : WeaponBehaviourBase
{
    // [SerializeField] Vector3 direction; // 移動方向
    

    // 開啟攻擊
    protected override void OnAttackStart()
    {
        PlaySound();
    }
    
    // 彈丸移動
    override protected void OnUpdateStart() {
      transform.position += FlightDirection * FlightSpeed * Time.deltaTime;
    }

    // 執行攻擊
    override protected void ApplyAttack(Collider2D collision)
    {
        // 檢查對象
        if(!collision.CompareTag("Enemy")) return;

        // 判定可受傷
        if(collision.GetComponent<IDamageable>() is IDamageable e){
          e.TakeDamage(Attack); // 申請對象傷害
          
          ActiveStop(); // 強制結束持續攻擊
        }
    }

    // 結束攻擊
    protected override void OnAttackEnd()
    {
        Destroy(gameObject); // 彈丸自滅
    }
}
