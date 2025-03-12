using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    // 武器素質
    [SerializeField] protected WeaponScriptable weaponData; // 武器數據
    public int attack; // 攻擊值
    public float attackInterval{get => attackCounter.GetTimeInterval(); set => attackCounter.SetTimeInterval(value);} // 攻擊間格
    [SerializeField] TimeCounter attackCounter  = new TimeCounter(1f, true); // 攻擊間格 計時用
    public float activeInterval; // 攻擊持續
    public float flightSpeed; // 飛行攻擊速度

    public void SetWeaponData(WeaponScriptable d) {
      attack = d.attack;
      attackInterval = d.attackInterval;
      activeInterval = d.activeInterval;
      flightSpeed = d.flightSpeed;
      
      weaponData = d;
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

#if DEBUG
    void OnValidate() { // 方便更新武器資料用
      if(weaponData != null) SetWeaponData(weaponData);
    }
#endif
}