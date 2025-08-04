using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummaryScoreManager : ManagerMonoBase
{
    [SerializeField] SummaryScoreUI summaryScoreUI;
    Sprite characterImage => DataGlobalManager._summaryCharacter; // 角色圖片
    List<ScoreSummary> weaponSummary => DataGlobalManager._summaryWeapon; // 武器結算資料

    void Start() {

        AudioGlobalManager.PlayBGM(enAudioBgmData.SummarySceneWin_BGM);

        // InitSummaryScoreUI()
        if(characterImage != null){
          summaryScoreUI.CharacterImage = characterImage;
          summaryScoreUI.BackToMenuButtonOnClick = BackToMenuButton; 
        }
        // InitWeaponSummary()
        if(weaponSummary != null){
            foreach(var item in weaponSummary) {
                summaryScoreUI.AddDetailScrollItem(
                    item.weaponIcon,
                    item.weaponName,
                    item.weaponScore
                    );
            }
        }
    }
    
    void BackToMenuButton() {
        SceneGlobalManager.LoadStartScene();
    }
    
    [System.Serializable]
    public class ScoreSummary {
        public Sprite weaponIcon;
        public string weaponName;
        public string weaponScore;
    }
}
