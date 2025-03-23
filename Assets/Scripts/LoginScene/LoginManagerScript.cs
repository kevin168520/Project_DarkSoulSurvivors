using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Data.SqlTypes;

public class LoginManagerScript : MonoBehaviour
{
    public StartGameScript _startGameScript;
    public ShopItemManagerScript _shopItemManagerScript;
    //public PlayerDataSavingScript _playerDataSavingScript;

    [Header("LoaginMenu")]
    public GameObject objLoginMenu;
    public Button btnGameStart;
    public Button btnShop;
    public Button btnExit;

    [Header("ShopMenu")]
    public GameObject objShopMenu;
    public GameObject objShopTip;

    public Button btnShopExit;
    public Button btnShopTipExit;
    public Button btnAbi1;
    public Button btnAbi2;
    public Button btnAbi3;
    public Button btnAbi4;
    public Button btnAbi5;
    public Button btnAbi6;
    public Button btnAbi7;
    public Button btnAbi8;
    public Button btnAbi9;

    public TextMeshProUGUI txtTip;
    public TextMeshProUGUI txtAbiDes1;
    public TextMeshProUGUI txtAbiDes2;
    public TextMeshProUGUI txtAbiDes3;
    public TextMeshProUGUI txtAbiDes4;
    public TextMeshProUGUI txtAbiDes5;
    public TextMeshProUGUI txtAbiDes6;
    public TextMeshProUGUI txtAbiDes7;
    public TextMeshProUGUI txtAbiDes8;
    public TextMeshProUGUI txtAbiDes9;
    public TextMeshProUGUI txtMoney;

    private int iMoney_HP;
    private int iMoney_ATK;
    private int iMoney_DEF;
    private int iMoney_moveSpeed;

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

        iMoney_ATK = 1000;
        iMoney_DEF = 1000;
        iMoney_HP = 1000;
        iMoney_moveSpeed = 1000;

        PlayerDataSavingScript.inst.PlayerDataLoading();
    }

    private void BtnCtrlLoginMenu()
    {
        btnGameStart.onClick.AddListener(delegate ()
        {
            objCharacterMenu.SetActive(true);
        });

        btnShop.onClick.AddListener(delegate ()
        {
            _shopItemManagerScript.PlayerShopStatusLoading();
            objShopMenu.SetActive(true);
            AbiDesRenew(enShopAbilityType.All.ToString());
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
            _shopItemManagerScript.PlayerShopStatusSaving();
            objShopMenu.SetActive(false);
        });

        btnShopTipExit.onClick.AddListener(delegate ()
        {
            objShopTip.SetActive(false);
        });

        btnAbi1.onClick.AddListener(delegate ()
        {
            if (_shopItemManagerScript.iPlayerMoney >= iMoney_HP)
            {
                _shopItemManagerScript.iPlayerMoney -= iMoney_HP;
                _shopItemManagerScript.iPlayerItemLevel_HP += 1;
                AbiDesRenew(enShopAbilityType.HP.ToString());
            }
            else
            {
                objShopTip.SetActive(true);
            }
        });

        btnAbi2.onClick.AddListener(delegate ()
        {
            if (_shopItemManagerScript.iPlayerMoney >= iMoney_ATK)
            {
                _shopItemManagerScript.iPlayerMoney -= iMoney_ATK;
                _shopItemManagerScript.iPlayerItemLevel_ATK += 1;
                AbiDesRenew(enShopAbilityType.ATK.ToString());
            }
            else
            {
                objShopTip.SetActive(true);
            }
        });

        btnAbi3.onClick.AddListener(delegate ()
        {
            if (_shopItemManagerScript.iPlayerMoney >= iMoney_DEF)
            {
                _shopItemManagerScript.iPlayerMoney -= iMoney_DEF;
                _shopItemManagerScript.iPlayerItemLevel_DEF += 1;
                AbiDesRenew(enShopAbilityType.DEF.ToString());
            }
            else
            {
                objShopTip.SetActive(true);
            }
        });

        btnAbi4.onClick.AddListener(delegate ()
        {
            if (_shopItemManagerScript.iPlayerMoney >= iMoney_moveSpeed)
            {
                _shopItemManagerScript.iPlayerMoney -= iMoney_moveSpeed;
                _shopItemManagerScript.iPlayerItemLevel_moveSpeed += 1;
                AbiDesRenew(enShopAbilityType.MoveSpeed.ToString());
            }
            else
            {
                objShopTip.SetActive(true);
            }
        });

        btnAbi5.onClick.AddListener(delegate ()
        {

        });

        btnAbi6.onClick.AddListener(delegate ()
        {

        });

        btnAbi7.onClick.AddListener(delegate ()
        {

        });

        btnAbi8.onClick.AddListener(delegate ()
        {

        });

        btnAbi9.onClick.AddListener(delegate ()
        {

        });
    }

    /// <summary>
    /// 離開遊戲 ExitGame()
    /// </summary>
    private void ExitGame()
    {
        PlayerDataSavingScript.inst.PlayerDataSaving(true);
    }

    /// <summary>
    /// 刷新商店技能說明的敘述 AbiDesRenew(string sAbiBtnClick)
    /// </summary>
    /// <param name="string sAbiBtnClick"></param>
    public void AbiDesRenew(string sAbiBtnClick)
    {
        string sMoney = "Money : " + _shopItemManagerScript.iPlayerMoney;
        string sTxtHP = "Money : " + iMoney_HP + "\n" + "HP Level: " + _shopItemManagerScript.iPlayerItemLevel_HP;
        string sTxtATK = "Money : " + iMoney_ATK + "\n" + "ATK Level: " + _shopItemManagerScript.iPlayerItemLevel_ATK;
        string sTxtDEF = "Money : " + iMoney_DEF + "\n" + "DEF Level : " + _shopItemManagerScript.iPlayerItemLevel_DEF;
        string sTxtMoveSpeed = "Money : " + iMoney_moveSpeed + "\n" + "MoveSpeed Level : " + _shopItemManagerScript.iPlayerItemLevel_moveSpeed;

        switch (sAbiBtnClick)
        {
            case "All":
                txtMoney.text = sMoney;
                txtAbiDes1.text = sTxtHP;
                txtAbiDes2.text = sTxtATK;
                txtAbiDes3.text = sTxtDEF;
                txtAbiDes4.text = sTxtMoveSpeed;
                break;

            case "HP":
                txtMoney.text = sMoney;
                txtAbiDes1.text = sTxtHP;
                break;

            case "ATK":
                txtMoney.text = sMoney;
                txtAbiDes2.text = sTxtATK;
                break;

            case "DEF":
                txtMoney.text = sMoney;
                txtAbiDes3.text = sTxtDEF;
                break;

            case "MoveSpeed":
                txtMoney.text = sMoney;
                txtAbiDes4.text = sTxtMoveSpeed;
                break;
        }
    }

    enum enShopAbilityType
    {
        All, Money, HP, ATK, DEF, MoveSpeed
    }
}