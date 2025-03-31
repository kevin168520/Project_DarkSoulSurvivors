using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary> 負責角色狀態與畫面等級 </summary>
public class PlayerManager : ManagerMonoBase
{
    UnityAction WeaponUpgrade => WeaponManager.InitWeaponUpgradeUI;
    [Header("玩家物件")]
    [SerializeField] private PlayerScript _player;
    public PlayerScript Player => _player;
    public static PlayerScript PLAYER; // 暫時用來對接 非 Manager 相關腳本
    [Header("玩家狀態 UI")]
    [SerializeField] private PlayerStatUI _playerStatUI;

    void Start()
    {
        // 載入角色資料
        CharacterScriptable characterData = DataGlobalManager._characterData;
        PlayerStoreData storeData = DataGlobalManager._playerData;
        Player.characterData = characterData;
        Player.storeData = storeData;
        PlayerManager.PLAYER = _player;

        // 載入角色圖片
        GameObject spritePrefab = Instantiate(characterData.spritePrefab);
        spritePrefab.transform.position = Player.trans.position;
        spritePrefab.transform.parent = Player.trans;
        spritePrefab.SetActive(true);
        
        // 載入角色資料
        Player.character.maxHp = characterData.hp + storeData.iPlayerItemLevel_HP;
        Player.character.def = characterData.def + storeData.iPlayerItemLevel_DEF;
        Player.character.speedMult = characterData.speedMult + storeData.iPlayerItemLevel_moveSpeed;
        Player.character.attackMult = characterData.attackMult;
        Player.character.currentHp = Player.character.maxHp;

        // 註冊角色監聽
        Player.character.dataChangeListener.AddListener(OnCharacterdataChange);
    } 

    public void OnCharacterdataChange(CharacterScript.StatType type){
      switch (type)
      {
        case CharacterScript.StatType.Level:
          _playerStatUI.ExpLevel = Player.character.level;
          WeaponUpgrade();
          break;
        case CharacterScript.StatType.TotalExp:
          _playerStatUI.ExpBar = (float)Player.character.totalExp / Player.character.levelUpExp;
          break;
        case CharacterScript.StatType.Def:
          break;
        case CharacterScript.StatType.SpeedMult:
          break;
        case CharacterScript.StatType.AttackMult:
          break;
        case CharacterScript.StatType.MaxHp:
        case CharacterScript.StatType.CurrentHp:
          _playerStatUI.HpBar = (float)Player.character.currentHp / Player.character.maxHp;
          break;
        case CharacterScript.StatType.isDead:
          GameManager.GameOver();
          break;
        case CharacterScript.StatType.Invincibility:
          break;
      }
    }
}
