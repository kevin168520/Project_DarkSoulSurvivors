using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneManager : ManagerMonoBase
{
    enum enUIPaneltate {
        Start, Shop, Character, Stage
    }
    public ShopItemManager _shopItemManager;
    public StartSceneUI _startSceneUI;
    
    [Header("CharacterData")]
    public CharacterDatabaseScriptable _characterDatabase;
    CharacterScriptable _character { // 跨場景用角色資料
        get => DataGlobalManager._characterData;
        set => DataGlobalManager._characterData = value;
    }

    private void Awake()
    {
        AudioGlobalManager.inst.PlayBGM(enAudioDataBGM.StartScene_BGM);
    }

    void Start()
    {
        initial();
        BtnCtrlLoginMenu();
        BtnCtrlCharacterMenu();
        BtnCtrlGameStartMenu();
    }

    private void initial()
    {
        // 更新初始畫面
        OnUIPanelRenew(enUIPaneltate.Start);
        // 註冊玩家商店離開 按鈕
        _shopItemManager.ShopExitOnClick(() => OnUIPanelRenew(enUIPaneltate.Start));
    }

    /// <summary>遊戲開始選擇 BtnCtrlLoginMenu()</summary>
    private void BtnCtrlLoginMenu()
    {
        // Play 按鈕
        _startSceneUI.btnGameStartOnClick = () => OnUIPanelRenew(enUIPaneltate.Character);
        // Shop 按鈕
        _startSceneUI.btnShopOnClick = () => OnShopGame();
        // Exit 按鈕
        _startSceneUI.btnExitOnClick = () => OnExitGame();
    }

    /// <summary>遊戲角色選擇 BtnCtrlCharacterMenu()</summary>
    private void BtnCtrlCharacterMenu()
    {
        // 角色 1 按鈕
        _startSceneUI.btnCharacter1OnClick = () => OnCharacterSelect(1);
        // 角色 2 按鈕
        _startSceneUI.btnCharacter2OnClick = () => OnCharacterSelect(2);
        // Back 按鈕
        _startSceneUI.btnBackToStateOnClick = () => OnUIPanelRenew(enUIPaneltate.Start);
    }

    /// <summary>遊戲場景切換 BtnCtrlGameStartMenu()</summary>
    private void BtnCtrlGameStartMenu()
    {
        // State1 按鈕
        _startSceneUI.btnGameState1OnClick = () => OnStageSelect(ScenesBuildData.MainGameLevel_1);
        // State2 按鈕
        _startSceneUI.btnGameState2OnClick = () => OnStageSelect(ScenesBuildData.MainGameLevel_2);
        // Back 按鈕
        _startSceneUI.btnBackToMenuOnClick = () => OnUIPanelRenew(enUIPaneltate.Character);
    }

    /// <summary>選擇角色</summary>
    void OnCharacterSelect(int index) {
        _character = _characterDatabase.Search(index);
        OnUIPanelRenew(enUIPaneltate.Stage);

        Debug.Log($"OnCharacterSelect({index})");
    }

    /// <summary>選擇關卡</summary>
    void OnStageSelect(ScenesBuildData stageScene) {
        SceneGlobalManager.LoadMainGameScene(stageScene);

        Debug.Log($"OnStateSelect({stageScene.ToString()})");
    }

    /// <summary>開啟商店</summary>
    void OnShopGame() {
        _shopItemManager.AbiDesRenew();
        OnUIPanelRenew(enUIPaneltate.Shop);
    }

    /// <summary>離開遊戲</summary>
    void OnExitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        _shopItemManager.PlayerShopStatusSaving();
        Application.Quit();
#endif
    }

    /// <summary>UI面板 顯示</summary>
    void OnUIPanelRenew(enUIPaneltate state) {
        // 顯示對應的 UI 面板
        _startSceneUI.objLoginMenuShow = state == enUIPaneltate.Start;
        _shopItemManager.UIPanelShow(state == enUIPaneltate.Shop);
        _startSceneUI.objCharacterMenuShow = state == enUIPaneltate.Character;
        _startSceneUI.objGameStartMenuShow = state == enUIPaneltate.Stage;
    }
}