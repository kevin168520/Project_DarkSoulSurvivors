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
    ///  GameManager.instance.player;
    /// </summary>
    public static GameManager instance; 
    public static CharacterScriptable character;

    [SerializeField] GamePauseUI _gamePauseUI;

    public GameObject player; // 玩家物件
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
    private bool bGameOver;

    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private PlayerManager playerManager;

    public Sprite characterImage { // 角色圖片
        set => PlayerDataSavingScript.inst._summaryCharacter = value;
    }
    public List<SummaryScoreManager.ScoreSummary> weaponSummary{ // 武器結算資料
        set => PlayerDataSavingScript.inst._summaryWeapon = value;
    }

    // 設置靜態指向自己
    void Awake() {

      instance = this;
    }

    void Start() 
    {
        _gamePauseUI.BtnCtrlGameScene();
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
        _gamePauseUI.gameOverPanel.SetActive(true);
        bGameOver = true;
    }

    // 遊戲通關
    public void GameComplete(){
        PauseGame();
        _gamePauseUI.gameCompletePanel.SetActive(true);
        bGameOver = true;
    }

    private void GameOverEvent(bool bGameOverKey)
    {
        if (bGameOverKey)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SummaryEvent();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
            InitPauseUI();
            _gamePauseUI.gamePauseMenu.SetActive(true);
        }
    }

    public void SummaryEvent()
    {
        UnPauseGame();
        CalcSummaryData();
        SceneManagerScript.inst.EndGameSceneAction();
    }
    private void InitPauseUI() {
        Sprite[] weaponIcon = new Sprite[weaponManager.GetWeapons().Count];
        for(int i =0; i<weaponIcon.Length; i++){
            weaponIcon[i] = weaponManager.GetWeapons()[i].weaponIcon;
        }
        _gamePauseUI.SetweaponIcons(weaponIcon);
    }

    private void CalcSummaryData() {
        // 角色結算
        this.characterImage = playerManager.GetCharacterImage();
        // 武器結算
        List<SummaryScoreManager.ScoreSummary> weaponSummary = new List<SummaryScoreManager.ScoreSummary>();
        foreach(var weapon in weaponManager.GetWeapons()){
            SummaryScoreManager.ScoreSummary item = new SummaryScoreManager.ScoreSummary();
            item.weaponIcon = weapon.weaponIcon;
            item.weaponName = weapon.weaponName;
            item.weaponScore = weapon.totalDamage.ToString();
            weaponSummary.Add(item);
        }
        this.weaponSummary = weaponSummary;
    }
}
