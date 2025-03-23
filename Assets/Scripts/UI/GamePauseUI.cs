using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    public GameObject gameOverPanel; // 遊戲結束UI
    public GameObject gameCompletePanel; // 遊戲通關UI
    public GameObject gamePauseMenu; // 遊戲暫停UI
    public List<Image> gamePauseWeaponsItems; // 玩家所有武器圖片位置

    [SerializeField] private Button btnReturnToGame; // 回到遊戲繼續進行
    [SerializeField] private Button btnSetting; // 尚無功能
    [SerializeField] private Button btnBackToMenu; // 回到LoginScene

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 遊戲介面按鈕控制 BtnCtrlGameScene()
    /// </summary>
    public void BtnCtrlGameScene()
    {
        btnReturnToGame.onClick.AddListener(delegate ()
        {
            gamePauseMenu.SetActive(false);
            GameManager.instance.UnPauseGame();
        });

        btnSetting.onClick.AddListener(delegate ()
        {
            Debug.Log("Not Finish");
        });

        btnBackToMenu.onClick.AddListener(delegate ()
        {
            gamePauseMenu.SetActive(false);
            GameManager.instance.SummaryEvent();
        });
    }
    
    public void SetweaponIcons(params Sprite[] weaponIcons) {
        for(int i = 0; i<gamePauseWeaponsItems.Count; i++) {
          if(i < weaponIcons.Length){
            gamePauseWeaponsItems[i].sprite = weaponIcons[i];
            gamePauseWeaponsItems[i].gameObject.SetActive(true);
          } else {
            gamePauseWeaponsItems[i].gameObject.SetActive(false);
          }
        }
    }
}
