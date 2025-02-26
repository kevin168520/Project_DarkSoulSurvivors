using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public static SceneManagerScript inst;

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
    /// 遊戲結束時的場景切換 EndGameSceneAction()
    /// </summary>
    public void EndGameSceneAction ()
    {
        SceneManager.LoadScene("LoginScene");  //回到主畫面
    }
}
