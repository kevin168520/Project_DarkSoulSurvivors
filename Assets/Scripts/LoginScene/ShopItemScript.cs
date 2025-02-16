using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ShopItemScript : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;    //���a���
    [SerializeField] private PlayerDataSavingScript _playerDataSavingScript;    //���a���

    [Header("ShopItemLevel")]
    public int iPlayerMoney;
    public int iPlayerItemLevel_HP;
    public int iPlayerItemLevel_ATK;
    public int iPlayerItemLevel_DEF;
    public int iPlayerItemLevel_moveSpeed;

    private void Awake()
    {
        //�p�G�s�ɦs�b�hŪ���s�ɪ���ơA�p�G���s�b�h���w�]��
        if (File.Exists(_playerDataSavingScript.sPlayerDataSavingPath))
        {
            iPlayerMoney = _playerData.iPlayer_Money;
            iPlayerItemLevel_HP = _playerData.iPlayerItemLevel_HP;
            iPlayerItemLevel_ATK = _playerData.iPlayerItemLevel_ATK;
            iPlayerItemLevel_DEF = _playerData.iPlayerItemLevel_DEF;
            iPlayerItemLevel_moveSpeed = _playerData.iPlayerItemLevel_moveSpeed;
            Debug.Log("PlayerDataSaving Loading Finish!");
        }
        else
        {
            iPlayerMoney = 0;
            iPlayerItemLevel_HP = 0;
            iPlayerItemLevel_ATK = 0;
            iPlayerItemLevel_DEF = 0;
            iPlayerItemLevel_moveSpeed = 0;
            Debug.Log("PlayerDataSaving not Found!");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
