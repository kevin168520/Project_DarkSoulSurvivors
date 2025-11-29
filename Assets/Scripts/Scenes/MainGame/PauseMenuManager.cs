using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseMenuManager : ManagerMonoBase
{
  // 內部成員
  [SerializeField] GamePauseUI _gamePauseUI;
  [SerializeField] OptionMenuUI _optionMenuUI;

  void Start() {
    // 返回遊戲 按鈕
    _gamePauseUI.btnReturnToGameOnClick = () =>{
      ShowPanelUI(false);
      GameManager.UnPauseGame();
    };
    // 遊戲設定 按鈕
    _gamePauseUI.btnSettingOnClick = () =>{
        //直接開啟 OptionMenu 其餘Option相關動作由OptionMenuManager控制
        _optionMenuUI.objOptionMenuShow = true;
    };
    // 返回開始 按鈕
    _gamePauseUI.btnBackToMenuOnClick = () =>{
      GameManager.GameEnd();
    };
  }

  void Update() {
    if (GameManager.IsGameOver) return;

    // 鍵盤 ESC 控制 開啟暫停
    if (Input.GetKeyDown(KeyCode.Escape)) {
      if (_gamePauseUI.gamePauseMenuShow) {
        ShowPanelUI(false);
        GameManager.UnPauseGame();
      } else {
        GameManager.PauseGame();
        ShowPanelUI(true);
        ActorPanelManager.UpdateUI();
      }
    }
  }

  /// <summary> 顯示 UI面板</summary>
  void ShowPanelUI(bool b){
      _gamePauseUI.gamePauseMenuShow = b;
  }
}
