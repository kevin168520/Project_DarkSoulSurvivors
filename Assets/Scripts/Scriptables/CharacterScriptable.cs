using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Character", order = 1)]
public class CharacterScriptable : ScriptableObject
{
    public string Name; // 顯示名稱
    public GameObject spritePrefab; // 資源欲置物
    public GameObject startingWeapon; // 起始武器
}
