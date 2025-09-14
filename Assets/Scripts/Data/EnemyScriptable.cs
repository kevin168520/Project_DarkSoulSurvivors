using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Enemy", order = 1)]
public class EnemyScriptable : ScriptableObject
{
    [Title("基礎數值")]
    public string showName;
    public int hp;
    public int damage;
    public float speed;

    [Title("參考資源")]
    public Sprite sprite;
    public RuntimeAnimatorController animator;
    public Vector2 offset;
    public Vector2 size;

    [Title("掉落物品")]
    [AssetsOnly, AssetSelector(Paths = "Assets/Prefabs/Item", FlattenTreeView = true)]
    public List<GameObject> drop;
}
