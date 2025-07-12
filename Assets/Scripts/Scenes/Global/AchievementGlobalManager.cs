using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Steamworks;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

/// <summary> 成就Api_ID 定義，需與Steamworks相同，避免呼叫時無法對應 </summary>
#region
public enum eSteamAchievementApi : int
{
    CharaA_Stage1 = 0,
    CharaB_Stage1 = 1,
    CharaC_Stage1 = 2,
    CharaA_Stage2 = 3,
    CharaB_Stage2 = 4,
    CharaC_Stage2 = 5,
}
#endregion

public class AchievementGlobalManager : GlobalMonoBase<AchievementGlobalManager>
{
    private CGameID m_GameID;

    private bool m_bStatsValid;
    private bool m_bStoreStats;

    private bool[] bUnlockAchievement = new bool[Enum.GetNames(typeof(eSteamAchievementApi)).Length];

    protected Callback<UserStatsReceived_t> m_UserStatsReceived;
    protected Callback<UserStatsStored_t> m_UserStatsStored;
    protected Callback<UserAchievementStored_t> m_UserAchievementStored;

    //與Steamworks對應的成就比對表
    private Achievement_t[] m_Achievements = new Achievement_t[] {
        new Achievement_t(eSteamAchievementApi.CharaA_Stage1, "CharaA_Stage1 Success", "Successfully clears the first level using character A"),
        new Achievement_t(eSteamAchievementApi.CharaB_Stage1, "CharaB_Stage1 Success", "Successfully clears the first level using character B"),
        new Achievement_t(eSteamAchievementApi.CharaC_Stage1, "CharaC_Stage1 Success", "Successfully clears the first level using character C"),
        new Achievement_t(eSteamAchievementApi.CharaA_Stage2, "CharaA_Stage2 Success", "Successfully clears the second level using character A"),
        new Achievement_t(eSteamAchievementApi.CharaB_Stage2, "CharaB_Stage2 Success", "Successfully clears the second level using character B"),
        new Achievement_t(eSteamAchievementApi.CharaC_Stage2, "CharaC_Stage2 Success", "Successfully clears the second level using character C"),
    };

    /// <summary> 在LauncherScene先啟動AchievementGlobalManager </summary>
    public void StartAchevement()
    {
        //檢查成就是否存在於Steamworks且遊戲必須已經發佈，未發佈則成就存在也會是false
        #if UNITY_EDITOR
        foreach (eSteamAchievementApi id in Enum.GetValues(typeof(eSteamAchievementApi)))
        {
            bool exists; //判斷對應成就是否存在於Steamworks上
            bool valid = SteamUserStats.GetAchievement(id.ToString(), out exists);
            Debug.Log($"{id} | 存在: {valid} | 已解鎖: {exists}");
        }
        #endif

        InitStatsAsync().Forget(); // 非同步初始化
    }

    /// <summary> Steam Stats 與 Achievement 觸發流程 /// </summary>
    /// <returns></returns>
    private async UniTask InitStatsAsync()
    {
        try
        {
            if (!SteamManager.Initialized)
            {
                Debug.LogWarning("SteamManager初始化未完成");
                return;
            }

            m_GameID = new CGameID(SteamUtils.GetAppID());

            m_UserStatsReceived = Callback<UserStatsReceived_t>.Create(OnUserStatsReceived);
            m_UserStatsStored = Callback<UserStatsStored_t>.Create(OnUserStatsStored);
            m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);

            bool success = SteamUserStats.RequestCurrentStats();
            if (!success)
            {
                Debug.LogError("SteamStats資訊取得失敗");
                return;
            }

            // 等待直到 Stats 被 Steam 回傳並成功解析
            await UniTask.WaitUntil(() => m_bStatsValid);

            // 判斷成就是否解鎖
            foreach (Achievement_t achievement in m_Achievements)
            {
                if (achievement.m_bAchieved)
                    continue;

                int i = (int)achievement.m_eAchievementID;
                if (bUnlockAchievement[i])
                {
                    UnlockAchievement(achievement, i);
                }
            }

            if (m_bStoreStats)
            {
                //SteamUserStats.SetStat("DummyStat", 1f); // 範例測試
                //bool storeSuccess = SteamUserStats.StoreStats();
                //m_bStoreStats = !storeSuccess;
                //Debug.Log("嘗試儲存成就：" + storeSuccess);
                Debug.Log("成就儲存");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("成就儲存 InitStatsAsync 發生例外: " + ex.Message + "\n" + ex.StackTrace);
        }
    }

