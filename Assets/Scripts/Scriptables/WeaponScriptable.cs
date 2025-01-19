using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Weapon", order = 1)]
public class WeaponScriptable : ScriptableObject
{
    public int damage;           // 傷害
    public float timeToAttack;   // 攻擊頻率
    public float timeToDisable;  // 攻擊持續
    public float projectileSpeed; // 彈丸速度
}
