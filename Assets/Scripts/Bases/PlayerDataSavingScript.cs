using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerDataSavingScript : MonoBehaviour
{
    public static PlayerDataSavingScript inst;
    public PlayerData _playerData;

    public string sPlayerDataSavingPath;

    private void Awake()
    {
        inst = this;
        string sGetDataPath = Path.GetFullPath(Application.dataPath);
        sPlayerDataSavingPath = Path.Combine(sGetDataPath, "PlayerDataSaving.json");    //預設路徑 + 檔案名稱(PlayerDataSaving.json)
        Debug.Log(sPlayerDataSavingPath);
    }

    // Start is called before the first frame update
    void Start()
    {
        _playerData = new PlayerData();
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
        //寫入Json Data
        string json = JsonUtility.ToJson(_playerData);
        Debug.Log("Serialized JSON : " + json);
        File.WriteAllText(sPlayerDataSavingPath, json);

        Application.Quit();
    }

    /// <summary>
    /// Loading Json Data的方法 PlayerDataLoading()
    /// </summary>
    /// <returns></returns>
    public string PlayerDataLoading()
    {

        if (File.Exists(sPlayerDataSavingPath))
        {
            // 讀取有存檔的Json資料
            string json = File.ReadAllText(sPlayerDataSavingPath);
            //Debug.Log("Player data loaded: " + json);
            return json;
        }
        else
        {
            //確認未存檔的Json資料
            string json = JsonUtility.ToJson(_playerData);
            //Debug.Log("Player data not found: " + json);
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