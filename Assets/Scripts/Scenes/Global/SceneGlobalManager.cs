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
        AudioGlobalManager.Instance.BGMStop();
        SceneManager.LoadScene((int)sGameSceneState, LoadSceneMode.Single);  //關卡 Scene
        SceneManager.LoadScene((int)ScenesBuildData.MainGamePlayer, LoadSceneMode.Additive);   //玩家角色 Scene        
    }

    /// <summary> 載入遊戲結算場景 </summary>
    public void LoadSummaryScene ()
    {
        AudioGlobalManager.Instance.BGMStop();
        SceneManager.LoadScene((int)ScenesBuildData.SummaryScene); //跳至結算畫面
    }

    /// <summary> 載入遊戲實際開始的選單場景 </summary>
    public void LoadStartScene()
    {
        AudioGlobalManager.Instance.BGMStop();
        SceneManager.LoadScene((int)ScenesBuildData.StartScene); //回到主畫面
    }

    /// <summary> 由Launcher進入遊戲實際開始的選單場景 </summary>
    public void LauncherLoadStartScene()
    {
        SceneManager.LoadScene((int)ScenesBuildData.StartScene); //進入主畫面
    }
}
