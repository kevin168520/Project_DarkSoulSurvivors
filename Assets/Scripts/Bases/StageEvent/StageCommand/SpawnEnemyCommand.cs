using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpawnEnemyCommand : StageCommandBase
{
    public override void Execute()
    {
        Debug.Log("SpawnEnemyCommand");
    }
}
