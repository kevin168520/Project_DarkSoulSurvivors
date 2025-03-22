using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummaryScoreManager : MonoBehaviour
{
    [SerializeField] SummaryScoreUI summaryScoreUI;
    public Sprite characterImage;
    public List<WeaponSummary> weaponSummary;

    void Start() {
        // InitSummaryScoreUI()
        summaryScoreUI.characterImage = characterImage;
        summaryScoreUI.backToHomeButton = BackToHomeButton;
        
        // InitWeaponSummary()
        foreach(var item in weaponSummary) {
          summaryScoreUI.AddDetailScrollItem(
            item.weaponIcon,
            item.weaponName,
            item.weaponScore
          );
        }
    }
    
    void BackToHomeButton() {
        SceneManagerScript.inst.EndGameSceneAction();
    }
    
    [System.Serializable]
    public class WeaponSummary {
      public Sprite weaponIcon;
      public string weaponName;
      public string weaponScore;
    }
}
