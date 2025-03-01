using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public static SceneManagerScript inst;

    private void Awake()
    {
        // �ˬd�O�_�w�g���@�ӹ��
        if (inst == null)
        {
            // �p�G�S����ҡA�]�m��e���󬰹�Ҩè���P��
            inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // �p�G�w����ҡA�P����e����
            Destroy(this);
        }
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
    /// �C���Ұʮɪ��������� StartGameSceneAction(string sGameSceneState)
    /// </summary>
    /// <param name="sGameSceneState"></param>
    public void StartGameSceneAction(string sGameSceneState)
    {
        SceneManager.LoadScene(sGameSceneState, LoadSceneMode.Single);  //���d�Ϊ�Scene
        SceneManager.LoadScene("PlayerDataScene", LoadSceneMode.Additive);   //Data��Scene
    }

    /// <summary>
    /// �C�������ɪ��������� EndGameSceneAction()
    /// </summary>
    public void EndGameSceneAction ()
    {
        SceneManager.LoadScene("LoginScene");  //�^��D�e��
    }
}
