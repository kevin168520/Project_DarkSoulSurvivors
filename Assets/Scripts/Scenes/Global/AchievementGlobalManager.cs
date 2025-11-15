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
        steamAchievement.UnlockAchievement(achievementApi);
    }

    /// <summary> 外部呼叫成就統計解鎖 </summary>
    public void UnlockAchievementProgress(SteamAchievementApi achievementApi, int value)
    {
        steamAchievement.UnlockAchievementProgress(achievementApi, value);
    }

    /// <summary> 外部呼叫刷新成就 </summary>
    public void RefreshAchievement()
    {
        steamAchievement.RefreshAchievement();
    }

    /// <summary> 人物通關成就 </summary>
    public void RequestCharacterAchievement(int number)
    {
        switch (number)
        {
            case 0:
                UnlockAchievement(SteamAchievementApi.Complete_Character1);
                break;
            case 1:
                UnlockAchievement(SteamAchievementApi.Complete_Character2);
                break;
            case 2:
                UnlockAchievement(SteamAchievementApi.Complete_Character3);
                break;
        }
    }

    /// <summary> 通關通關成就 </summary>
    public void RequestStageAchievement(ScenesBuildData scene)
    {
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