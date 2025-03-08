using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Weapon", order = 1)]
public class WeaponScriptable : ScriptableObject
{
    public int attack; // 攻擊值
    public float attackInterval; // 攻擊間格
    public float activeInterval; // 攻擊持續
    public float flightSpeed; // 飛行攻擊速度
}
