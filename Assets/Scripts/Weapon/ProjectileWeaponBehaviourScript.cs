using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponBehaviour : WeaponBehaviourBase
{
    // [SerializeField] Vector3 direction; // 移動方向
    

    // 結束攻擊 彈丸自滅
    override public void OnEnable() {
        base.OnEnable();
        PlaySound();
    }

    // 彈丸移動
    override protected void BeforeUpdate() {
      transform.position += FlightDirection * FlightSpeed * Time.deltaTime;
    }

    // 結束攻擊 彈丸自滅
    void OnDisable() {
      Destroy(gameObject);
    }

    // 執行攻擊
    override protected void ApplyDamage(Collider2D collision)
    {
        // 判定可受傷
        if(collision.GetComponent<IDamageable>() is IDamageable e){
          e.TakeDamage(Attack); // 申請對象傷害
          
          gameObject.SetActive(false); // 結束攻擊
        }
    }
}
