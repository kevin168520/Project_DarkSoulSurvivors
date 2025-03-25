using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ShopItemManagerScript : MonoBehaviour
{
    [Header("ShopItemLevel")]
    public int iPlayerMoney;
    public int iPlayerItemLevel_HP;
    public int iPlayerItemLevel_ATK;
    public int iPlayerItemLevel_DEF;
    public int iPlayerItemLevel_moveSpeed;

    /// <summary>
    /// 玩家的商店狀態儲存 PlayerShopStatusSaving()
    /// </summary>
    public void PlayerShopStatusSaving()
    {
        DataGlobalManager.inst._playerData.iPlayer_Money = iPlayerMoney;
        DataGlobalManager.inst._playerData.iPlayerItemLevel_HP = iPlayerItemLevel_HP;
        DataGlobalManager.inst._playerData.iPlayerItemLevel_ATK = iPlayerItemLevel_ATK;
        DataGlobalManager.inst._playerData.iPlayerItemLevel_DEF = iPlayerItemLevel_DEF;
        DataGlobalManager.inst._playerData.iPlayerItemLevel_moveSpeed = iPlayerItemLevel_moveSpeed;

        DataGlobalManager.inst.PlayerDataSaving(false);
        Debug.Log("PlayerShopStatusSaving Finish!");
    }

    /// <summary>
    /// 玩家的商店狀態載入 PlayerShopStatusLoading()
    /// </summary>
    public void PlayerShopStatusLoading()
    {
        DataGlobalManager.inst.PlayerDataLoading();
        iPlayerMoney = DataGlobalManager.inst._playerData.iPlayer_Money;
        iPlayerItemLevel_HP = DataGlobalManager.inst._playerData.iPlayerItemLevel_HP;
        iPlayerItemLevel_ATK = DataGlobalManager.inst._playerData.iPlayerItemLevel_ATK;
        iPlayerItemLevel_DEF = DataGlobalManager.inst._playerData.iPlayerItemLevel_DEF;
        iPlayerItemLevel_moveSpeed = DataGlobalManager.inst._playerData.iPlayerItemLevel_moveSpeed;
        Debug.Log("PlayerShopStatusLoading Finish!" + iPlayerMoney + "/" + DataGlobalManager.inst._playerData.iPlayer_Money);
    }
}