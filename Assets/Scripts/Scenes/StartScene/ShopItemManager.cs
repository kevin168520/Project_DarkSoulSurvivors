using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class ShopItemManager : ManagerMonoBase
{
    public enum enShopAbilityType
    {
        HP, ATK, DEF, MoveSpeed
    }

    [SerializeField] ShopMenuUI _shopMenuUI;
    
    [Header("ShopItemSell")]
    public int iMoney_HP;
    public int iMoney_ATK;
    public int iMoney_DEF;
    public int iMoney_moveSpeed;

    int iPlayer_Money { 
        get => DataGlobalManager._playerData.iPlayer_Money; 
        set => DataGlobalManager._playerData.iPlayer_Money = value; 
    }
    int iPlayerItemLevel_HP { 
        get => DataGlobalManager._playerData.iPlayerItemLevel_HP; 
        set => DataGlobalManager._playerData.iPlayerItemLevel_HP = value; 
    }
    int iPlayerItemLevel_ATK { 
        get => DataGlobalManager._playerData.iPlayerItemLevel_ATK; 
        set => DataGlobalManager._playerData.iPlayerItemLevel_ATK = value; 
    }
    int iPlayerItemLevel_DEF { 
        get => DataGlobalManager._playerData.iPlayerItemLevel_DEF; 
        set => DataGlobalManager._playerData.iPlayerItemLevel_DEF = value; 
    }
    int iPlayerItemLevel_moveSpeed { 
        get => DataGlobalManager._playerData.iPlayerItemLevel_moveSpeed; 
        set => DataGlobalManager._playerData.iPlayerItemLevel_moveSpeed = value; 
    }
    PlayerStoreData _playerData { 
        get => DataGlobalManager._playerData; 
        set => DataGlobalManager._playerData = value; 
    }

    void Start()
    {
        // 初始玩家商店價格
        iMoney_ATK = 1000;
        iMoney_DEF = 1000;
        iMoney_HP = 1000;
        iMoney_moveSpeed = 1000;

        // 載入玩家商店紀錄
        PlayerShopStatusLoading();

        // 註冊 按鈕
        BtnCtrlShopMenu();
    }

    /// <summary>商店介面 BtnCtrlShopMenu()</summary>
    private void BtnCtrlShopMenu()
    {
        // 金錢提示 右上 X 按鈕
        _shopMenuUI.btnShopTipExitOnClick = () => _shopMenuUI.objShopTipShow = false;
        // HP 按鈕
        _shopMenuUI.btnAbi1OnClick = () => BuyAbiDes(enShopAbilityType.HP);
        // ATK 按鈕
        _shopMenuUI.btnAbi2OnClick = () => BuyAbiDes(enShopAbilityType.ATK);
        // DEF 按鈕
        _shopMenuUI.btnAbi3OnClick = () => BuyAbiDes(enShopAbilityType.DEF);
        // MoveSpeed 按鈕
        _shopMenuUI.btnAbi4OnClick = () => BuyAbiDes(enShopAbilityType.MoveSpeed);
    }

    /// <summary>
    /// 玩家的商店狀態儲存 PlayerShopStatusSaving()
    /// </summary>
    public void PlayerShopStatusSaving()
    {
        PlayerStoreData data = _playerData;
        StorageUtility.PlayerStoreData().Save(data);
        Debug.Log("PlayerShopStatusSaving Finish!" + iPlayer_Money);
    }

    /// <summary>
    /// 玩家的商店狀態載入 PlayerShopStatusLoading()
    /// </summary>
    public void PlayerShopStatusLoading()
    {
        PlayerStoreData data = new PlayerStoreData();
        data = StorageUtility.PlayerStoreData().Load();
        _playerData = data;

        Debug.Log("PlayerShopStatusLoading Finish!" + iPlayer_Money);
    }

    /// <summary>購買 商店道具</summary>
    public bool BuyAbiDes(enShopAbilityType ability){
        int price = ability switch
        {
            enShopAbilityType.HP => iMoney_HP,
            enShopAbilityType.ATK => iMoney_ATK,
            enShopAbilityType.DEF => iMoney_DEF,
            enShopAbilityType.MoveSpeed => iMoney_moveSpeed,
            _ => 0,
        };
        // 判定錢包可購買
        if(iPlayer_Money < price) {
          _shopMenuUI.objShopTipShow = true;
          return false;
        }

        // 錢包扣款
        iPlayer_Money -= price;
        _shopMenuUI.SetPlayerMoneyText(iPlayer_Money);
        // 更新商店紀錄
        switch (ability)
        {
          case enShopAbilityType.HP:
            iPlayerItemLevel_HP += 1;
            _shopMenuUI.SetAbiDesHPText(iMoney_HP, iPlayerItemLevel_HP);
            break;
          case enShopAbilityType.ATK:
            iPlayerItemLevel_ATK += 1;
            _shopMenuUI.SetAbiDesATKText(iMoney_ATK, iPlayerItemLevel_ATK);
            break;
          case enShopAbilityType.DEF:
            iPlayerItemLevel_DEF += 1;
            _shopMenuUI.SetAbiDesDEFText(iMoney_DEF, iPlayerItemLevel_DEF);
            break;
          case enShopAbilityType.MoveSpeed:
            iPlayerItemLevel_moveSpeed += 1;
            _shopMenuUI.SetAbiDesMoveSpeedText(iMoney_moveSpeed, iPlayerItemLevel_moveSpeed);
            break;
        }

        return true;
    }

    /// <summary>更新 商店資訊</summary>
    public void AbiDesRenew(){
        _shopMenuUI.SetPlayerMoneyText(iPlayer_Money);
        _shopMenuUI.SetAbiDesHPText(iMoney_HP, iPlayerItemLevel_HP);
        _shopMenuUI.SetAbiDesATKText(iMoney_ATK, iPlayerItemLevel_ATK);
        _shopMenuUI.SetAbiDesDEFText(iMoney_DEF, iPlayerItemLevel_DEF);
        _shopMenuUI.SetAbiDesMoveSpeedText(iMoney_moveSpeed, iPlayerItemLevel_moveSpeed);
    }

    /// <summary>UI 面板顯示</summary>
    public void UIPanelShow(bool b){
        _shopMenuUI.objShopMenuShow = b;
    }

    /// <summary>註冊 Exit 按鈕</summary>
    public void ShopExitOnClick(UnityAction e){
        _shopMenuUI.btnShopExitOnClick = () => {
          PlayerShopStatusSaving(); // 離開時自動記錄
          e.Invoke();
        };
    }

}