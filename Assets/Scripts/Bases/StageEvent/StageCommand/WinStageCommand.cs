using System;
using UnityEngine;

[Serializable]
public class WinStageCommand : StageCommandBase
{
    [SerializeField]
    string message = "WinStageCommand"; // 後台訊息

    public override void Execute()
    {
        Debug.Log(message);
    }
}
