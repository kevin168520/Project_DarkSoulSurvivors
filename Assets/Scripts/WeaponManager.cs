using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    Transform playerTransform => GameManager.instance.playerTransform; // 玩家座標資料
    CharacterScriptable playerData => GameManager.instance.playerData; // 角色資料
    // [SerializeField] WeaponBase weaponPrefab;
    [SerializeField] List<WeaponBase> equipWeapon = new List<WeaponBase>();
    
    void Start()
    {
        WeaponBase startingWeapon = Instantiate(playerData.startingWeapon).GetComponent<WeaponBase>(); // 起始武器

        AddWeapon(startingWeapon);
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
}
