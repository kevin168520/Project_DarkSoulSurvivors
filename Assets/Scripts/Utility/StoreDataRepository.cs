using System;
using System.IO;
using UnityEngine;
using Cysharp.Threading.Tasks;  // 請先用 Package Manager 安裝 UniTask

[Serializable]
public static class StoreDataRepository
{
    static string _sPlayerDataSavingPath;
    static string _sUserDataSavingPath;

    public static string PlayerDataPath
    {
        get
        {
            if (_sPlayerDataSavingPath == null)
                _sPlayerDataSavingPath = PathManager.GetSaveFilePath("PlayerDataSaving.json");
            return _sPlayerDataSavingPath;
        }
    }

    public static string UserSettingPath
    {
        get
        {
            if (_sUserDataSavingPath == null)
                _sUserDataSavingPath = PathManager.GetSaveFilePath("UserSettingSaving.json");
            return _sUserDataSavingPath;
        }
    }

    #region 同步版本
    /// <summary> 玩家資料儲存的方法實作 </summary>
    public static void PlayerDataSaving(ref PlayerStoreData data)
    {
        try
        {
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(PlayerDataPath, json);
            Debug.Log("PlayerDataSaving Success : " + json);
        }
        catch (Exception e)
        {
            Debug.LogError($"[Sync] Error saving player data: {e}");
        }
    }

    public static void PlayerDataLoading(ref PlayerStoreData data)
    {
        try
        {
            if (File.Exists(PlayerDataPath))
            {
                string json = File.ReadAllText(PlayerDataPath);
                data = JsonUtility.FromJson<PlayerStoreData>(json);
                Debug.Log("PlayerDataLoading Success : " + json);
            }
            else
            {
                Debug.LogWarning("[Sync] Player data file not found, creating new data.");
                if (data == null) data = new PlayerStoreData();
                PlayerDataSaving(ref data);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[Sync] Error loading player data: {e}");
        }
    }

    public static void UserDataSaving(ref UserStoreData data)
    {
        try
        {
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(UserSettingPath, json);
            Debug.Log("UserDataSaving Success : " + json);
        }
        catch (Exception e)
        {
            Debug.LogError($"[Sync] Error saving user data: {e}");
        }
    }

    public static void UserDataLoading(ref UserStoreData data)
    {
        try
        {
            if (File.Exists(UserSettingPath))
            {
                string json = File.ReadAllText(UserSettingPath);
                data = JsonUtility.FromJson<UserStoreData>(json);
                Debug.Log("UserDataLoading Success : " + json);
            }
            else
            {
                Debug.LogWarning("[Sync] User data file not found, creating new data.");
                if (data == null) data = new UserStoreData();
                UserDataSaving(ref data);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[Sync] Error loading user data: {e}");
        }
    }

    #endregion

    #region 非同步 UniTask 版本
    /// <summary> 玩家資料儲存的方法實作 </summary>
    public static async UniTask PlayerDataSavingAsync(PlayerStoreData data)
    {
        try
        {
            string json = JsonUtility.ToJson(data);
            using (var writer = new StreamWriter(PlayerDataPath, false))
            {
                await writer.WriteAsync(json);
            }
            Debug.Log("PlayerDataSavingAsync Success : " + json);
        }
        catch (Exception e)
        {
            Debug.LogError($"[Async] Error saving player data: {e}");
        }
    }

    public static async UniTask PlayerDataLoadingAsync(PlayerStoreData data)
    {
        try
        {
            if (File.Exists(PlayerDataPath))
            {
                using (var reader = new StreamReader(PlayerDataPath))
                {
                    string json = await reader.ReadToEndAsync();
                    JsonUtility.FromJsonOverwrite(json, data);
                    Debug.Log("PlayerDataLoadingAsync Success : " + json);
                }
            }
            else
            {
                Debug.LogWarning("[Async] Player data file not found, creating new data.");
                await PlayerDataSavingAsync(data);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[Async] Error loading player data: {e}");
        }
    }

    public static async UniTask UserDataSavingAsync(UserStoreData data)
    {
        try
        {
            string json = JsonUtility.ToJson(data);
            using (var writer = new StreamWriter(UserSettingPath, false))
            {
                await writer.WriteAsync(json);
            }
            Debug.Log("UserDataSavingAsync Success : " + json);
        }
        catch (Exception e)
        {
            Debug.LogError($"[Async] Error saving user data: {e}");
        }
    }

    public static async UniTask UserDataLoadingAsync(UserStoreData data)
    {
        try
        {
            if (File.Exists(UserSettingPath))
            {
                using (var reader = new StreamReader(UserSettingPath))
                {
                    string json = await reader.ReadToEndAsync();
                    JsonUtility.FromJsonOverwrite(json, data);
                    Debug.Log("UserDataLoadingAsync Success : " + json);
                }
            }
            else
            {
                Debug.LogWarning("[Async] User data file not found, creating new data.");
                await UserDataSavingAsync(data);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[Async] Error loading user data: {e}");
        }
    }

    #endregion
}

/// <summary>
/// 建立存檔輸出路徑：透過UnityEditor建立的會在Assets下，透過Build出來的會在AppData/LocalLow/公司名稱/BlazingMaiidens
/// </summary>
public static class PathManager
{
    /// <summary>取得遊戲存檔資料夾路徑</summary>
    public static string SaveFolderPath
    {
        get {
            #if UNITY_EDITOR
            // 編輯器下 → 存到專案 Assets 資料夾
            return Application.dataPath.Replace("\\", "/");
            #else
            // Build 後 → 直接使用 persistentDataPath
            return Application.persistentDataPath.Replace("\\", "/");
            #endif
        }
    }

    /// <summary>取得指定檔案的完整儲存路徑與檔名</summary>
    public static string GetSaveFilePath(string fileName)
    {
        // 確保資料夾存在
        if (!Directory.Exists(SaveFolderPath))
            Directory.CreateDirectory(SaveFolderPath);

        return Path.Combine(SaveFolderPath, fileName).Replace("\\", "/");
    }

    /// <summary>取得 DataSaving.json 的完整路徑</summary>
    public static string DataSavingFilePath
    {
        get {return GetSaveFilePath("DataSaving.json");}
    }
}