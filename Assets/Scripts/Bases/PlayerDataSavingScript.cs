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
        sPlayerDataSavingPath = Path.Combine(sGetDataPath, "PlayerDataSaving.json");    //�w�]���| + �ɮצW��(PlayerDataSaving.json)
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
    /// Saving Json Data����k PlayerDataSaving()
    /// </summary>
    public void PlayerDataSaving()
    {
        //�g�JJson Data
        string json = JsonUtility.ToJson(_playerData);
        Debug.Log("Serialized JSON : " + json);
        File.WriteAllText(sPlayerDataSavingPath, json);

        Application.Quit();
    }

    /// <summary>
    /// Loading Json Data����k PlayerDataLoading()
    /// </summary>
    /// <returns></returns>
    public string PlayerDataLoading()
    {

        if (File.Exists(sPlayerDataSavingPath))
        {
            // Ū�����s�ɪ�Json���
            string json = File.ReadAllText(sPlayerDataSavingPath);
            //Debug.Log("Player data loaded: " + json);
            return json;
        }
        else
        {
            //�T�{���s�ɪ�Json���
            string json = JsonUtility.ToJson(_playerData);
            //Debug.Log("Player data not found: " + json);
            return null;
        }
    }

}

/// <summary>
/// �Ω�s��/Ū�������a���
/// </summary>
public class PlayerData
{
    public int iPlayer_Money;
    public int iPlayerItemLevel_HP;
    public int iPlayerItemLevel_ATK;
    public int iPlayerItemLevel_DEF;
    public int iPlayerItemLevel_moveSpeed;
}