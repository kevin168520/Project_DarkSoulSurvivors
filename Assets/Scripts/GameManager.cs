using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject player; // 玩家

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

    public bool isPause {get => Time.timeScale == 0;} // 玩家
    
    [SerializeField] private GameObject gameOverPanel; // 遊戲結束UI

    // 設置靜態指向自己
    void Awake() {

      instance = this;
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
    }

}
