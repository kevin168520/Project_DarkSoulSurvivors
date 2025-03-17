using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : WeaponBase
{
    IDirection direction => GameManager.instance.playerDirection; // 方向組件
    [SerializeField] private List<WeaponBehaviourBase> attackEffects;
    

    // 執行攻擊 生成攻擊行為
    override protected void HandleAttack()
    {
      
        if(direction.Normalized.x >= 0){
          attackEffects[0].ApplyWeaponStats(this);
          attackEffects[0].gameObject.SetActive(true);
        } else {
          attackEffects[1].ApplyWeaponStats(this);
          attackEffects[1].gameObject.SetActive(true);
        }
    }
}
