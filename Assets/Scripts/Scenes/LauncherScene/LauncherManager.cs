using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using Cysharp.Threading.Tasks;

/// <summary> Launcher定義 </summary>
public enum LoginPlatform { Steam, EA, Custom }

public class LauncherManager : ManagerMonoBase
{
    public LoginPlatform loginPlatform;

    private void Start()
    {
        LauncherSelectAsync(loginPlatform).Forget();
    }

    /// <summary> 判斷登入的Launcher類型 </summary>
    private async UniTask LauncherSelectAsync(LoginPlatform login)
    {
        try
        {
            switch (login)
            {
                case LoginPlatform.Steam:
                    bool isSuccess = await TrySteamLoginAsync();

                    if (isSuccess)
                    {
                        Debug.Log("Steam 登入成功");
                        AchievementGlobalManager.StartAchevement(); // 成就初始化
                        SceneGlobalManager.LauncherLoadStartScene(); // 進入主場景
                    }
                    else
                    {
                        Debug.LogError("Steam 登入失敗，請確認是否由 Steam 啟動遊戲");
                    }
                    break;

                    // 可擴充其他平台
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogException(ex); // 為 fire-and-forget 加入安全網
        }
    }

    /// <summary> Steam 登入處理（使用 UniTask 非同步流程） </summary>
    private async UniTask<bool> TrySteamLoginAsync()
    {
        await UniTask.Yield(); // 確保進入非同步流程（可擴充等待初始化）

        if (!SteamManager.Initialized)
        {
            Debug.LogError("Steam 未初始化，請透過 Steam 啟動遊戲");
            return false;
        }

        CSteamID steamID = SteamUser.GetSteamID();  // 此處可加入後端帳號綁定、資料下載等流程（也是用 await）
        Debug.Log("Steam ID: " + steamID);

        return true;
    }
}
