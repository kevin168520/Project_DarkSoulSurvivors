using System;
using UnityEngine;

[Serializable]
public class SpawnEnemyCommand : StageCommandBase
{
    public EnemyScriptable enemy;

    [Range(0, 200)]
    public int enemyCount;

    public override void Execute()
    {
        Debug.Log("SpawnEnemyCommand");
    }
}
