using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneGlobalManager : GlobalMonoBase<SceneGlobalManager>
{
    /// <summary> 載入主遊戲場景 需要指定關卡 </summary>
    public void LoadMainGameScene(ScenesBuildData sGameSceneState)
    {
        SceneManager.LoadScene((int)sGameSceneState, LoadSceneMode.Single);  //關卡 Scene
        SceneManager.LoadScene((int)ScenesBuildData.MainGamePlayer, LoadSceneMode.Additive);   //玩家角色 Scene
    }

    /// <summary> 載入遊戲結算場景 </summary>
    public void LoadSummaryScene ()
    {
        SceneManager.LoadScene((int)ScenesBuildData.SummaryScene);
    }

    /// <summary> 載入開始選單場景 </summary>
    public void LoadStartScene()
    {
        SceneManager.LoadScene((int)ScenesBuildData.StartScene);
    }
}
