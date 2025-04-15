using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneGlobalManager : MonoBehaviour
{
    public static SceneGlobalManager inst;

    private void Awake()
    {
        // 檢查是否已經有一個實例
        if (inst == null)
        {
            // 如果沒有實例，設置當前物件為實例並防止銷毀
            inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 如果已有實例，銷毀當前物件
            Destroy(this);
        }
    }

    /// <summary>
    /// 遊戲啟動時的場景切換 StartGameSceneAction(string sGameSceneState)
    /// </summary>
    /// <param name="sGameSceneState"></param>
    public void StartGameSceneAction(ScenesBuildData sGameSceneState)
    {
        AudioGlobalManager.inst.BGMStop();
        SceneManager.LoadScene((int)sGameSceneState, LoadSceneMode.Single);  //關卡 Scene
        SceneManager.LoadScene((int)ScenesBuildData.MainGamePlayer, LoadSceneMode.Additive);   //玩家角色 Scene        
    }

    /// <summary>
    /// 遊戲結束時的場景切換 EndGameSceneAction()
    /// </summary>
    public void EndGameSceneAction ()
    {
        AudioGlobalManager.inst.BGMStop();
        SceneManager.LoadScene((int)ScenesBuildData.SummaryScene);  //跳至結算畫面
    }

    /// <summary>
    /// 返回主選單的場景切換 BackToMenuSceneAction()
    /// </summary>
    public void BackToMenuSceneAction()
    {
        AudioGlobalManager.inst.BGMStop();
        SceneManager.LoadScene((int)ScenesBuildData.StartScene);  //回到主畫面
    }
}
