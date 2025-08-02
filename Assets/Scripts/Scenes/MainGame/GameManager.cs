using System;
using System.Collections;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;

/// <summary> 負責遊戲流程管控 暫停 勝利 失敗 跳轉其他場景 </summary>    
public class GameManager : ManagerMonoBase
{
    [Header("遊戲場景物件")]
    public GameObject gameOverPanel; // 遊戲結束UI
    public GameObject gameCompletePanel; // 遊戲通關UI
    bool bGameOver; // 遊戲結束判定
    public bool IsGameOver => bGameOver; // 遊戲結束判定

    void Start() {
        var level = (ScenesBuildData) UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        switch(level) {
            case ScenesBuildData.MainGameLevel_1:
            AudioGlobalManager.PlayBGM(enAudioBgmData.MainGameLevel1_BGM);
            break;
            case ScenesBuildData.MainGameLevel_2:
            AudioGlobalManager.PlayBGM(enAudioBgmData.MainGameLevel2_BGM);
            break;
        }
    }
  
    void Update() {
        if (bGameOver && Input.GetKeyDown(KeyCode.Return)) {
            GameEnd();
        }

        if (Input.GetKeyDown(KeyCode.F1)){
            Debug.Log("SteamAchievementId CharaA_Stage1 Success - Start to Unlock Achievement");
            AchievementGlobalManager.SteamAchievementUnlock(eSteamAchievementApi.CharaA_Stage1);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Debug.Log("SteamAchievementId CharaA_Stage2 Success - Start to Unlock Achievement");
            AchievementGlobalManager.SteamAchievementUnlock(eSteamAchievementApi.CharaA_Stage2);
        }

        if (Input.GetKeyDown(KeyCode.F10))
        {
            bool achieved;
            SteamUserStats.GetAchievement(eSteamAchievementApi.CharaA_Stage1.ToString(), out achieved);
            Debug.Log($"Achievement already unlocked? {achieved}");
        }
    }

    // 暫停
    public void PauseGame() {
        Time.timeScale = 0f;
    }

    // 解除暫停
    public void UnPauseGame() {
        Time.timeScale = 1f;
    }


    // 遊戲結束
    public void GameOver() {
        PauseGame();
        gameOverPanel.SetActive(true);
        bGameOver = true;
    }

    // 遊戲結算
    public void GameEnd() {
        UnPauseGame();
        CalcSummaryData();
        SceneGlobalManager.LoadSummaryScene();
    }

    // 遊戲通關
    public void GameComplete() {
        PauseGame();
        gameCompletePanel.SetActive(true);
        bGameOver = true;
    }

    // 結算成績
    void CalcSummaryData() {
        // 角色結算
        DataGlobalManager._summaryCharacter = PlayerManager.Player.characterData.showImage;
        // 武器結算
        List<SummaryScoreManager.ScoreSummary> weaponSummary = new List<SummaryScoreManager.ScoreSummary>();
            foreach(var weapon in WeaponManager.GetWeapons()){
            SummaryScoreManager.ScoreSummary item = new SummaryScoreManager.ScoreSummary();
            item.weaponIcon = weapon.weaponIcon;
            item.weaponName = weapon.weaponName;
            item.weaponScore = weapon.totalDamage.ToString();
            weaponSummary.Add(item);
            }
        DataGlobalManager._summaryWeapon = weaponSummary;
    }
}
