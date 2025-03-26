using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopMenuUI : MonoBehaviour
{
    [Header("ShopMenu")]
    [SerializeField] GameObject objShopMenu;
    public bool objShopMenuShow {set => objShopMenu.SetActive(value);}
    [SerializeField] Button btnShopExit;
    public UnityAction btnShopExitOnClick {set => btnShopExit.onClick.AddListener(value);}
    [SerializeField] TextMeshProUGUI txtMoney;
    public string txtMoneyText {set => txtMoney.text = value;}
    
    [Header("ShopTip")]
    public GameObject objShopTip;
    public bool objShopTipShow {set => objShopTip.SetActive(value);}
    [SerializeField] Button btnShopTipExit;
    public UnityAction btnShopTipExitOnClick {set => btnShopTipExit.onClick.AddListener(value);}
    [SerializeField] TextMeshProUGUI txtTip;
    public string txtTipText {set => txtTip.text = value;}

    [Header("ShopItem")]
    [SerializeField] Button btnAbi1;
    public UnityAction btnAbi1OnClick {set => btnAbi1.onClick.AddListener(value);}
    [SerializeField] Button btnAbi2;
    public UnityAction btnAbi2OnClick {set => btnAbi2.onClick.AddListener(value);}
    [SerializeField] Button btnAbi3;
    public UnityAction btnAbi3OnClick {set => btnAbi3.onClick.AddListener(value);}
    [SerializeField] Button btnAbi4;
    public UnityAction btnAbi4OnClick {set => btnAbi4.onClick.AddListener(value);}
    [SerializeField] Button btnAbi5;
    public UnityAction btnAbi5OnClick {set => btnAbi5.onClick.AddListener(value);}
    [SerializeField] Button btnAbi6;
    public UnityAction btnAbi6OnClick {set => btnAbi6.onClick.AddListener(value);}
    [SerializeField] Button btnAbi7;
    public UnityAction btnAbi7OnClick {set => btnAbi7.onClick.AddListener(value);}
    [SerializeField] Button btnAbi8;
    public UnityAction btnAbi8OnClick {set => btnAbi8.onClick.AddListener(value);}
    [SerializeField] Button btnAbi9;
    public UnityAction btnAbi9OnClick {set => btnAbi9.onClick.AddListener(value);}
    [SerializeField] TextMeshProUGUI txtAbiDes1;
    public string txtAbiDes1Text {set => txtAbiDes1.text = value;}
    [SerializeField] TextMeshProUGUI txtAbiDes2;
    public string txtAbiDes2Text {set => txtAbiDes2.text = value;}
    [SerializeField] TextMeshProUGUI txtAbiDes3;
    public string txtAbiDes3Text {set => txtAbiDes3.text = value;}
    [SerializeField] TextMeshProUGUI txtAbiDes4;
    public string txtAbiDes4Text {set => txtAbiDes4.text = value;}
    [SerializeField] TextMeshProUGUI txtAbiDes5;
    public string txtAbiDes5Text {set => txtAbiDes5.text = value;}
    [SerializeField] TextMeshProUGUI txtAbiDes6;
    public string txtAbiDes6Text {set => txtAbiDes6.text = value;}
    [SerializeField] TextMeshProUGUI txtAbiDes7;
    public string txtAbiDes7Text {set => txtAbiDes7.text = value;}
    [SerializeField] TextMeshProUGUI txtAbiDes8;
    public string txtAbiDes8Text {set => txtAbiDes8.text = value;}
    [SerializeField] TextMeshProUGUI txtAbiDes9;
    public string txtAbiDes9Text {set => txtAbiDes9.text = value;}

    
    public void SetPlayerMoneyText(int money) => txtMoneyText = "Money : " + money;

    public void SetAbiDesHPText(int money, int level) => txtAbiDes1Text = "Money : " + money + "\n" + "HP Level: " + level;

    public void SetAbiDesATKText(int money, int level) => txtAbiDes2Text = "Money : " + money + "\n" + "ATK Level: " + level;

    public void SetAbiDesDEFText(int money, int level) => txtAbiDes3Text = "Money : " + money + "\n" + "DEF Level : " + level;

    public void SetAbiDesMoveSpeedText(int money, int level) => txtAbiDes4Text = "Money : " + money + "\n" + "MoveSpeed Level : " + level;

}
