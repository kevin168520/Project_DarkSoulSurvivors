using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class WinStageCommand : StageCommandBase
{
    [LabelText("勝利後台訊息"), LabelWidth(80)]
    [SerializeField]
    string message = "WinStageCommand";

    public override void Execute()
    {
        Debug.Log(message);
    }
}
