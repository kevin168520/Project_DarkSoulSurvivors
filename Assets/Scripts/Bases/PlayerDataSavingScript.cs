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

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Saving Json Data����k PlayerDataSaving(bool bGameExit)
    /// </summary>
    /// <param name="bGameExit"></param>
    public void PlayerDataSaving(bool bGameExit)
    {
        //�g�JJson Data
        string json = JsonUtility.ToJson(_playerData);
        Debug.Log("Serialized JSON : " + json);
        File.WriteAllText(sPlayerDataSavingPath, json);

        if (bGameExit)
        {
            Application.Quit();
        }
    }

    /// <summary>
    /// Loading Json Data����k PlayerDataLoading()
    /// </summary>
    /// <returns></returns>
    public string PlayerDataLoading()
    {
        //�P�O�O�_���s�ɸ��sPlayerDataSavingPath
        if (File.Exists(sPlayerDataSavingPath))
        {
            //�a�J���|���
            string json = File.ReadAllText(sPlayerDataSavingPath);
            _playerData = JsonUtility.FromJson<PlayerData>(json);
            Debug.Log("Player data loaded: " + json);
            return json;
        }
        else
        {
            //�]�����|�U�S��������ơA�]�����ͤ@�ӷs���æ۰ʦs��
            _playerData = new PlayerData();
            Debug.Log("Player data not found");
            PlayerDataSaving(false);
            return null;
        }
    }

}

[System.Serializable]
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