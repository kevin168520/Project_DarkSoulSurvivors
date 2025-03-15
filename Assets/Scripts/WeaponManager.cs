using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    Transform playerTransform => GameManager.instance.playerTransform; // 玩家座標資料
    CharacterScriptable playerData => GameManager.instance.playerData; // 角色資料
    CharacterScript playerCharacter => GameManager.instance.playerCharacter; // 玩家角色
    [SerializeField] private WeaponUpgradeUI weaponUpgradeUI; // 玩家升級 UI
    [SerializeField] List<WeaponBase> equipWeapon = new List<WeaponBase>();
    [SerializeField] List<WeaponScriptable> upgradeWeapon = new List<WeaponScriptable>();
    [SerializeField] List<WeaponScriptable> allWeapon;
    
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
      weapon.transform.localPosition  = Vector3.zero;
      weapon.gameObject.SetActive(true);
    }

    public WeaponScriptable SearchWeapon(int number) {
      foreach(WeaponScriptable w in allWeapon){
        if(w.weaponNumber == number) return w;
      }

      Debug.LogWarning($"can't Search WeaponNumber {number}");
      return null;
    }

    public bool CheckUpgradeWeapon(WeaponScriptable weaponData, WeaponBase weapon) {
        return weaponData.weaponDatas.Count > (weapon.weaponLevel+1);
    }
    
    // 檢查可升級的武器列表
    public void CheckUpgradeWeapons(){
      upgradeWeapon = new List<WeaponScriptable>();
      List<WeaponScriptable> nonupgradeWeapon = new List<WeaponScriptable>();

      foreach(WeaponBase weapon in equipWeapon){
        WeaponScriptable weaponData = Instantiate(SearchWeapon(weapon.weaponNumber));
        if(CheckUpgradeWeapon(weaponData, weapon)) {
          weaponData.weaponLevel++;
          upgradeWeapon.Add(weaponData);
        } else {
          nonupgradeWeapon.Add(weaponData);
        }
      }

      
      for(int i = upgradeWeapon.Count; i < weaponUpgradeUI.options.Count; i++){
        foreach(WeaponScriptable weaponDataPre in allWeapon){
            if(!upgradeWeapon.Contains(weaponDataPre) && !nonupgradeWeapon.Contains(weaponDataPre)) upgradeWeapon.Add(weaponDataPre);
        }
      }
    }
    
    // 初始化武器升級介面 只會在角色升級時被呼叫
    public void InitWeaponUpgradeUI(){
      List<UpgradeOptions> options = weaponUpgradeUI.options;

      for(int i = 0; i < options.Count; i++){
        UpgradeOptions op = options[i];
        if(i < upgradeWeapon.Count) {
          WeaponScriptable weaponData = upgradeWeapon[i];
          op.IconImage = weaponData.weaponIcon;
          op.TitleText = weaponData.weaponName;
          op.DescriptionText = weaponData.description;
          op.Active = true;
          op.Listener = UpgradeWeapons;
          op.ReturnValue = weaponData;
        } else {
          op.Active = false;
        }
      }
    }
    
    public void UpgradeWeapons(Object obj){
      if(obj is WeaponScriptable weaponData){
        bool upgrade = false;
        foreach(WeaponBase weapon in equipWeapon){
          if(CheckUpgradeWeapon(weaponData, weapon)) {
            Debug.Log($"{weaponData.weaponName} Upgrade To ({weaponData.weaponLevel} Level)");
            weapon.SetWeaponData(weaponData);
            upgrade = true;
            break;
          }
        }

        if(!upgrade){
          AddWeapon(weaponData);
        }
      }

      weaponUpgradeUI.Active = false;
    }
    

    public void OnCharacterdataChange(CharacterScript.StatType type){
      switch (type)
      {
        case CharacterScript.StatType.Level:
          CheckUpgradeWeapons();
          InitWeaponUpgradeUI();
          weaponUpgradeUI.Active = true;
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
