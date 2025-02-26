using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public static SceneManagerScript inst;

    private void Awake()
    {
        inst = this;
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
    /// �C�������ɪ��������� EndGameSceneAction (string sGameSceneState)
    /// </summary>
    /// <param name="sGameSceneState"></param>
    public void EndGameSceneAction (string sGameSceneState)
    {

    }
}
