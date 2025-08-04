using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary> 負責角色武器與添加狀態與畫面等級 </summary>
public class WeaponManager : ManagerMonoBase
{
    [Header("武器放置座標")]
    [SerializeField] Transform weaponTransform;
    [Header("武器升級 UI")]
    [SerializeField] WeaponUpgradeUI weaponUpgradeUI;
    [Header("武器資料庫")]
    [SerializeField] WeaponDatabaseScriptable weaponDatabase;
    [Header("已裝備武器")]
    [SerializeField] List<WeaponHandlerBase> equipWeapon = new List<WeaponHandlerBase>();

    void Start()
    {
        // 載入起始武器資料
        CharacterScriptable characterData = DataGlobalManager._characterData;
        AddWeapon(characterData.startingWeapon);
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

        // 移動武器放置座標
        weapon.transform.parent = weaponTransform;
        weapon.transform.localPosition  = Vector3.zero;
        weapon.gameObject.SetActive(true);
    }

    public List<WeaponHandlerBase> GetWeapons() => equipWeapon;
    
    // 初始化武器升級介面
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
            GameManager.PauseGame();
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
                    GameManager.UnPauseGame();
                    return;
                }
            }
            // 升級武器不是當前裝備 執行新增武器
            Debug.Log($"{weaponData.weaponName} New Add");
            AddWeapon(weaponData);
            weaponUpgradeUI.Active = false;
            GameManager.UnPauseGame();
        }
    }
}
