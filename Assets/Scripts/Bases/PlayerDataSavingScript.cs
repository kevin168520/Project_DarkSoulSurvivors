using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerDataSavingScript : MonoBehaviour
{
    private PlayerData _playerData;

    public string sPlayerDataSavingPath;

    private void Awake()
    {
        sPlayerDataSavingPath = Application.dataPath + "/PlayerDataSaving.json";    //預設路徑 + 檔案名稱(PlayerDataSaving.json)
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Saving Json Data的方法 PlayerDataSaving()
    /// </summary>
    public void PlayerDataSaving()
    {
        _playerData.iPlayer_Money = 0;
        _playerData.iPlayerItemLevel_HP = 0;
        _playerData.iPlayerItemLevel_ATK = 0;
        _playerData.iPlayerItemLevel_DEF = 0;
        _playerData.iPlayerItemLevel_moveSpeed = 0;


        //寫入Json Data
        string json = JsonUtility.ToJson(_playerData);
        Debug.Log("Serialized JSON : " + json);
        File.WriteAllText(sPlayerDataSavingPath, json);
    }

    /// <summary>
    /// Loading Json Data的方法 PlayerDataLoading()
    /// </summary>
    /// <returns></returns>
    public static string PlayerDataLoading()
    {
        // 指定加?的文件路?和文件名
        string filePath = Path.Combine(Application.persistentDataPath, "data.json");

        if (File.Exists(filePath))
        {
            // 讀取Json資料
            string json = File.ReadAllText(filePath);
            Debug.Log("Player data loaded.");
            return json;
        }
        else
        {
            Debug.Log("Player data not found.");
            return null;
        }
    }

}

/// <summary>
/// 用於存檔/讀取的玩家資料
/// </summary>
public class PlayerData
{
    public int iPlayer_Money;
    public int iPlayerItemLevel_HP;
    public int iPlayerItemLevel_ATK;
    public int iPlayerItemLevel_DEF;
    public int iPlayerItemLevel_moveSpeed;
}