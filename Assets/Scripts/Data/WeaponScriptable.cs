using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Weapon", order = 1)]
public class WeaponScriptable : ScriptableObject
{
    // 武器資訊
    public int weaponNumber; // 武器編號
    public string weaponName; // 武器名稱
    public Sprite weaponIcon; // 武器圖片
    public WeaponBase weaponPrefab; // 武器模組
    public int weaponLevel; // 武器等級
    public List<WeaponData> weaponDatas; // 武器資料

    // 武器素質
    public int attack{get => weaponDatas[weaponLevel].attack;} // 攻擊值
    public float attackInterval{get => weaponDatas[weaponLevel].attackInterval;} // 攻擊間格
    public float activeInterval{get => weaponDatas[weaponLevel].activeInterval;} // 攻擊持續
    public float flightSpeed{get => weaponDatas[weaponLevel].flightSpeed;} // 飛行攻擊速度
    public string description{get => weaponDatas[weaponLevel].description;} // 可以升級說明

    // 比較方法
    public bool EqualsWeapon(WeaponScriptable weapon) // 比較同一把武器
    {
        return weaponNumber == weapon.weaponNumber;
    }

    public override bool Equals(object obj) // List 相關比較
    {
        if (obj is WeaponScriptable other)
        {
            return EqualsWeapon(other);
        }
        return false;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(weaponName);
    }
}

[System.Serializable]
public struct WeaponData {
    public int attack; // 攻擊值
    public float attackInterval; // 攻擊間格
    public float activeInterval; // 攻擊持續
    public float flightSpeed; // 飛行攻擊速度
    public string description; // 說明文
}