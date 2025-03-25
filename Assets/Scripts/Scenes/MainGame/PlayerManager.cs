using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Transform playerTransform => GameManager.instance.playerTransform; // 玩家座標資料
    CharacterScriptable playerData => DataGlobalManager.inst._characterData; // 角色資料
    PlayerData playerStoreData => DataGlobalManager.inst._playerData; // 商店資料
    CharacterScript playerCharacter => GameManager.instance.playerCharacter; // 玩家角色
    [SerializeField] private PlayerStatUI playerStatUI; // 遊戲等級 UI
    void Start()
    {
        // 載入角色圖片
        GameObject spritePrefab = Instantiate(playerData.spritePrefab);
        spritePrefab.transform.position = playerTransform.position;
        spritePrefab.transform.parent = playerTransform;
        spritePrefab.SetActive(true);
        
        // 載入角色資料
        playerCharacter.maxHp = playerData.hp + playerStoreData.iPlayerItemLevel_HP;
        playerCharacter.def = playerData.def + playerStoreData.iPlayerItemLevel_DEF;
        playerCharacter.speedMult = playerData.speedMult + playerStoreData.iPlayerItemLevel_moveSpeed;
        playerCharacter.attackMult = playerData.attackMult;
        playerCharacter.currentHp = playerCharacter.maxHp;

        // 註冊角色監聽
        playerCharacter.dataChangeListener.AddListener(OnCharacterdataChange);
    }

    public Sprite GetCharacterImage() => playerData.showImage;

    public void OnCharacterdataChange(CharacterScript.StatType type){
      switch (type)
      {
        case CharacterScript.StatType.Level:
          playerStatUI.ExpLevel = playerCharacter.level;
          break;
        case CharacterScript.StatType.TotalExp:
          playerStatUI.ExpBar = (float)playerCharacter.totalExp / playerCharacter.levelUpExp;
          break;
        case CharacterScript.StatType.Def:
          break;
        case CharacterScript.StatType.SpeedMult:
          break;
        case CharacterScript.StatType.AttackMult:
          break;
        case CharacterScript.StatType.MaxHp:
        case CharacterScript.StatType.CurrentHp:
          playerStatUI.HpBar = (float)playerCharacter.currentHp / playerCharacter.maxHp;
          break;
        case CharacterScript.StatType.isDead:
          GameManager.instance.GameOver();
          break;
        case CharacterScript.StatType.Invincibility:
          break;
      }
    }
}
