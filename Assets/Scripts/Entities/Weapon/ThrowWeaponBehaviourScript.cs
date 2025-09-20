using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowWeaponBehaviourScript : WeaponBehaviourBase
{
    Vector3 velocity;
    float gravity = -30f;
    float verticalSpeed = 14f; // 初始向上速度
    HashSet<Object> enemiesHit = new HashSet<Object>(); // 避免相同敵人多次攻擊 HashSet 無排序速度較快

    // 開啟攻擊
    protected override void OnAttackStart()
    {
        PlaySound(enAudioSfxData.PhysicalWeaponWhip);
        velocity = new Vector3(0, verticalSpeed, 0);
    }
    
    // 彈丸移動
    override protected void OnUpdateStart() {
        // 水平持續向前移動
        transform.position += FlightDirection * FlightSpeed * Time.deltaTime;
      
        // 垂直方向拋物線運動
        velocity.y += gravity * Time.deltaTime;
        transform.position += new Vector3(0, velocity.y, 0) * Time.deltaTime;
    }

    // 執行攻擊
    override protected void HandleAttack(Collider2D collision)
    {
        if(OnAttackEvent(collision)){
          enemiesHit.Add(collision); // 加入曾被攻擊過
        }
    }

    // 結束攻擊
    protected override void OnAttackEnd()
    {
        Destroy(gameObject); // 彈丸自滅
    }
}
