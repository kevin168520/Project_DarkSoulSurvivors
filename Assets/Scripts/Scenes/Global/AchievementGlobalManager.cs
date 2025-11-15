using Cysharp.Threading.Tasks;
using UnityEngine;

public class AchievementGlobalManager : GlobalMonoBase<AchievementGlobalManager>
{
    private SteamAchievement steamAchievement;

    public async UniTask Initialize()
    {
        if (!SteamManager.Initialized)
        {
            Debug.LogWarning("SteamManager初始化未完成");
            return;
        }

        steamAchievement = new SteamAchievement();
        await steamAchievement.Initialize();
    }

    /// <summary> 外部呼叫成就解鎖 </summary>
    public void UnlockAchievement(SteamAchievementApi achievementApi)
    {
        if (steamAchievement == null)
        {
            Debug.LogWarning("steamAchievement 尚未初始化");
            return;
        }
        steamAchievement.UnlockAchievement(achievementApi);
    }

    /// <summary> 外部呼叫成就統計解鎖 </summary>
    public void UnlockAchievementProgress(SteamAchievementApi achievementApi, int value)
    {
        if (steamAchievement == null)
        {
            Debug.LogWarning("steamAchievement 尚未初始化");
            return;
        }
        steamAchievement.UnlockAchievementProgress(achievementApi, value);
    }

    /// <summary> 外部呼叫刷新成就 </summary>
    public void RefreshAchievement()
    {
        if (steamAchievement == null)
        {
            Debug.LogWarning("steamAchievement 尚未初始化");
            return;
        }
        steamAchievement.RefreshAchievement();
    }

    /// <summary> 人物通關成就 </summary>
    public void RequestCharacterAchievement(int number)
    {
        if (steamAchievement == null)
        {
            Debug.LogWarning("steamAchievement 尚未初始化");
            return;
        }
        switch (number)
        {
            case 1:
                UnlockAchievement(SteamAchievementApi.Complete_Character1);
                break;
            case 2:
                UnlockAchievement(SteamAchievementApi.Complete_Character2);
                break;
            case 3:
                UnlockAchievement(SteamAchievementApi.Complete_Character3);
                break;
        }
    }

    /// <summary> 通關通關成就 </summary>
    public void RequestStageAchievement(ScenesBuildData scene)
    {
        if (steamAchievement == null)
        {
            Debug.LogWarning("steamAchievement 尚未初始化");
            return;
        }
        switch (scene)
        {
            case ScenesBuildData.MainGameLevel_1:
                UnlockAchievement(SteamAchievementApi.Complete_Stage1);
                break;
            case ScenesBuildData.MainGameLevel_2:
                UnlockAchievement(SteamAchievementApi.Complete_Stage2);
                break;
            case ScenesBuildData.MainGameLevel_3:
                UnlockAchievement(SteamAchievementApi.Complete_Stage3);
                break;
        }
    }
}