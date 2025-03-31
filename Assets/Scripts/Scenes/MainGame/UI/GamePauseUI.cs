using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] GameObject gamePauseMenu; // 遊戲暫停UI
    public bool gamePauseMenuShow {
      get => gamePauseMenu.activeSelf;
      set => gamePauseMenu.SetActive(value);}

    [Header("武器圖片")]
    [SerializeField] List<Image> imgWeaponsIcons;

    [Header("功能按鈕")]
    [SerializeField] Button btnReturnToGame; // 回到遊戲繼續進行
    public UnityAction btnReturnToGameOnClick {set => btnReturnToGame.onClick.AddListener(value);}
    [SerializeField]  Button btnSetting; // Setting 按鈕
    public UnityAction btnSettingOnClick {set => btnSetting.onClick.AddListener(value);}
    [SerializeField]  Button btnBackToMenu; // 回到LoginScene
    public UnityAction btnBackToMenuOnClick {set => btnBackToMenu.onClick.AddListener(value);}
    
    public void SetWeaponIcons(params Sprite[] weaponIcons) {
        for(int i = 0; i<imgWeaponsIcons.Count; i++) {
          if(i < weaponIcons.Length){
            imgWeaponsIcons[i].sprite = weaponIcons[i];
            imgWeaponsIcons[i].gameObject.SetActive(true);
          } else {
            imgWeaponsIcons[i].gameObject.SetActive(false);
          }
        }
    }
}
