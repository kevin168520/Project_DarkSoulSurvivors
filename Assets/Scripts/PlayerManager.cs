using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Transform playerTransform => GameManager.instance.playerTransform; // 玩家座標資料
    CharacterScriptable playerData => GameManager.instance.playerData; // 角色資料
    CharacterScript playerCharacter => GameManager.instance.playerCharacter; // 玩家角色
    [SerializeField] private PlayerStatUI playerLevelUpUI; // 遊戲等級 UI
    void Start()
    {
        // 載入角色圖片
        GameObject spritePrefab = Instantiate(playerData.spritePrefab);
        spritePrefab.transform.position = playerTransform.position;
        spritePrefab.transform.parent = playerTransform;
        spritePrefab.SetActive(true);
        
        // 載入角色資料
        playerCharacter.maxHp = playerData.hp;
        playerCharacter.def = playerData.def;
        playerCharacter.speedMult = playerData.speedMult;
        playerCharacter.attackMult = playerData.attackMult;
        playerCharacter.currentHp = playerData.hp;

        // 註冊角色監聽
        playerCharacter.dataChangeListener.AddListener(OnCharacterdataChange);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCharacterdataChange(CharacterScript.StatType type){
      switch (type)
      {
        case CharacterScript.StatType.Level:
          playerLevelUpUI.ExpLevel = playerCharacter.level;
          break;
        case CharacterScript.StatType.TotalExp:
          playerLevelUpUI.ExpBar = (float)playerCharacter.totalExp / playerCharacter.levelUpExp;
          break;
        case CharacterScript.StatType.MaxHp:
          break;
        case CharacterScript.StatType.Def:
          break;
        case CharacterScript.StatType.SpeedMult:
          break;
        case CharacterScript.StatType.AttackMult:
          break;
        case CharacterScript.StatType.CurrentHp:
          break;
        case CharacterScript.StatType.isDead:
          break;
        case CharacterScript.StatType.Invincibility:
          break;
      }
    }
}
