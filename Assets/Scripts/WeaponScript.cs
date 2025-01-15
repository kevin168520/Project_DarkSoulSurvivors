using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : WeaponBase
{
    [SerializeField] private List<WeaponBehaviourBase> attackEffects;
    

    // 執行攻擊 生成攻擊行為
    override protected void Attack()
    {
      
        if(direction.Normalized.x >= 0){
          attackEffects[0].SetWeaponData(weaponData);
          attackEffects[0].gameObject.SetActive(true);
        } else {
          attackEffects[1].SetWeaponData(weaponData);
          attackEffects[1].gameObject.SetActive(true);
        }
    }
}
