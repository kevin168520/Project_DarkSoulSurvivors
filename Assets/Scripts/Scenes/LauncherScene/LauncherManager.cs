using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

/// <summary>
/// Launcher�w�q
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
    /// �P�_�n�J��Launcher����
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
    /// ��Steam�n�J�ɪ��P�_�B�z
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
        // �i�H�e�ܫ�ݰ��b���j�w
        LauncherLoginSuccess();
    }

    /// <summary>
    /// ���\���ɦ۰����
    /// </summary>
    void LauncherLoginSuccess()
    {
        Debug.Log("Steam Launcher Login Success!");
        //�Ұ�AchievementGlobalManager
        AchievementGlobalManager.StartAchevement();
        //��J��������
        SceneGlobalManager.LauncherLoadStartScene();
    }
}
