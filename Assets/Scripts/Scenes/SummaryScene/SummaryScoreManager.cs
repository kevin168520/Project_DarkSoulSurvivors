using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummaryScoreManager : MonoBehaviour
{
    [SerializeField] SummaryScoreUI summaryScoreUI;
    Sprite characterImage => DataGlobalManager.inst._summaryCharacter; // 角色圖片
    List<ScoreSummary> weaponSummary => DataGlobalManager.inst._summaryWeapon; // 武器結算資料

    void Start() {

        AudioGlobalManager.inst.PlayBGM(enAudioDataBGM.SummaryScene_BGM);

        // InitSummaryScoreUI()
        if (characterImage != null){
          summaryScoreUI.characterImage = characterImage;
          summaryScoreUI.backToMenuButton = BackToMenuButton; 
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
        SceneGlobalManager.inst.BackToMenuSceneAction();
    }
    
    [System.Serializable]
    public class ScoreSummary {
        public Sprite weaponIcon;
        public string weaponName;
        public string weaponScore;
    }
}
