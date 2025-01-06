using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginManagerScript : MonoBehaviour
{
    public StartGameScript _startGameScript;

    public GameObject objLoginMenu;
    public Button btnGameStart;
    public Button btnShop;
    public Button btnExit;

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
            objGameStartMenu.SetActive(true);
        });

        btnShop.onClick.AddListener(delegate ()
        {

        });

        btnExit.onClick.AddListener(delegate ()
        {
            Application.Quit();
        });
    }

    /// <summary>
    /// 遊戲場景切換 BtnCtrlGameStartMenu()
    /// </summary>
    private void BtnCtrlGameStartMenu()
    {
        btnGameState1.onClick.AddListener(delegate ()
        {
            sGameSceneState = "GameState1"; //GameState1需修改為對應的遊戲場景1的名稱
            _startGameScript.StartGame(sGameSceneState);
            Debug.Log(sGameSceneState);
        });

        btnGameState2.onClick.AddListener(delegate ()
        {
            sGameSceneState = "GameState2"; //GameState2需修改為對應的遊戲場景1的名稱
            Debug.Log(sGameSceneState);
        });

        btnBackToMenu.onClick.AddListener(delegate ()
        {
            objGameStartMenu.SetActive(false);
            sGameSceneState = "";
            Debug.Log(sGameSceneState);
        });
    }
}
