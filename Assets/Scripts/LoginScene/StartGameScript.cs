using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour
{
    public void StartGame(string sGameSceneState)
    {
        SceneManager.LoadScene(sGameSceneState, LoadSceneMode.Single);  //關卡用的Scene
        SceneManager.LoadScene("PlayerDataScene", LoadSceneMode.Additive);   //Data的Scene
    }
}