using UnityEngine;
using Steamworks;
using Cysharp.Threading.Tasks;
using System;

public class LauncherManager : ManagerMonoBase
{
    UserStoreData _userData
    {
        get => DataGlobalManager._userData;
        set => DataGlobalManager._userData = value;
    }

    private async void Start()
    {
        // 載入資料並套用環境設定
        if (_userData == null)
            _userData = new UserStoreData();

        _userData = await StorageUtility.UserStoreData().LoadAsync();
        UserEnvironmentLoad(_userData);

        // 判斷 Steam 登入
        await TrySteamLoginAsync();

        // 初始化 Steam 成就系統
        AchievementGlobalManager.StartAchevement();

        // 載入下一個場景
        SceneGlobalManager.LauncherLoadStartScene();
    }

    /// <summary> Steam 登入處理 </summary>
    private async UniTask TrySteamLoginAsync()
    {
        await UniTask.Yield(); // 確保進入非同步流程（可擴充等待初始化）

        if (!SteamManager.Initialized)
        {
            Debug.LogError("Error:Steam 初始化失敗，請透過 Steam 啟動遊戲");
            return;
        }

        CSteamID steamID = SteamUser.GetSteamID();  // 此處可加入後端帳號綁定、資料下載等流程（也是用 await）
        Debug.Log("Steam ID: " + steamID);
    }

    /// <summary> 使用者環境設定帶入後進入主場景 </summary>
    private void UserEnvironmentLoad(UserStoreData userdata)
    {
        var mode = userdata.bFullScreen ? FullScreenMode.MaximizedWindow: FullScreenMode.Windowed;

        // 解析度依照使用者設定(UserStoreData)調整
        switch (userdata.iWondowsResolution)
        {
            case 0: Screen.SetResolution(1920, 1080, mode); break;
            case 1: Screen.SetResolution(2560, 1440, mode); break;
            default: Screen.SetResolution(1920, 1080, mode); break;
        }

        // 音量大小依照使用者設定(UserStoreData)調整
        float fBGM = (userdata.iVolumeALL * userdata.iVolumeBGM) / 10000f;
        float fSFX = (userdata.iVolumeALL * userdata.iVolumeSFX) / 10000f;
        AudioGlobalManager.BGMReset(fBGM);
        AudioGlobalManager.SFXReset(fSFX);
    }
}
