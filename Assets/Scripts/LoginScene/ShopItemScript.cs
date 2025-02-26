using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemScript : MonoBehaviour
{
    public PlayerData _playerData;    //���a���
    public Button btnShopExit;
    //[SerializeField] private PlayerDataSavingScript _playerDataSavingScript;    //���a���

    [Header("ShopItemLevel")]
    public int iPlayerMoney;
    public int iPlayerItemLevel_HP;
    public int iPlayerItemLevel_ATK;
    public int iPlayerItemLevel_DEF;
    public int iPlayerItemLevel_moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _playerData = new PlayerData();

        //�p�G�s�ɦs�b�hŪ���s�ɪ���ơA�p�G���s�b�h���w�]��
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
    /// ���a���ө����A�x�s PlayerShopStatusSaving()
    /// </summary>
    public void PlayerShopStatusSaving()
    {
        _playerData.iPlayer_Money = iPlayerMoney;
        _playerData.iPlayerItemLevel_HP = iPlayerItemLevel_HP;
        _playerData.iPlayerItemLevel_ATK = iPlayerItemLevel_ATK;
        _playerData.iPlayerItemLevel_DEF = iPlayerItemLevel_DEF;
        _playerData.iPlayerItemLevel_moveSpeed = iPlayerItemLevel_moveSpeed;

        PlayerDataSavingScript.inst.PlayerDataSaving();
        Debug.Log("PlayerShopStatusSaving Finish!");
    }

    /// <summary>
    /// ���a���ө����A���J PlayerShopStatusLoading()
    /// </summary>
    public void PlayerShopStatusLoading()
    {
        PlayerDataSavingScript.inst.PlayerDataLoading();
        iPlayerMoney = _playerData.iPlayer_Money;
        iPlayerItemLevel_HP = _playerData.iPlayerItemLevel_HP;
        iPlayerItemLevel_ATK = _playerData.iPlayerItemLevel_ATK;
        iPlayerItemLevel_DEF = _playerData.iPlayerItemLevel_DEF;
        iPlayerItemLevel_moveSpeed = _playerData.iPlayerItemLevel_moveSpeed;
        Debug.Log("PlayerShopStatusLoading Finish!");
    }
}
