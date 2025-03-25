using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    Transform playerTransform => GameManager.instance.playerTransform; // 玩家座標資料
    CharacterScriptable playerData => DataGlobalManager.inst._characterData; // 角色資料
    CharacterScript playerCharacter => GameManager.instance.playerCharacter; // 玩家角色
    [SerializeField] WeaponUpgradeUI weaponUpgradeUI; // 玩家升級 UI
    [SerializeField] List<WeaponHandlerBase> equipWeapon = new List<WeaponHandlerBase>(); // 已裝備武器
    [SerializeField] WeaponDatabaseScriptable weaponDatabase; // 武器資料庫
    
    void Start()
    {
        // 添加角色起始武器
        AddWeapon(playerData.startingWeapon);

        // 註冊角色監聽
        playerCharacter.dataChangeListener.AddListener(OnCharacterdataChange);
    }
    
    // 添加武器物件 透過武器資料
    public void AddWeapon(WeaponScriptable weaponData){
      WeaponHandlerBase weapon = Instantiate(weaponData.weaponPrefab).GetComponent<WeaponHandlerBase>();
      weapon.LoadWeaponData(weaponData);
      AddWeapon(weapon);
    }
    
    // 添加武器物件
    public void AddWeapon(WeaponHandlerBase weapon){
      equipWeapon.Add(weapon);

      // 移動玩家座標下
      weapon.transform.parent = playerTransform;
      weapon.transform.localPosition  = Vector3.zero;
      weapon.gameObject.SetActive(true);
    }

    public List<WeaponHandlerBase> GetWeapons() => equipWeapon;
    
    // 初始化武器升級介面 只會在角色升級時被呼叫
    public void InitWeaponUpgradeUI(){
      
      // 篩選武器內容
      List<WeaponScriptable> upgradeWeapon = new List<WeaponScriptable>();
      List<WeaponScriptable> nonupgradeWeapon = new List<WeaponScriptable>();

      foreach(WeaponHandlerBase weapon in equipWeapon){
        WeaponScriptable weaponData = Instantiate(weaponDatabase.Search(weapon.weaponNumber));
        if(weapon.IsWeaponUpgradeable()) {
          weaponData.weaponLevel++;
          upgradeWeapon.Add(weaponData);
        } else {
          nonupgradeWeapon.Add(weaponData);
        }
      }

      // 如果還有剩餘位置 從武器資料庫中塞選
      if(upgradeWeapon.Count < weaponUpgradeUI.options.Count){
        weaponDatabase.Shuffle();
        
        foreach (WeaponScriptable weaponDataPre in weaponDatabase)
        {
            if (upgradeWeapon.Count >= weaponUpgradeUI.options.Count) break;
            if (!upgradeWeapon.Contains(weaponDataPre) && !nonupgradeWeapon.Contains(weaponDataPre))
            {
                upgradeWeapon.Add(weaponDataPre);
            }
        }
      }

      // 設置選項內容
      for(int i = 0; i < weaponUpgradeUI.options.Count; i++){
        UpgradeOptions op = weaponUpgradeUI.options[i];
        if(i < upgradeWeapon.Count) {
          WeaponScriptable weaponData = upgradeWeapon[i];
          op.IconImage = weaponData.weaponIcon;
          op.TitleText = weaponData.weaponName;
          op.DescriptionText = weaponData.description;
          op.Active = true;
          op.Listener = OnUpgradeWeapons;
          op.ReturnValue = weaponData;
        } else {
          op.Active = false;
        }
      }

      // 開啟武器生級選單
      if(upgradeWeapon.Count > 0) {
        weaponUpgradeUI.Active = true;
        GameManager.instance.PauseGame();
      }
    }
    
    // 升級武器選項的監聽
    public void OnUpgradeWeapons(Object obj){
      if(obj is WeaponScriptable weaponData){
        // 升級武器是當前裝備 更新武器資料
        foreach(WeaponHandlerBase weapon in equipWeapon){
          if(weapon.IsSameWeapon(weaponData)) {
            Debug.Log($"{weaponData.weaponName} Upgrade To ({weaponData.weaponLevel} Level)");
            weapon.LoadWeaponData(weaponData);
            weaponUpgradeUI.Active = false;
            GameManager.instance.UnPauseGame();
            return;
          }
        }
        // 升級武器不是當前裝備 執行新增武器
        Debug.Log($"{weaponData.weaponName} New Add");
        AddWeapon(weaponData);
        weaponUpgradeUI.Active = false;
        GameManager.instance.UnPauseGame();
      }
    }
    
    // 角色資料監聽
    public void OnCharacterdataChange(CharacterScript.StatType type){
      switch (type)
      {
        case CharacterScript.StatType.Level:
          InitWeaponUpgradeUI();
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
