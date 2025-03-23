using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    // 結算素質
    public string weaponName; // 武器名稱
    public Sprite weaponIcon; // 武器圖片
    public int totalDamage; // 總傷害
    
    // 武器素質
    public int weaponNumber; // 武器編號
    public int weaponLevel; // 武器等級
    public int weaponLevelMax; // 武器最高等級
    public int attack; // 攻擊值
    public float attackInterval{ // 攻擊間格
      get => attackCounter.GetTimeInterval(); 
      set => attackCounter.SetTimeInterval(value);
    }
    [SerializeField] TimeCounter attackCounter  = new TimeCounter(1f, true); // 攻擊間格 計時用
    public float activeInterval; // 攻擊持續
    public float flightSpeed; // 飛行攻擊速度

    // 設置武器資料
    public void LoadWeaponData(WeaponScriptable weaponData) {
      weaponIcon = weaponData.weaponIcon;
      weaponName = weaponData.weaponName;
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
    
    // 執行攻擊
    virtual protected bool ApplyDamage(Collider2D collision)
    {
        // 檢查對象
        if(!collision.CompareTag("Enemy")) return false;

        // 判定可受傷
        if(collision.GetComponent<IDamageable>() is IDamageable e){
          e.TakeDamage(attack); // 申請對象傷害

          totalDamage += attack;
          return true;
        }
        return false;
    }
}