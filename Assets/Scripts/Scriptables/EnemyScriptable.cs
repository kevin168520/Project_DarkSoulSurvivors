using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Enemy", order = 1)]
public class EnemyScriptable : ScriptableObject
{
    public GameObject spritePrefab; // 資源欲置物
    public string showName;

    public int hp;
    public int damage;
    public float speed;
}
