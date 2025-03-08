using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Transform playerTransform => GameManager.instance.playerTransform; // 玩家座標資料
    CharacterScriptable playerData => GameManager.instance.playerData; // 角色資料
    CharacterScript playerCharacter => GameManager.instance.playerCharacter; // 玩家角色
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
