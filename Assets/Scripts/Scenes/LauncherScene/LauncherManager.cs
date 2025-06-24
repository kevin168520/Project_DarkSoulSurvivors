using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

/// <summary>
/// Launcher定義
/// </summary>
public enum LoginPlatform { Steam, EA, Custom }

public class LauncherManager : ManagerMonoBase
{
    public LoginPlatform loginPlatform;

    void Start()
    {
        LauncherSelect(loginPlatform);
    }

    /// <summary>
    /// 判斷登入的Launcher類型
    /// </summary>
    /// <param name="login"></param>
    void LauncherSelect(LoginPlatform login)
    {
        switch (login)
        {
            case LoginPlatform.Steam:
                SteamLogin();
                break;
                //case LoginPlatform.EA:
                //    EALogin();
                //    break;
                //case LoginPlatform.Custom:
                //    CustomLogin();
                //    break;
        }
    }

    /// <summary>
    /// 由Steam登入時的判斷處理
    /// </summary>
    void SteamLogin()
    {
        if (!SteamManager.Initialized)
        {
            Debug.LogError("Steam Initial Failed");
            return;
        }

        var steamID = SteamUser.GetSteamID();
        Debug.Log("Steam ID: " + steamID);
        // 可以送至後端做帳號綁定
        LauncherLoginSuccess();
    }

    /// <summary>
    /// 成功的時自動轉場
    /// </summary>
    void LauncherLoginSuccess()
    {
        Debug.Log("Steam Launcher Login Success!");
        //啟動AchievementGlobalManager
        AchievementGlobalManager.StartAchevement();
        //轉入正式場景
        SceneGlobalManager.LauncherLoadStartScene();
    }
}
