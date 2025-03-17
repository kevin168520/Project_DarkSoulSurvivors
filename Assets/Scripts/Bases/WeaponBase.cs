using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    // 武器素質
    public int weaponNumber; // 武器編號
    public int weaponLevel; // 武器等級
    public int weaponLevelMax; // 武器最高等級
    public int attack; // 攻擊值
    public float attackInterval{get => attackCounter.GetTimeInterval(); set => attackCounter.SetTimeInterval(value);} // 攻擊間格
    [SerializeField] TimeCounter attackCounter  = new TimeCounter(1f, true); // 攻擊間格 計時用
    public float activeInterval; // 攻擊持續
    public float flightSpeed; // 飛行攻擊速度

    // 設置武器資料
    public void LoadWeaponData(WeaponScriptable weaponData) {
      weaponNumber = weaponData.weaponNumber;
      weaponLevel = weaponData.weaponLevel;
      weaponLevelMax = weaponData.weaponDatas.Count;
      attack = weaponData.attack;
      attackInterval = weaponData.attackInterval;
      activeInterval = weaponData.activeInterval;
      flightSpeed = weaponData.flightSpeed;
    }
    
    // 判定武器可升級
    public bool IsWeaponUpgradeable() {
        return weaponLevelMax > (weaponLevel+1);
    }

    // 判定同一把武器
    public bool IsSameWeapon(WeaponScriptable weaponData) {
        return weaponData.weaponNumber == weaponNumber;
    }
    
    // 計時攻擊頻率
    void Update(){
      if(attackCounter.UpdateDelta())
      {
        HandleAttack();
      }
    }

    // 子類實作攻擊
    abstract protected void HandleAttack();
}