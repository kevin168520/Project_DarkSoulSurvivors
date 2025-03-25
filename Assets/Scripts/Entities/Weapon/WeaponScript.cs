using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : WeaponBase
{
    IDirection direction => GameManager.instance.playerDirection; // 方向組件
    [SerializeField] private List<WeaponBehaviourBase> weaponBehaviours;
    
    void Start() {
        foreach(var weaponBehaviour in weaponBehaviours) {
            weaponBehaviour.OnAttackEvent = ApplyDamage;
        }
    }

    // 執行攻擊 生成攻擊行為
    override protected void HandleAttack()
    {
      
        if(direction.Normalized.x >= 0){
          weaponBehaviours[0].ApplyWeaponStats(this);
          weaponBehaviours[0].gameObject.SetActive(true);
        } else {
          weaponBehaviours[1].ApplyWeaponStats(this);
          weaponBehaviours[1].gameObject.SetActive(true);
        }
    }
}
