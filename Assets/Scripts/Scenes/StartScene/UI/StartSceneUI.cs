using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StartSceneUI : MonoBehaviour
{
    [Header("LoaginMenu")]
    [SerializeField] GameObject objLoginMenu;
    public bool objLoginMenuShow {set => objLoginMenu.SetActive(value);}
    [SerializeField] Button btnGameStart;
    public UnityAction btnGameStartOnClick {set => btnGameStart.onClick.AddListener(value);}
    [SerializeField] Button btnShop;
    public UnityAction btnShopOnClick {set => btnShop.onClick.AddListener(value);}
    [SerializeField] Button btnExit;
    public UnityAction btnExitOnClick {set => btnExit.onClick.AddListener(value);}

    [Header("CharacterMenu")]
    [SerializeField] GameObject objCharacterMenu;
    public bool objCharacterMenuShow {set => objCharacterMenu.SetActive(value);}
    [SerializeField] Button btnCharacter1;
    public UnityAction btnCharacter1OnClick {set => btnCharacter1.onClick.AddListener(value);}
    [SerializeField] Button btnCharacter2;
    public UnityAction btnCharacter2OnClick {set => btnCharacter2.onClick.AddListener(value);}
    [SerializeField] Button btnCharacter3;
    public UnityAction btnCharacter3OnClick {set => btnCharacter3.onClick.AddListener(value);}
    [SerializeField] Button btnBackToState;
    public UnityAction btnBackToStateOnClick {set => btnBackToState.onClick.AddListener(value);}

    [Header("GameStartMenu")]
    [SerializeField] GameObject objGameStartMenu;
    public bool objGameStartMenuShow {set => objGameStartMenu.SetActive(value);}
    [SerializeField] Button btnGameState1;
    public UnityAction btnGameState1OnClick {set => btnGameState1.onClick.AddListener(value);}
    [SerializeField] Button btnGameState2;
    public UnityAction btnGameState2OnClick {set => btnGameState2.onClick.AddListener(value);}
    [SerializeField] Button btnGameState3;
    public UnityAction btnGameState3OnClick {set => btnGameState3.onClick.AddListener(value);}
    [SerializeField] Button btnBackToMenu;
    public UnityAction btnBackToMenuOnClick {set => btnBackToMenu.onClick.AddListener(value);}
}
