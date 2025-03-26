using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataGlobalManager : MonoBehaviour
{
    public static DataGlobalManager inst;
    
    
    [Header("玩家商店資料")]
    public PlayerStoreData _playerData; // 商店資料
    
    [Header("玩家角色資料")]
    public CharacterScriptable _characterData; // 玩家資料
    
    [Header("遊戲結算資料")]
    public Sprite _summaryCharacter; // 角色圖片
    public List<SummaryScoreManager.ScoreSummary> _summaryWeapon; // 武器結算資料

    private void Awake()
    {
        // 檢查是否已經有一個實例
        if (inst == null)
        {
            // 如果沒有實例，設置當前物件為實例並防止銷毀
            inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 如果已有實例，銷毀當前物件
            Destroy(this);
        }
    }

}