using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Character", order = 1)]
public class CharacterScriptable : ScriptableObject
{
    public string showName; // 顯示名稱
    public GameObject spritePrefab; // 資源欲置物
    public WeaponScriptable startingWeapon; // 起始武器

    public int hp = 100; // 血量
    public int def = 0; // 防禦
    public float speedMult = 1f; // 速度倍率
    public float attackMult = 1f; // 攻擊倍率
}
