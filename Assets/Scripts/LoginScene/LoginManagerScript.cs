using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginManagerScript : MonoBehaviour
{
    public StartGameScript _startGameScript;
    public ShopItemScript _shopItemScript;
    //public PlayerDataSavingScript _playerDataSavingScript;

    [Header("LoaginMenu")]
    public GameObject objLoginMenu;
    public Button btnGameStart;
    public Button btnShop;
    public Button btnExit;

    [Header("ShopMenu")]
    public GameObject objShopMenu;
    public Button btnShopExit;

    [Header("CharacterMenu")]
    public GameObject objCharacterMenu;
    public Button btnCharacter1;
    public Button btnCharacter2;
    public Button btnBackToState;

    [Header("GameStartMenu")]
    public GameObject objGameStartMenu;
    public Button btnGameState1;
    public Button btnGameState2;
    public Button btnBackToMenu;

    private string sGameSceneState;

    // Start is called before the first frame update
    void Start()
    {
        initial();
        BtnCtrlLoginMenu();
        BtnCtrlShopMenu();
        BtnCtrlCharacterMenu();
        BtnCtrlGameStartMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void initial()
    {
        sGameSceneState = "";
        objGameStartMenu.SetActive(false);
    }

    private void BtnCtrlLoginMenu()
    {
        btnGameStart.onClick.AddListener(delegate ()
        {
            objCharacterMenu.SetActive(true);
        });

        btnShop.onClick.AddListener(delegate ()
        {
            _shopItemScript.PlayerShopStatusLoading();
            objShopMenu.SetActive(true);
        });

        btnExit.onClick.AddListener(delegate ()
        {
            ExitGame();
        });
    }

    /// <summary>
    /// 遊戲角色選擇 BtnCtrlCharacterMenu()
    /// </summary>
    private void BtnCtrlCharacterMenu()
    {
        btnCharacter1.onClick.AddListener(delegate ()
        {
            objGameStartMenu.SetActive(true);
            _startGameScript.SetCharacter(0);
        });

        btnCharacter2.onClick.AddListener(delegate ()
        {
            objGameStartMenu.SetActive(true);
            _startGameScript.SetCharacter(1);
        });

        btnBackToState.onClick.AddListener(delegate ()
        {
            objCharacterMenu.SetActive(false);
        });
    }

    /// <summary>
    /// 遊戲場景切換 BtnCtrlGameStartMenu()
    /// </summary>
    private void BtnCtrlGameStartMenu()
    {
        btnGameState1.onClick.AddListener(delegate ()
        {
            sGameSceneState = "MainGameLevel_1"; //GameState1需修改為對應的遊戲場景1的名稱
            _startGameScript.StartGame(sGameSceneState);
            Debug.Log(sGameSceneState);
        });

        btnGameState2.onClick.AddListener(delegate ()
        {
            sGameSceneState = "MainGameLevel_2"; //GameState2需修改為對應的遊戲場景2的名稱
            _startGameScript.StartGame(sGameSceneState);
            Debug.Log(sGameSceneState);
        });

        btnBackToMenu.onClick.AddListener(delegate ()
        {
            objGameStartMenu.SetActive(false);
            sGameSceneState = "";
            Debug.Log(sGameSceneState);
        });
    }

    /// <summary>
    /// 商店介面 BtnCtrlShopMenu()
    /// </summary>
    private void BtnCtrlShopMenu()
    {
        btnShopExit.onClick.AddListener(delegate ()
        {
            _shopItemScript.PlayerShopStatusSaving();
            objShopMenu.SetActive(false);
        });
    }

    /// <summary>
    /// 離開遊戲 ExitGame()
    /// </summary>
    private void ExitGame()
    {
        _shopItemScript.PlayerShopStatusSaving();
    }
}
