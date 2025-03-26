using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StoreDataRepository : MonoBehaviour
{
    static string _sPlayerDataSavingPath;
    public static string PATH { get {
        if(_sPlayerDataSavingPath == null){
            //預設路徑 + 檔案名稱(PlayerDataSaving.json)
            string sGetDataPath = Path.GetFullPath(Application.dataPath);
            _sPlayerDataSavingPath = Path.Combine(sGetDataPath, "PlayerDataSaving.json");
        }
        return _sPlayerDataSavingPath;
    }}

    /// <summary> Saving Json Data的方法 PlayerDataSaving(bool bGameExit)</summary>
    static public void PlayerDataSaving(ref PlayerStoreData _playerData)
    {
        //寫入Json Data
        string json = JsonUtility.ToJson(_playerData);
        File.WriteAllText(PATH, json);
        Debug.Log("Serialized JSON : " + json);
    }

    /// <summary> Loading Json Data的方法 PlayerDataLoading() </summary>
    static public void PlayerDataLoading(ref PlayerStoreData _playerData)
    {
        //判別是否有存檔資料sPlayerDataSavingPath
        if (File.Exists(PATH))
        {
            //帶入路徑資料
            string json = File.ReadAllText(PATH);
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
}
