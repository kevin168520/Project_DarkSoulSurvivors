using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataGlobalManager : GlobalMonoBase<DataGlobalManager>
{
    [Header("使用者資料")]
    public UserStoreData _userData; // 使用者資料

    [Header("玩家商店資料")]
    public PlayerStoreData _playerData; // 商店資料
    
    [Header("玩家角色資料")]
    public CharacterScriptable _characterData; // 玩家資料
    
    [Header("遊戲結算資料")]
    public Sprite _summaryCharacter; // 角色圖片
    public List<SummaryScoreManager.ScoreSummary> _summaryWeapon; // 武器結算資料
    public int _summaryGold; // 金幣結算資料
}
