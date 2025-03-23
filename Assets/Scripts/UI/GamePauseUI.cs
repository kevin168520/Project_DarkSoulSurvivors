using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel; // 遊戲結束UI
    [SerializeField] private GameObject gameCompletePanel; // 遊戲通關UI
    [SerializeField] private GameObject gamePauseMenu; // 遊戲暫停UI

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
    private void BtnCtrlGameScene()
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
            GameManager.instance.UnPauseGame();
            SceneManagerScript.inst.EndGameSceneAction();
        });
    }
}
