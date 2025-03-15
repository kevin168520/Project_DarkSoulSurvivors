using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public WeaponScriptable weaponData;
    // 武器素質
    public int weaponNumber; // 武器編號
    public int weaponLevel; // 武器等級
    public int attack; // 攻擊值
    public float attackInterval{get => attackCounter.GetTimeInterval(); set => attackCounter.SetTimeInterval(value);} // 攻擊間格
    [SerializeField] TimeCounter attackCounter  = new TimeCounter(1f, true); // 攻擊間格 計時用
    public float activeInterval; // 攻擊持續
    public float flightSpeed; // 飛行攻擊速度

    public void SetWeaponData(WeaponScriptable weapon) {
      weaponData = weapon;

      weaponNumber = weapon.weaponNumber;
      weaponLevel = weapon.weaponLevel;
      attack = weapon.attack;
      attackInterval = weapon.attackInterval;
      activeInterval = weapon.activeInterval;
      flightSpeed = weapon.flightSpeed;
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