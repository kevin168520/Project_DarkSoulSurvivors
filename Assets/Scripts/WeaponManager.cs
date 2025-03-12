using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    Transform playerTransform => GameManager.instance.playerTransform; // 玩家座標資料
    CharacterScriptable playerData => GameManager.instance.playerData; // 角色資料
    CharacterScript playerCharacter => GameManager.instance.playerCharacter; // 玩家角色
    [SerializeField] List<WeaponBase> equipWeapon = new List<WeaponBase>();
    
    void Start()
    {
        // 添加角色起始武器
        AddWeapon(playerData.startingWeapon);

        // 註冊角色監聽
        playerCharacter.dataChangeListener.AddListener(OnCharacterdataChange);
    }
    
    // 透過武器資料添加武器物件
    public void AddWeapon(WeaponScriptable weaponData){
      WeaponBase weapon = Instantiate<WeaponBase>(weaponData.weaponPrefab);
      weapon.SetWeaponData(weaponData);
      AddWeapon(weapon);
    }
    
    // 添加武器物件並移動玩家座標下
    public void AddWeapon(WeaponBase weapon){
      equipWeapon.Add(weapon);
      weapon.transform.parent = playerTransform;
      weapon.transform.position = Vector3.zero;
      weapon.gameObject.SetActive(true);
    }
    

    public void OnCharacterdataChange(CharacterScript.StatType type){
      switch (type)
      {
        case CharacterScript.StatType.Level:
          break;
        case CharacterScript.StatType.SpeedMult:
          break;
        case CharacterScript.StatType.AttackMult:
          break;
        case CharacterScript.StatType.isDead:
          break;
      }
    }
}
