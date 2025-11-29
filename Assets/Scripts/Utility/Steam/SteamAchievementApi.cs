using System;

/// <summary> 成就定義，需與Steamworks相同，避免呼叫時無法對應 </summary>
public enum SteamAchievementApi
{
    Complete_Stage1,
    Complete_Stage2,
    Complete_Stage3,
    Complete_Character1,
    Complete_Character2,
    Complete_Character3,
    ACH_MONSTER_HUNTER,
}

/// <summary> 成就資料定義，這些是固定資料，跟隨 Steam 成就資料結構 </summary>
public struct SteamAchievementData
{
    public int AchievementID;
    public string ApiName;
    public string DisplayName;
    public string Description;
    public bool IsUnlocked;
    public int IconImageHandle;
}

/// <summary> 成就參數擴展 </summary>
public static class SteamAchievementApiExtensions
{
    /// <summary> 成就ID </summary>
    public static string GetID(this SteamAchievementApi achievement)
    {
        return Enum.GetName(typeof(SteamAchievementApi), achievement);
    }

    /// <summary> 成就的統計ID </summary>
    public static string GetStatID(this SteamAchievementApi achievement)
    {
        return achievement switch
        {
            SteamAchievementApi.ACH_MONSTER_HUNTER => "STAT_MONSTER_KILL_COUNT",
            _ => throw new NotImplementedException(),
        };
    }

    /// <summary> 成就的統計ID </summary>
    public static int GetStatMax(this SteamAchievementApi achievement)
    {
        return achievement switch
        {
            SteamAchievementApi.ACH_MONSTER_HUNTER => 1000,
            _ => throw new NotImplementedException(),
        };
    }
}