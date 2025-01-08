using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : WeaponBase
{
    [SerializeField] private string targetTag; // 目標
    [SerializeField] private List<WeaponBehaviour> attackEffects;
    

    // 執行攻擊 生成攻擊行為
    override protected void Attack()
    {
        foreach(WeaponBehaviour attackEffect in attackEffects) {
          attackEffect.transform.position = transform.position + new Vector3(2, -0.5f, 0);
          attackEffect.SetWeaponData(weaponData);

          attackEffect.gameObject.SetActive(true);
        }
    }
}
