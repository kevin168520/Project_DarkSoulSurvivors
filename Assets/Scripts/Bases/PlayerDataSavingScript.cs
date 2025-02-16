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
        sPlayerDataSavingPath = Application.dataPath + "/PlayerDataSaving.json";    //�w�]���| + �ɮצW��(PlayerDataSaving.json)
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
    /// Saving Json Data����k PlayerDataSaving()
    /// </summary>
    public void PlayerDataSaving()
    {
        _playerData.iPlayer_Money = 0;
        _playerData.iPlayerItemLevel_HP = 0;
        _playerData.iPlayerItemLevel_ATK = 0;
        _playerData.iPlayerItemLevel_DEF = 0;
        _playerData.iPlayerItemLevel_moveSpeed = 0;


        //�g�JJson Data
        string json = JsonUtility.ToJson(_playerData);
        Debug.Log("Serialized JSON : " + json);
        File.WriteAllText(sPlayerDataSavingPath, json);
    }

    /// <summary>
    /// Loading Json Data����k PlayerDataLoading()
    /// </summary>
    /// <returns></returns>
    public static string PlayerDataLoading()
    {
        // ���w�[?������?�M���W
        string filePath = Path.Combine(Application.persistentDataPath, "data.json");

        if (File.Exists(filePath))
        {
            // Ū��Json���
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