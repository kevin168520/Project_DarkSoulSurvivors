using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using Cysharp.Threading.Tasks;
using System;

/// <summary> Launcher定義 </summary>
public enum LoginPlatform { Steam, EA, Custom }

public class LauncherManager : ManagerMonoBase
{
    public LoginPlatform loginPlatform;

    private int iVolumeALL;
    private int iVolumeBGM;
    private int iVolumeSFX;
    private bool isFullScreen;
    private int iWindowResolution;
    private int iLanguage;
    UserStoreData _userData
    {
        get => DataGlobalManager._userData;
        set => DataGlobalManager._userData = value;
    }

    private void Start()
    {
        // UserData載入
        UserStoreData data = new UserStoreData();
        StoreDataRepository.UserDataLoading(ref data);
        _userData = data;

        UserDataLoading();
        LauncherSelectAsync(loginPlatform).Forget();
    }

    /// <summary> 載入使用者環境設定資訊 </summary>
    private void UserDataLoading() {
        UserStoreData userStoreData = DataGlobalManager._userData;
        iVolumeALL = userStoreData.iVolumeALL;
        iVolumeBGM = userStoreData.iVolumeBGM;
        iVolumeSFX = userStoreData.iVolumeSFX;
        isFullScreen = userStoreData.bFullScreen;
        iWindowResolution = userStoreData.iWondowsResolution;
        iLanguage = userStoreData.iLanguage;
    }

    /// <summary> 判斷登入的Launcher類型 </summary>
    private async UniTask LauncherSelectAsync(LoginPlatform login)
    {
        try {
            switch (login) { // 可擴充其他平台
                case LoginPlatform.Steam:
                    bool isSuccess = await TrySteamLoginAsync();

                    if (isSuccess) {
                        Debug.Log("Steam 登入成功");
                        AchievementGlobalManager.StartAchevement(); // 成就初始化
                        UserEnvironmentLoad(iVolumeALL, iVolumeBGM, iVolumeSFX, isFullScreen, iWindowResolution, iLanguage);
                    }
                    else {
                        Debug.LogError("Error001:Steam 初始化失敗，請透過 Steam 啟動遊戲");
                    }
                    break;
            }
        }
        catch (System.Exception ex) {
            Debug.LogException(ex); // 為 fire-and-forget 加入安全網
        }
    }

    /// <summary> Steam 登入處理（使用 UniTask 非同步流程） </summary>
    private async UniTask<bool> TrySteamLoginAsync()
    {
        await UniTask.Yield(); // 確保進入非同步流程（可擴充等待初始化）

        if (!SteamManager.Initialized) {
            Debug.LogError("Error002:Steam 初始化失敗，請透過 Steam 啟動遊戲");
            return false;
        }

        CSteamID steamID = SteamUser.GetSteamID();  // 此處可加入後端帳號綁定、資料下載等流程（也是用 await）
        Debug.Log("Steam ID: " + steamID);
        return true;
    }

    /// <summary> 使用者環境設定帶入後進入主場景 </summary>
    private void UserEnvironmentLoad(int VolumeALL, int VolumeBGM, int VolumeSFX, bool FullScreen,int WindowResolution, int Language)
    {
        FullScreenMode mode;
        mode = (FullScreen) ? FullScreenMode.ExclusiveFullScreen : FullScreenMode.MaximizedWindow;

        // 解析度依照使用者設定(UserStoreData)調整
        switch (WindowResolution) {
            case 0:
                Screen.SetResolution(1920, 1080, mode);
                break;
            case 1:
                Screen.SetResolution(2560, 1440, mode);
                break;
            default:
                Screen.SetResolution(1920, 1080, mode);
                break;
        }

        // 音量大小依照使用者設定(UserStoreData)調整
        float fBGM = (VolumeALL * VolumeBGM) / 10000f;
        float fSFX = (VolumeALL * VolumeSFX) / 10000f;

        AudioGlobalManager.BGMReset(fBGM);
        AudioGlobalManager.SFXReset(fSFX);

        SceneGlobalManager.LauncherLoadStartScene(); // 進入主場景
    }
}
