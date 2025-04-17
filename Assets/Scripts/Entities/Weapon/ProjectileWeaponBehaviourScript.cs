using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponBehaviour : WeaponBehaviourBase
{
    // 開啟攻擊
    protected override void OnAttackStart()
    {
        PlaySound(enAudioSfxData.PhysicalWeaponArrow);
    }
    
    // 彈丸移動
    override protected void OnUpdateStart() {
      transform.position += FlightDirection * FlightSpeed * Time.deltaTime;
    }

    // 執行攻擊
    override protected void HandleAttack(Collider2D collision)
    {
        if(OnAttackEvent(collision)){
          OnAttackEnd(); // 彈丸擊中目標後結束
        }
    }

    // 結束攻擊
    protected override void OnAttackEnd()
    {
        Destroy(gameObject); // 彈丸自滅
    }
}
