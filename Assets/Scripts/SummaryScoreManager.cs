using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummaryScoreManager : MonoBehaviour
{
    [SerializeField] SummaryScoreUI summaryScoreUI;
    public Sprite characterImage; //要換掉
    public List<ScoreSummary> weaponSummary;

    void Start() {
        // InitSummaryScoreUI()
        summaryScoreUI.characterImage = characterImage;
        summaryScoreUI.backToMenuButton = BackToMenuButton; 
        
        // InitWeaponSummary()
        foreach(var item in weaponSummary) {
            summaryScoreUI.AddDetailScrollItem(
                item.weaponIcon,
                item.weaponName,
                item.weaponScore
          );
        }
    }
    
    void BackToMenuButton() {
        SceneManagerScript.inst.BackToMenuSceneAction();
    }
    
    [System.Serializable]
    public class ScoreSummary {
        public Sprite characterImage; //正式的
        public Sprite weaponIcon;
        public string weaponName;
        public string weaponScore;
    }
}
