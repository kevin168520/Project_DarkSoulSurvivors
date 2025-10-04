using UnityEngine;
using Steamworks;
using Cysharp.Threading.Tasks;
using System;

public class LauncherManager : ManagerMonoBase
{
#if UNITY_EDITOR
    [SerializeField] bool skipSteam;
#endif

    private async void Start()
    {
        // 判斷 Steam 登入
        if (!await TrySteamLoginAsync())
        {
            AppQuit("Steam 初始化失敗，請透過 Steam 啟動遊戲");
            return;
        }

        // 判斷 Steam 初始化
        if (!await InitSteam())
        {
            AppQuit("Steam 初始化失敗，請透過 Steam 啟動遊戲");
            return;
        }

        // 載入資料並套用環境設定
        var userData = await StorageUtility.UserStoreData().LoadAsync();
        SetupResolution(userData.bFullScreen, userData.iWondowsResolution);
        SetupAudio(userData.iVolumeBGM, userData.iVolumeSFX, userData.iVolumeALL);
        DataGlobalManager._userData = userData;

        // 載入下一個場景
        SceneGlobalManager.LauncherLoadStartScene();
    }

    /// <summary> Steam 登入處理 </summary>
    private async UniTask<bool> TrySteamLoginAsync()
    {
#if UNITY_EDITOR
        if (skipSteam) return true;
#endif
        await UniTask.CompletedTask;
        return SteamManager.Initialized;
    }

    /// <summary> Steam 初始化 </summary>
    private async UniTask<bool> InitSteam()
    {
#if UNITY_EDITOR
        if (skipSteam) return true;
#endif
        try
        {
            // 初始化 Steam 成就系統
            AchievementGlobalManager.StartAchevement();

            // 此處可加入後端帳號綁定、資料下載等流程（也是用 await
            CSteamID steamID = SteamUser.GetSteamID();
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            return false;
        }
        await UniTask.CompletedTask;
        return true;
    }

    /// <summary> 解析度依照使用者設定(UserStoreData)調整</summary>
    private void SetupResolution(bool full, int resolution)
    {
        var screenMode = full ? FullScreenMode.MaximizedWindow : FullScreenMode.Windowed;
        switch (resolution)
        {
            case 0: Screen.SetResolution(1920, 1080, screenMode); break;
            case 1: Screen.SetResolution(2560, 1440, screenMode); break;
            default: Screen.SetResolution(1920, 1080, screenMode); break;
        }
    }

    /// <summary> 音量大小依照使用者設定(UserStoreData)調整 </summary>
    private void SetupAudio(int bgm, int sfx, int ratio)
    {
        AudioGlobalManager.BGMReset(ratio * bgm / 10000f);
        AudioGlobalManager.SFXReset(ratio * sfx / 10000f);
    }

    /// <summary> 呼叫 Windos 系統的對話框 </summary>
    [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
    private static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

    /// <summary> 開啟對話框 </summary>
    private static void MessageBoxShow(string text, string caption) => MessageBox(IntPtr.Zero, text, caption, 0);

    /// <summary> 關閉 APP </summary>
    private void AppQuit(string msg)
    {
#if UNITY_EDITOR
        Debug.LogWarning(msg);
        UnityEditor.EditorApplication.isPlaying = false;
#else
        MessageBoxShow(msg, "錯誤");
        Application.Quit();
#endif
    }
}
