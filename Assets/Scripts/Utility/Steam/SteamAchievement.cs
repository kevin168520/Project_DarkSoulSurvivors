using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Steamworks;
using UnityEngine;

public class SteamAchievement
{
    // Steam 成就回傳監聽
    protected Callback<UserStatsReceived_t> userStatsReceived;
    protected Callback<UserStatsStored_t> userStatsStored;
    protected Callback<UserAchievementStored_t> userAchievementStored;

    // Steam APP ID
    private CGameID gameID;

    // 判定初始化
    public bool Initialized { get; private set; }

    // 判定已接受統計資料
    public bool StatsReceived { get; private set; }

    // 判定需要刷新統計資料
    public bool RefreshPending { get; private set; }

    // 預先建立 Steam 成就表
    private Dictionary<string, SteamAchievementData> achievements = new();

    public async UniTask Initialize()
    {
        if (Initialized) return;

        gameID = new CGameID(SteamUtils.GetAppID());

        userStatsReceived = Callback<UserStatsReceived_t>.Create(OnUserStatsReceived);
        userStatsStored = Callback<UserStatsStored_t>.Create(OnUserStatsStored);
        userAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);

        // 初始化第一次請求資料
        StatsReceived = false;
        if (!SteamUserStats.RequestCurrentStats())
        {
            Debug.LogError("SteamStats資訊取得失敗");
            return;
        }
        await UniTask.WaitUntil(() => StatsReceived);
        // 初始化後載入所有成就資料
        LoadAchievements(ref achievements);

        Initialized = true;
    }

    /// <summary> 載入所有成就資料 只會在一開始執行一次 </summary>
    private void LoadAchievements(ref Dictionary<string, SteamAchievementData> achievementDatas)
    {
        achievementDatas.Clear();
        uint count = SteamUserStats.GetNumAchievements();
        for (uint i = 0; i < count; i++)
        {
            SteamAchievementData data = new SteamAchievementData();

            data.AchievementID = (int)i;
            data.ApiName = SteamUserStats.GetAchievementName(i);

            bool achieved;
            SteamUserStats.GetAchievement(data.ApiName, out achieved);
            data.IsUnlocked = achieved;

            data.DisplayName = SteamUserStats.GetAchievementDisplayAttribute(data.ApiName, "name");
            data.Description = SteamUserStats.GetAchievementDisplayAttribute(data.ApiName, "desc");

            data.IconImageHandle = SteamUserStats.GetAchievementIcon(data.ApiName);

            achievementDatas.Add(data.ApiName, data);
        }
    }

    /// <summary> 檢查成就是否存在於Steamworks且遊戲必須已經發佈，未發佈則成就存在也會是false </summary>
    public void Check()
    {
        foreach(var (k, _) in achievements)
        {
            var achievement = achievements[k];
            if (SteamUserStats.GetAchievement(achievement.ApiName, out achievement.IsUnlocked))
                Debug.Log($"{achievement.ApiName} | 解鎖狀態: {achievement.IsUnlocked}");
            else
                Debug.LogError($"{achievement.ApiName} | 不存在");
        }
    }

    /// <summary> Steam本地端成就解鎖 </summary>
    public void UnlockAchievement(SteamAchievementApi achievementApi)
    {
        var achievement = achievements[achievementApi.GetID()];
        if (achievement.IsUnlocked == true)
            return;

        SteamUserStats.SetAchievement(achievement.ApiName);
    }

    /// <summary> Steam本地端成就解鎖 </summary>
    public void UnlockAchievementProgress(SteamAchievementApi achievementApi, int value)
    {
        var achievement = achievements[achievementApi.GetID()];
        if (achievement.IsUnlocked == true)
            return;

        // 累計統計數值
        SteamUserStats.GetStat(achievementApi.GetStatID(), out int current);
        current += value;
        SteamUserStats.SetStat(achievementApi.GetStatID(), current);

        // 檢查是否達成成就
        if (current < achievementApi.GetStatMax())
            return;

        SteamUserStats.SetAchievement(achievement.ApiName);
    }

    /// <summary>
    /// 重新整理並提交目前的 Steam 成就與統計資料，
    /// 必須呼叫此才能及時遊戲中觸發 Steam 成就提示。
    /// </summary>
    public void RefreshAchievement()
    {
        if (RefreshPending) return;
        RefreshPending = true;
        SteamUserStats.StoreStats();
    }

    /// <summary> 取得 Steam 的數據統計(Stats) </summary>
    private void OnUserStatsReceived(UserStatsReceived_t pCallback)
    {
        if (pCallback.m_nGameID != (ulong)gameID)
        {
            Debug.LogError($"Steam GameID({(ulong)gameID}) 不一致: " + pCallback.m_nGameID);
            return;
        }

        if (pCallback.m_eResult != EResult.k_EResultOK)
        {
            Debug.LogError("SteamStats資訊取得失敗: " + pCallback.m_eResult);
            return;
        }

        // 每當取得資料後理應更新成就資料
        Check();
        StatsReceived = true;
    }

    /// <summary> 確認 Steam 的數據統計(Stats) 儲存結果 每次儲存都會觸發</summary>
    private void OnUserStatsStored(UserStatsStored_t pCallback)
    {
        if (pCallback.m_nGameID != (ulong)gameID)
        {
            Debug.LogError($"Steam GameID({(ulong)gameID}) 不一致: " + pCallback.m_nGameID);
            return;
        }

        if (pCallback.m_eResult == EResult.k_EResultInvalidParam)
        {
            Debug.LogError("Steam 統計或成就參數無效");
            return;
        }

        if (pCallback.m_eResult != EResult.k_EResultOK)
        {
            Debug.LogError("Steam 統計資料儲存失敗: " + pCallback.m_eResult);
            return;
        }

        Debug.Log("Steam 統計資料儲存成功。");

        // 如果有刷新請求 則額外執行
        if (RefreshPending)
        {
            RefreshPending = false;
            StatsReceived = false;
            SteamUserStats.RequestCurrentStats();
        }
        SteamUserStats.GetNumAchievements();
    }

    /// <summary> 確認 Steam 的成就(Achievement) 解鎖結果 只有新成就解鎖才會觸發 </summary>
    /// pCallback.m_nMaxProgress 可以作為判定是否統計成就，但這邊是接收已解鎖成就，追蹤進度也沒意義
    private void OnAchievementStored(UserAchievementStored_t pCallback)
    {
        if (pCallback.m_nGameID != (ulong)gameID)
            return;

        Debug.Log("Steam 成就解鎖: " + pCallback.m_rgchAchievementName);
    }
}
