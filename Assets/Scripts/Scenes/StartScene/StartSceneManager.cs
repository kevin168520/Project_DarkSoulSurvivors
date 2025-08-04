using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneManager : ManagerMonoBase
{
    enum enUIPanelState {
        Start, Shop, Character, Stage, Credit
    }
    public ShopItemManager _shopItemManager;
    public StartSceneUI _startSceneUI;
    public OptionMenuUI _optionMenuUI;

    private enUIPanelState enNowPanel;
    
    [Header("CharacterData")]
    public CharacterDatabaseScriptable _characterDatabase;
    CharacterScriptable _character { // 跨場景用角色資料
        get => DataGlobalManager._characterData;
        set => DataGlobalManager._characterData = value;
    }

    void Start()
    {
        AudioGlobalManager.PlayBGM(enAudioBgmData.StartScene_BGM);
        initial();
        BtnCtrlLoginMenu();
        BtnCtrlCharacterMenu();
        BtnCtrlGameStartMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && enNowPanel != enUIPanelState.Start)
        {
            OnUIPanelRenew(enUIPanelState.Start);
        }
    }

    private void initial()
    {
        // 更新初始畫面
        OnUIPanelRenew(enUIPanelState.Start);
        // 註冊玩家商店離開 按鈕
        _shopItemManager.ShopExitOnClick(() => OnUIPanelRenew(enUIPanelState.Start));
    }

    /// <summary> 遊戲開始選擇 </summary>
    private void BtnCtrlLoginMenu()
    {
        // Play 按鈕
        _startSceneUI.btnGameStartOnClick = () => OnUIPanelRenew(enUIPanelState.Character);
        // Option 按鈕
        _startSceneUI.btnOptionOnClick = () => OnOptionSelect();
        // Credit按鈕
        _startSceneUI.btnCreditOnClick = () => OnCreditSelect();
        // Shop 按鈕
        _startSceneUI.btnShopOnClick = () => OnShopGame();
        // Exit 按鈕
        _startSceneUI.btnExitOnClick = () => OnExitGame();
    }

    /// <summary> 遊戲角色選擇 </summary>
    private void BtnCtrlCharacterMenu()
    {
        // 角色 1 按鈕
        _startSceneUI.btnCharacter1OnClick = () => OnCharacterSelect(1);
        // 角色 2 按鈕
        _startSceneUI.btnCharacter2OnClick = () => OnCharacterSelect(2);
        // 角色 3 按鈕
        _startSceneUI.btnCharacter3OnClick = () => OnCharacterSelect(3);
        // Back 按鈕
        _startSceneUI.btnBackToStateOnClick = () => OnUIPanelRenew(enUIPanelState.Start);
    }

    /// <summary> 遊戲場景切換 </summary>
    private void BtnCtrlGameStartMenu()
    {
        // State1 按鈕
        _startSceneUI.btnGameState1OnClick = () => OnStageSelect(ScenesBuildData.MainGameLevel_1);
        // State2 按鈕
        _startSceneUI.btnGameState2OnClick = () => OnStageSelect(ScenesBuildData.MainGameLevel_2);
        // State3 按鈕
        _startSceneUI.btnGameState3OnClick = () => OnStageSelect(ScenesBuildData.MainGameLevel_3);
        // Back 按鈕
        _startSceneUI.btnBackToMenuOnClick = () => OnUIPanelRenew(enUIPanelState.Character);
    }

    /// <summary> 選擇設定 </summary>
    void OnOptionSelect()
    {
        //直接開啟 OptionMenu 其餘Option相關動作由OptionMenuManager控制
        _optionMenuUI.objOptionMenuShow = true;
    }

    /// <summary> 選擇製作者名單 </summary>
    void OnCreditSelect()
    {
        OnUIPanelRenew(enUIPanelState.Credit);
    }

    /// <summary> 選擇角色 </summary>
    void OnCharacterSelect(int index) {
        _character = _characterDatabase.Search(index);
        OnUIPanelRenew(enUIPanelState.Stage);

        Debug.Log($"OnCharacterSelect({index})");
    }

    /// <summary> 選擇關卡 </summary>
    void OnStageSelect(ScenesBuildData stageScene) {
        SceneGlobalManager.LoadMainGameScene(stageScene);

        Debug.Log($"OnStateSelect({stageScene.ToString()})");
    }

    /// <summary> 開啟商店 </summary>
    void OnShopGame() {
        _shopItemManager.AbiDesRenew();
        OnUIPanelRenew(enUIPanelState.Shop);
    }

    /// <summary> 離開遊戲 </summary>
    void OnExitGame() {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        _shopItemManager.PlayerShopStatusSaving();
        Application.Quit();
    #endif
    }

    /// <summary> UI面板 顯示 </summary>
    void OnUIPanelRenew(enUIPanelState state) {
        // 紀錄當前頁面，Option頁面例外
        enNowPanel = state;
        // 顯示對應的 UI 面板
        _startSceneUI.objLoginMenuShow = state == enUIPanelState.Start;
        _startSceneUI.objCreditMenuShow = state == enUIPanelState.Credit;
        _shopItemManager.UIPanelShow(state == enUIPanelState.Shop);
        _startSceneUI.objCharacterMenuShow = state == enUIPanelState.Character;
        _startSceneUI.objGameStartMenuShow = state == enUIPanelState.Stage;
    }
}