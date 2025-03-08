using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] protected WeaponScriptable weaponData; // 武器數據
    TimeCounter attackCounter  = new TimeCounter(1f, true); // 計時用

    public void SetWeaponData(WeaponScriptable d) {
        weaponData = d;
        attackCounter.SetTimeInterval(weaponData.timeToAttack);
    }
    
    // 計時攻擊頻率
    void Update(){
      if(attackCounter.UpdateDelta())
      {
        Attack();
      }
    }

    // 子類取得當前傷害倍率
    // void GetDamage() {
    //   // virtual SetData(WeaponData wd) 設定武器數據
    //   weaponData = wd;
    //   timeToAttack = weaponData.stats.timeToAttack;
    //   weaponStats = new WeaponStats(wd.stats.damage, wd.stats.timeToAttack)
    // }

    // 子類實作攻擊
    abstract protected void Attack();
}