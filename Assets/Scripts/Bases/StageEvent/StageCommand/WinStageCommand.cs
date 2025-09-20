using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WinStageCommand : StageCommandBase
{
    public override void Execute()
    {
        Debug.Log("SpawnEnemyCommand");
    }
}
