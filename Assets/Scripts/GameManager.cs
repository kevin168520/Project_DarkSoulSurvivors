using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// GameManager 管理特定對象。每個關卡都會存在一個 GameManager 需要優先於其他腳本。
/// </summary>    
public class GameManager : MonoBehaviour
{
    /// <summary>
    ///  GameManager 靜態物件。從外部要讀取內部變數使用如下:
    ///   GameManager.instance.player;
    /// </summary>
    public static GameManager instance; 
    public static CharacterScriptable character;
    public GameObject player; // 玩家
    public CharacterScriptable _playerData; // 玩家資料
    [HideInInspector] public CharacterScriptable playerData { get{
      if(_playerData == null) _playerData = character;
      return _playerData;
    }}

    Transform _playerTransform; // 玩家座標
    [HideInInspector] public Transform playerTransform { get{
      if(_playerTransform == null) _playerTransform = player.transform;
      return _playerTransform;
    }}
    CharacterScript _playerCharacter; // 玩家狀態
    [HideInInspector] public CharacterScript playerCharacter { get{
      if(_playerCharacter == null) {
        CharacterScript[] cs = player.transform.GetComponentsInChildren<CharacterScript>();
        if(cs.Length > 0)_playerCharacter = cs[0];
      }
      return _playerCharacter;
    }}
    IDirection _playerDirection; // 玩家方向
    [HideInInspector] public IDirection playerDirection { get{
      if(_playerDirection == null) {
        _playerDirection = player.transform.GetComponent<IDirection>();
      }
      return _playerDirection;
    }}

    public bool isPause {get => Time.timeScale == 0;} // 玩家
    
    [SerializeField] private GameObject gameOverPanel; // 遊戲結束UI
    [SerializeField] private GameObject gameCompletePanel; // 遊戲通關UI
    [SerializeField] private GameObject gamePauseMenu; // 遊戲暫停UI

    [SerializeField] private Button btnReturnToGame; // 回到遊戲繼續進行
    [SerializeField] private Button btnSetting; // 尚無功能
    [SerializeField] private Button btnBackToMenu; // 回到LoginScene

    private bool bGameOver;

    // 設置靜態指向自己
    void Awake() {

      instance = this;
    }

    void Start() 
    {
        BtnCtrlGameScene();
    }

     void Update()
    {
        GameOverEvent(bGameOver);
    }

    // 暫停
    public void PauseGame(){
      Time.timeScale = 0f;
    }

    // 解除暫停
    public void UnPauseGame(){
      Time.timeScale = 1f;
    }


    // 遊戲結束
    public void GameOver(){
      PauseGame();
      gameOverPanel.SetActive(true);
      bGameOver = true;
    }


    // 遊戲通關
    public void GameComplete(){
      PauseGame();
      gameCompletePanel.SetActive(true);
      bGameOver = true;
    }

    private void GameOverEvent(bool bGameOverKey)
    {
        if (bGameOverKey)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManagerScript.inst.EndGameSceneAction();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
            gamePauseMenu.SetActive(true);
        }
    }

    /// <summary>
    /// 遊戲介面按鈕控制 BtnCtrlGameScene()
    /// </summary>
    private void BtnCtrlGameScene()
    {
        btnReturnToGame.onClick.AddListener(delegate ()
        {
            gamePauseMenu.SetActive(false);
            UnPauseGame();
        });

        btnSetting.onClick.AddListener(delegate ()
        {
            Debug.Log("Not Finish");
        });

        btnBackToMenu.onClick.AddListener(delegate ()
        {
            gamePauseMenu.SetActive(false);
            UnPauseGame();
            SceneManagerScript.inst.EndGameSceneAction();
        });
    }
}
