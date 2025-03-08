using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] protected WeaponScriptable weaponData; // 武器數據
    TimeCounter attackCounter  = new TimeCounter(1f, true); // 計時用

    public void SetWeaponData(WeaponScriptable d) {
      weaponData = d;
      attackCounter.SetTimeInterval(d.flightSpeed);
    }
    
    // 計時攻擊頻率
    void Update(){
      if(attackCounter.UpdateDelta())
      {
        Attack();
      }
    }

    // 子類實作攻擊
    abstract protected void Attack();
}