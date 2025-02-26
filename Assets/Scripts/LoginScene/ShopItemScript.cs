using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ShopItemScript : MonoBehaviour
{
    [Header("ShopItemLevel")]
    public int iPlayerMoney;
    public int iPlayerItemLevel_HP;
    public int iPlayerItemLevel_ATK;
    public int iPlayerItemLevel_DEF;
    public int iPlayerItemLevel_moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //如果存檔存在則讀取存檔的資料，如果不存在則為預設值
        if (File.Exists(PlayerDataSavingScript.inst.sPlayerDataSavingPath))
        {
            PlayerDataSavingScript.inst.PlayerDataLoading();
            Debug.Log("Initial - PlayerDataSaving Loading Finish: " + PlayerDataSavingScript.inst.sPlayerDataSavingPath);
        }
        else
        {
            iPlayerMoney = 0;
            iPlayerItemLevel_HP = 0;
            iPlayerItemLevel_ATK = 0;
            iPlayerItemLevel_DEF = 0;
            iPlayerItemLevel_moveSpeed = 0;
            Debug.Log("Initial - PlayerDataSaving not Found: " + PlayerDataSavingScript.inst.sPlayerDataSavingPath);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 玩家的商店狀態儲存 PlayerShopStatusSaving(bool bGameExit)
    /// </summary>
    /// <param name="b"></param>
    public void PlayerShopStatusSaving(bool bGameExit)
    {
        PlayerDataSavingScript.inst._playerData.iPlayer_Money = iPlayerMoney;
        PlayerDataSavingScript.inst._playerData.iPlayerItemLevel_HP = iPlayerItemLevel_HP;
        PlayerDataSavingScript.inst._playerData.iPlayerItemLevel_ATK = iPlayerItemLevel_ATK;
        PlayerDataSavingScript.inst._playerData.iPlayerItemLevel_DEF = iPlayerItemLevel_DEF;
        PlayerDataSavingScript.inst._playerData.iPlayerItemLevel_moveSpeed = iPlayerItemLevel_moveSpeed;

        PlayerDataSavingScript.inst.PlayerDataSaving(bGameExit);
        Debug.Log("PlayerShopStatusSaving Finish! And bGameExit = " + bGameExit);
    }

    /// <summary>
    /// 玩家的商店狀態載入 PlayerShopStatusLoading()
    /// </summary>
    public void PlayerShopStatusLoading()
    {
        PlayerDataSavingScript.inst.PlayerDataLoading();
        iPlayerMoney = PlayerDataSavingScript.inst._playerData.iPlayer_Money;
        iPlayerItemLevel_HP = PlayerDataSavingScript.inst._playerData.iPlayerItemLevel_HP;
        iPlayerItemLevel_ATK = PlayerDataSavingScript.inst._playerData.iPlayerItemLevel_ATK;
        iPlayerItemLevel_DEF = PlayerDataSavingScript.inst._playerData.iPlayerItemLevel_DEF;
        iPlayerItemLevel_moveSpeed = PlayerDataSavingScript.inst._playerData.iPlayerItemLevel_moveSpeed;
        Debug.Log("PlayerShopStatusLoading Finish!");
    }
}