    /// <summary> 取得來自Steam的數據統計(Stats) </summary>
    private void OnUserStatsReceived(UserStatsReceived_t pCallback)
    {
        if ((ulong)m_GameID != pCallback.m_nGameID)
            return;

        if (pCallback.m_eResult != EResult.k_EResultOK)
        {
            Debug.LogError("SteamStats資訊取得失敗: " + pCallback.m_eResult);
            return;
        }

        m_bStatsValid = true;

        foreach (var ach in m_Achievements)
        {
            bool result = SteamUserStats.GetAchievement(ach.m_eAchievementID.ToString(), out ach.m_bAchieved);
            if (result)
            {
                ach.m_strName = SteamUserStats.GetAchievementDisplayAttribute(ach.m_eAchievementID.ToString(), "name");
                ach.m_strDescription = SteamUserStats.GetAchievementDisplayAttribute(ach.m_eAchievementID.ToString(), "desc");
            }
            else
            {
                Debug.LogWarning("SteamAchievement資訊取得失敗: " + ach.m_eAchievementID);
            }
        }
    }

    /// <summary> 進行Steam的數據統計(Stats)儲存 </summary>
    private void OnUserStatsStored(UserStatsStored_t pCallback)
    {
        if ((ulong)m_GameID != pCallback.m_nGameID)
            return;

        if (pCallback.m_eResult == EResult.k_EResultInvalidParam)
        {
            Debug.LogWarning("SteamAchievement資訊取得失敗");
            OnUserStatsReceived(new UserStatsReceived_t
            {
                m_eResult = EResult.k_EResultOK,
                m_nGameID = (ulong)m_GameID
            });
        }
    }

    /// <summary> 進行Steam的成就(Achievement)儲存 </summary>
    private void OnAchievementStored(UserAchievementStored_t pCallback)
    {
        if ((ulong)m_GameID != pCallback.m_nGameID)
            return;

        if (pCallback.m_nMaxProgress == 0)
        {
            Debug.Log("成就已解鎖: " + pCallback.m_rgchAchievementName);
        }
        else
        {
            Debug.Log("成就資料更新: " + pCallback.m_rgchAchievementName +
                      $" ({pCallback.m_nCurProgress}/{pCallback.m_nMaxProgress})");
        }
    }

    /// <summary> 外部呼叫成就解鎖 </summary>
    public void SteamAchievementUnlock(eSteamAchievementApi id)
    {
        Debug.Log("解鎖請求: " + id);
        bUnlockAchievement[(int)id] = true;
        InitStatsAsync().Forget(); // 重新判斷並嘗試解鎖
    }

    /// <summary> Steam本地端成就解鎖，實際在伺服器有進行解鎖動作需要進行SteamUserStats.StoreStats(); </summary>
    private void UnlockAchievement(Achievement_t achievement, int i)
    {
        Debug.Log("解鎖成就: " + achievement.m_eAchievementID);
        achievement.m_bAchieved = true;
        bUnlockAchievement[i] = false;

        SteamUserStats.SetAchievement(achievement.m_eAchievementID.ToString());

        if (SteamUserStats.GetAchievement(achievement.m_eAchievementID.ToString(), out bool exists))
        {
            Debug.Log("解鎖成就成功");
        }
        else
        {
            Debug.LogWarning("解鎖成就失敗 - 成就ID不存在");
        }

        m_bStoreStats = true;
    }

    /// <summary> 建構式-成就參數定義 </summary>
    private class Achievement_t
    {
        public eSteamAchievementApi m_eAchievementID;
        public string m_strName;
        public string m_strDescription;
        public bool m_bAchieved;

        public Achievement_t(eSteamAchievementApi api, string name, string desc)
        {
            m_eAchievementID = api;     //成就ID (Api_ID)
            m_strName = name;           //成就的顯示名稱 (DisplayName)
            m_strDescription = desc;    //成就的內容敘述 (Description)
            m_bAchieved = false;        //成就是否已解鎖
        }
    }
}