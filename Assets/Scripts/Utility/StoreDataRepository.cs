using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StoreDataRepository : MonoBehaviour
{
    static string _sPlayerDataSavingPath;
    static string _sUserDataSavingPath;

    public static string PlayerDataPath 
    { 
        get 
        {
            if(_sPlayerDataSavingPath == null){
            //預設路徑 + 檔案名稱(PlayerDataSaving.json)
            string sGetDataPath = Path.GetFullPath(Application.dataPath);
            _sPlayerDataSavingPath = Path.Combine(sGetDataPath, "PlayerDataSaving.json");
        }
        return _sPlayerDataSavingPath;
    }}

    public static string UserSettingPath
    {
        get
        {
            if (_sUserDataSavingPath == null)
            {
                //預設路徑 + 檔案名稱(PlayerDataSaving.json)
                string sGetDataPath = Path.GetFullPath(Application.dataPath);
                _sUserDataSavingPath = Path.Combine(sGetDataPath, "UserSettingSaving.json");
            }
            return _sUserDataSavingPath;
        }
    }

    /// <summary>
    /// 玩家資料儲存的方法 PlayerDataSaving(ref PlayerStoreData _playerData)
    /// </summary>
    /// <param name="_playerData"></param>
    static public void PlayerDataSaving(ref PlayerStoreData _playerData)
    {
        //寫入Json Data
        string json = JsonUtility.ToJson(_playerData);
        File.WriteAllText(PlayerDataPath, json);
        Debug.Log("Serialized JSON : " + json);
    }

    /// <summary>
    /// 玩家資料載入的方法 PlayerDataLoading(ref PlayerStoreData _playerData)
    /// </summary>
    /// <param name="_playerData"></param>
    static public void PlayerDataLoading(ref PlayerStoreData _playerData)
    {
        //判別是否有存檔資料sPlayerDataSavingPath
        if (File.Exists(PlayerDataPath))
        {
            //帶入路徑資料
            string json = File.ReadAllText(PlayerDataPath);
            _playerData = JsonUtility.FromJson<PlayerStoreData>(json);
            Debug.Log("Player data loaded: " + json);
        }
        else
        {
            //因為路徑下沒有對應資料，因此產生一個新的並自動存檔
            Debug.Log("Player data not found");
            StoreDataRepository.PlayerDataSaving(ref _playerData);
        }
    }

    /// <summary> 使用者設定儲存的方法 </summary>
    static public void UserDataSaving(ref UserStoreSetting _userSetting)
    {
        //寫入Json Data
        string json = JsonUtility.ToJson(_userSetting);
        File.WriteAllText(UserSettingPath, json);
        Debug.Log("Serialized JSON : " + json);
    }

    /// <summary> 使用者設定載入的方法 </summary>
    static public void UserDataLoading(ref UserStoreSetting _userSetting)
    {
        //判別是否有存檔資料sPlayerDataSavingPath
        if (File.Exists(UserSettingPath))
        {
            //帶入路徑資料
            string json = File.ReadAllText(UserSettingPath);
            _userSetting = JsonUtility.FromJson<UserStoreSetting>(json);
            Debug.Log("Player data loaded: " + json);
        }
        else
        {
            //因為路徑下沒有對應資料，因此產生一個新的並自動存檔
            Debug.Log("Player data not found");
            StoreDataRepository.UserDataSaving(ref _userSetting);
        }
    }

}
