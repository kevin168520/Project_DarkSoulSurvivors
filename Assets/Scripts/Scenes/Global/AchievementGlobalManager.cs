using System;
using System.Collections;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

// ���N API �w�q
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
    private CGameID m_GameID; // Game ID

    // Did we get the stats from Steam?
    private bool m_bRequestedStats; //�O�_�i�H���oSteam�W��Stats(�έp)
    private bool m_bStatsValid;  //Steam�W��Stats�O�_����

    // Should we store stats this frame?
    private bool m_bStoreStats;

    private bool[] bUnlockAchievement = new bool[Enum.GetNames(typeof(eSteamAchievementApi)).Length];

    private int m_iStats1;

    //public AchievementData _achievementData;

    protected Callback<UserStatsReceived_t> m_UserStatsReceived;
    protected Callback<UserStatsStored_t> m_UserStatsStored;
    protected Callback<UserAchievementStored_t> m_UserAchievementStored;

    // �إ߹���Steamworks�����N��
    private Achievement_t[] m_Achievements = new Achievement_t[] {
        new Achievement_t(eSteamAchievementApi.CharaA_Stage1, "CharaA_Stage1 Success", "Successfully clears the first level using character A"),
        new Achievement_t(eSteamAchievementApi.CharaB_Stage1, "CharaB_Stage1 Success", "Successfully clears the first level using character B"),
        new Achievement_t(eSteamAchievementApi.CharaC_Stage1, "CharaC_Stage1 Success", "Successfully clears the first level using character C"),
        new Achievement_t(eSteamAchievementApi.CharaA_Stage2, "CharaA_Stage2 Success", "Successfully clears the second level using character A"),
        new Achievement_t(eSteamAchievementApi.CharaA_Stage2, "CharaA_Stage2 Success", "Successfully clears the second level using character B"),
        new Achievement_t(eSteamAchievementApi.CharaA_Stage2, "CharaA_Stage2 Success", "Successfully clears the second level using character C"),
    };

    public void StartAchevement()
    {
        //�ˬd���N�O�_�s�b��Steamworks�B�C�������w�g�o�G�A���o�G�h���N�s�b�]�|�Ofalse
        #if UNITY_EDITOR
        foreach (eSteamAchievementApi id in Enum.GetValues(typeof(eSteamAchievementApi)))
        {
            bool exists;
            bool valid = SteamUserStats.GetAchievement(id.ToString(), out exists);
            Debug.Log($"{id} | �s�b: {valid} | �w����: {exists}");
        }
        return;
        #endif
    }

    void OnEnable()
    {
        if (!SteamManager.Initialized)
            return;

        // �qSteam��� GameID �Өϥ�Callback���v��
        m_GameID = new CGameID(SteamUtils.GetAppID());

        m_UserStatsReceived = Callback<UserStatsReceived_t>.Create(OnUserStatsReceived);
        m_UserStatsStored = Callback<UserStatsStored_t>.Create(OnUserStatsStored);
        m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);

        // These need to be reset to get the stats upon an Assembly reload in the Editor.
        m_bRequestedStats = false;
        m_bStatsValid = false;
    }

    private void Update()
    {
        // �T�OSteamManager�s�b�B��l�Ƨ���
        if (!SteamManager.Initialized)
            return;

        // �O�_�n�VSteam�ШD�έp�ƾ� (Stats)
        if (!m_bRequestedStats)
        {
            // Is Steam Loaded? if no, can't get stats, done
            if (!SteamManager.Initialized)
            {
                m_bRequestedStats = true;
                return;
            }

            // �VSteam���o�έp�ƾ� (Stats)�A�Ӷ��|Ĳ�oOnUserStatsRecived��Callback
            bool bSuccess = SteamUserStats.RequestCurrentStats();

            // This function should only return false if we weren't logged in, and we already checked that.
            // But handle it being false again anyway, just ask again later.
            m_bRequestedStats = bSuccess;
        }

        // ���N�Ѱ��P�_
        foreach (Achievement_t achievement in m_Achievements)
        {
            if (achievement.m_bAchieved)
                continue;

            for (int i = 0; i < Enum.GetNames(typeof(eSteamAchievementApi)).Length; i++)
            {
                if (bUnlockAchievement[i] && (int)achievement.m_eAchievementID == i)
                {
                    Debug.Log(achievement.m_eAchievementID.ToString() + " / " + i);
                    UnlockAchievemen(achievement, i);
                    break;
                }
            }
        }
            //�ˬd�O�_���x�s�ݨD
            if (m_bStoreStats)
        {
            Debug.Log("StoreStats Start");

            SteamUserStats.SetStat("", 1f); //�Ƚd�Ҧ]��Steam��S�]�w�]���L��ڥ\��

            bool bSuccess = SteamUserStats.StoreStats();

            // If this failed, we never sent anything to the server, try
            // again later.
            m_bStoreStats = !bSuccess;
        }
    }
    //-----------------------------------------------------------------------------
    //  ���o�Ӧ�Steam���ƾڲέp(Stats)
    //-----------------------------------------------------------------------------
    private void OnUserStatsReceived(UserStatsReceived_t pCallback)
    {
        Debug.Log("OnUserStatsReceived");
        // we may get callbacks for other games' stats arriving, ignore them
        if ((ulong)m_GameID == pCallback.m_nGameID)
        {
            if (EResult.k_EResultOK == pCallback.m_eResult)
            {
                Debug.Log("Received stats and achievements from Steam\n");

                m_bStatsValid = true;

                // load achievements
                foreach (Achievement_t ach in m_Achievements)
                {
                    bool ret = SteamUserStats.GetAchievement(ach.m_eAchievementID.ToString(), out ach.m_bAchieved);
                    if (ret)
                    {
                        ach.m_strName = SteamUserStats.GetAchievementDisplayAttribute(ach.m_eAchievementID.ToString(), "name");
                        ach.m_strDescription = SteamUserStats.GetAchievementDisplayAttribute(ach.m_eAchievementID.ToString(), "desc");
                    }
                    else
                    {
                        Debug.LogWarning("SteamUserStats.GetAchievement failed for Achievement " + ach.m_eAchievementID + "\nIs it registered in the Steam Partner site?");
                    }
                }

                // load stats
                SteamUserStats.GetStat("Test1", out m_iStats1);

                // load achievement
                SteamUserStats.GetAchievement(eSteamAchievementApi.CharaA_Stage1.ToString(), out bool achieved1);
                SteamUserStats.GetAchievement(eSteamAchievementApi.CharaB_Stage1.ToString(), out bool achieved2);
                SteamUserStats.GetAchievement(eSteamAchievementApi.CharaC_Stage1.ToString(), out bool achieved3);
                SteamUserStats.GetAchievement(eSteamAchievementApi.CharaA_Stage2.ToString(), out bool achieved4);
                SteamUserStats.GetAchievement(eSteamAchievementApi.CharaB_Stage2.ToString(), out bool achieved5);
                SteamUserStats.GetAchievement(eSteamAchievementApi.CharaC_Stage2.ToString(), out bool achieved6);
            }
            else
            {
                Debug.Log("RequestStats - failed, " + pCallback.m_eResult);
            }
        }
        else
        {
            Debug.Log("Error");
        }
    }

    //-----------------------------------------------------------------------------
    // �i��Steam���ƾڲέp(Stats)�x�s
    //-----------------------------------------------------------------------------
    private void OnUserStatsStored(UserStatsStored_t pCallback)
    {
        Debug.Log("OnUserStatsStored");
        // �ư�����Ӧ�Steam����L�C���έp����x�s��Callback�A�L�o�X�ۨ��C�����ӳB�z
        if ((ulong)m_GameID == pCallback.m_nGameID)
        {
            if (EResult.k_EResultOK == pCallback.m_eResult)
            {
                Debug.Log("StoreStats - success");
            }
            else if (EResult.k_EResultInvalidParam == pCallback.m_eResult)
            {
                // One or more stats we set broke a constraint. They've been reverted,
                // and we should re-iterate the values now to keep in sync.
                Debug.Log("StoreStats - some failed to validate");
                // Fake up a callback here so that we re-load the values.
                UserStatsReceived_t callback = new UserStatsReceived_t();
                callback.m_eResult = EResult.k_EResultOK;
                callback.m_nGameID = (ulong)m_GameID;
                OnUserStatsReceived(callback);
            }
            else
            {
                Debug.Log("StoreStats - failed, " + pCallback.m_eResult);
            }
        }
    }

    //-----------------------------------------------------------------------------
    // �i��Steam�����N(Achievement)�x�s
    //-----------------------------------------------------------------------------
    private void OnAchievementStored(UserAchievementStored_t pCallback)
    {
        Debug.Log("OnAchievementStored");

        // We may get callbacks for other games' stats arriving, ignore them
        if ((ulong)m_GameID == pCallback.m_nGameID)
        {
            if (0 == pCallback.m_nMaxProgress)
            {
                Debug.Log("Achievement '" + pCallback.m_rgchAchievementName + "' unlocked!");
            }
            else
            {
                Debug.Log("Achievement '" + pCallback.m_rgchAchievementName + "' progress callback, (" + pCallback.m_nCurProgress + "," + pCallback.m_nMaxProgress + ")");
            }
        }
    }

    /// <summary>
    /// ���N����
    /// </summary>
    public void SteamAchievementUnlock(eSteamAchievementApi id)
    {
        Debug.Log((int)id + " / " + id.ToString());
        bUnlockAchievement[(int)id] = true;
    }

    private void UnlockAchievemen(Achievement_t achievement, int i)
    {
        Debug.Log("UnlockAchievement: " + achievement.m_eAchievementID.ToString());
        achievement.m_bAchieved = true;
        bUnlockAchievement[i] = false;
        // mark it down
        SteamUserStats.SetAchievement(achievement.m_eAchievementID.ToString());

        bool exists;
        if (SteamUserStats.GetAchievement(achievement.m_eAchievementID.ToString(), out exists))
        {
            Debug.Log("���N�s�b�A��e���A: " + exists);
        }
        else
        {
            Debug.LogWarning("���N ID ���s�b�I");
        }

        m_bStoreStats = true;
    }

    private class Achievement_t
    {
        public eSteamAchievementApi m_eAchievementID;
        public string m_strName;
        public string m_strDescription;
        public bool m_bAchieved;

        /// <summary>
        /// Creates an Achievement. You must also mirror the data provided here in https://partner.steamgames.com/apps/achievements/yourappid
        /// </summary>
        /// <param name="achievement">The "API Name Progress Stat" used to uniquely identify the achievement.</param>
        /// <param name="name">The "Display Name" that will be shown to players in game and on the Steam Community.</param>
        /// <param name="desc">The "Description" that will be shown to players in game and on the Steam Community.</param>
        public Achievement_t(eSteamAchievementApi api, string name, string desc)
        {
            m_eAchievementID = api;
            m_strName = name;
            m_strDescription = desc;
            m_bAchieved = false;
        }
    }
}