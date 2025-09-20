using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class SpawnEnemyCommand : StageCommandBase
{
    [LabelText("生成敵人"), LabelWidth(55)]
    [HorizontalGroup("SpawnEnemyCommand", Width = 100)]
    [MinValue(0), MaxValue(200)]
    public int enemyCount;

    [HideLabel]
    [HorizontalGroup("SpawnEnemyCommand")]
    public EnemyScriptable enemy;

    public override void Execute()
    {
        Debug.Log("SpawnEnemyCommand");
    }
}
