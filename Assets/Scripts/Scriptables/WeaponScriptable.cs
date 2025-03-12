using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Weapon", order = 1)]
public class WeaponScriptable : ScriptableObject
{
    public int attack{get => upgradeData[upgradeLevel].attack;} // 攻擊值
    public float attackInterval{get => upgradeData[upgradeLevel].attackInterval;} // 攻擊間格
    public float activeInterval{get => upgradeData[upgradeLevel].activeInterval;} // 攻擊持續
    public float flightSpeed{get => upgradeData[upgradeLevel].flightSpeed;} // 飛行攻擊速度
    public WeaponBase weaponPrefab; // 武器模組
    [NonSerialized]public int upgradeLevel; // 當前升級等級
    public List<WeaponData> upgradeData; // 升級資料
}

[System.Serializable]
public struct WeaponData {
    public int attack; // 攻擊值
    public float attackInterval; // 攻擊間格
    public float activeInterval; // 攻擊持續
    public float flightSpeed; // 飛行攻擊速度
    public string description; // 說明文
}