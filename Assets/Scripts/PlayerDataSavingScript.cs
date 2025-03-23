using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerDataSavingScript : MonoBehaviour
{
    public static PlayerDataSavingScript inst;
    public PlayerData _playerData; // 商店資料

    public string sPlayerDataSavingPath;
    public CharacterScriptable _characterData; // 玩家資料

    private void Awake()
    {
        // 檢查是否已經有一個實例
        if (inst == null)
        {
            // 如果沒有實例，設置當前物件為實例並防止銷毀
            inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 如果已有實例，銷毀當前物件
            Destroy(this);
        }

        string sGetDataPath = Path.GetFullPath(Application.dataPath);
        sPlayerDataSavingPath = Path.Combine(sGetDataPath, "PlayerDataSaving.json");    //預設路徑 + 檔案名稱(PlayerDataSaving.json)
        Debug.Log(sPlayerDataSavingPath);
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
    /// Saving Json Data的方法 PlayerDataSaving(bool bGameExit)
    /// </summary>
    /// <param name="bGameExit"></param>
    public void PlayerDataSaving(bool bGameExit)
    {
        //寫入Json Data
        string json = JsonUtility.ToJson(_playerData);
        Debug.Log("Serialized JSON : " + json);
        File.WriteAllText(sPlayerDataSavingPath, json);

        if (bGameExit)
        {
            Application.Quit();
        }
    }

    /// <summary>
    /// Loading Json Data的方法 PlayerDataLoading()
    /// </summary>
    /// <returns></returns>
    public string PlayerDataLoading()
    {
        //判別是否有存檔資料sPlayerDataSavingPath
        if (File.Exists(sPlayerDataSavingPath))
        {
            //帶入路徑資料
            string json = File.ReadAllText(sPlayerDataSavingPath);
            _playerData = JsonUtility.FromJson<PlayerData>(json);
            Debug.Log("Player data loaded: " + json);
            return json;
        }
        else
        {
            //因為路徑下沒有對應資料，因此產生一個新的並自動存檔
            _playerData = new PlayerData();
            Debug.Log("Player data not found");
            PlayerDataSaving(false);
            return null;
        }
    }

}

[System.Serializable]
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