using System;
using System.IO;
using UnityEngine;
using Cysharp.Threading.Tasks;  // 請先用 Package Manager 安裝 UniTask

[Serializable]
public static class StoreDataRepository
{
    public static string PlayerDataPath => PathManager.GetSaveFilePath("PlayerDataSaving.json");
    public static string UserSettingPath => PathManager.GetSaveFilePath("UserSettingSaving.json");

    #region 同步版本
    /// <summary> 玩家資料儲存的方法實作 </summary>
    public static void PlayerDataSaving(ref PlayerStoreData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(PlayerDataPath, json);
    }

    public static void PlayerDataLoading(ref PlayerStoreData data)
    {
        if (File.Exists(PlayerDataPath))
        {
            string json = File.ReadAllText(PlayerDataPath);
            data = JsonUtility.FromJson<PlayerStoreData>(json);
        }
        else
        {
            if (data == null) data = new PlayerStoreData();
            PlayerDataSaving(ref data);
        }
    }

    public static void UserDataSaving(ref UserStoreData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(UserSettingPath, json);
    }

    public static void UserDataLoading(ref UserStoreData data)
    {
        if (File.Exists(UserSettingPath))
        {
            string json = File.ReadAllText(UserSettingPath);
            data = JsonUtility.FromJson<UserStoreData>(json);
        }
        else
        {
            if (data == null) data = new UserStoreData();
            UserDataSaving(ref data);
        }
    }

    #endregion

    #region 非同步 UniTask 版本
    /// <summary> 玩家資料儲存的方法實作 </summary>
    public static async UniTask PlayerDataSavingAsync(PlayerStoreData data)
    {
        string json = JsonUtility.ToJson(data);
        using (var writer = new StreamWriter(PlayerDataPath, false))
        {
            await writer.WriteAsync(json);
        }
    }

    public static async UniTask PlayerDataLoadingAsync(PlayerStoreData data)
    {
        if (File.Exists(PlayerDataPath))
        {
            using (var reader = new StreamReader(PlayerDataPath))
            {
                string json = await reader.ReadToEndAsync();
                JsonUtility.FromJsonOverwrite(json, data);
            }
        }
        else
        {
            await PlayerDataSavingAsync(data);
        }
    }

    public static async UniTask UserDataSavingAsync(UserStoreData data)
    {
        string json = JsonUtility.ToJson(data);
        using (var writer = new StreamWriter(UserSettingPath, false))
        {
            await writer.WriteAsync(json);
        }
    }

    public static async UniTask UserDataLoadingAsync(UserStoreData data)
    {
        if (File.Exists(UserSettingPath))
        {
            using (var reader = new StreamReader(UserSettingPath))
            {
                string json = await reader.ReadToEndAsync();
                JsonUtility.FromJsonOverwrite(json, data);
            }
        }
        else
        {
            await UserDataSavingAsync(data);
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
    public static string SaveFolderPath => Application.persistentDataPath.Replace("\\", "/");

    /// <summary>取得指定檔案的完整儲存路徑與檔名</summary>
    public static string GetSaveFilePath(string fileName)
    {
        // 確保資料夾存在
        if (!Directory.Exists(SaveFolderPath))
            Directory.CreateDirectory(SaveFolderPath);

        return Path.Combine(SaveFolderPath, fileName).Replace("\\", "/");
    }

    /// <summary>取得 DataSaving.json 的完整路徑</summary>
    public static string DataSavingFilePath => GetSaveFilePath("DataSaving.json");
}