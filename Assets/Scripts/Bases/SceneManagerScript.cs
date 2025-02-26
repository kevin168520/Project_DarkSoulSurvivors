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
    /// 遊戲啟動時的場景切換 StartGameSceneAction(string sGameSceneState)
    /// </summary>
    /// <param name="sGameSceneState"></param>
    public void StartGameSceneAction(string sGameSceneState)
    {
        SceneManager.LoadScene(sGameSceneState, LoadSceneMode.Single);  //關卡用的Scene
        SceneManager.LoadScene("PlayerDataScene", LoadSceneMode.Additive);   //Data的Scene
    }

    /// <summary>
    /// 遊戲結束時的場景切換 EndGameSceneAction (string sGameSceneState)
    /// </summary>
    /// <param name="sGameSceneState"></param>
    public void EndGameSceneAction (string sGameSceneState)
    {

    }
}
