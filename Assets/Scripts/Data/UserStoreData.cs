using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/// <summary>
/// 用於存檔/讀取的玩家資料
/// </summary>
public class UserStoreData
{
    public int iVolumeALL;
    public int iVolumeBGM;
    public int iVolumeSFX;
    public bool bFullScreen;
    public int iWondowsResolution;
    public int iLanguage;
}