using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour
{
    public List<CharacterScriptable> _characterList;
    public void SetCharacter(int num)
    {
        if(_characterList[num] == null) {
          Debug.LogWarning("Lost Character Data!!!");
          return;
        }
        GameManager.character = _characterList[num];
    }
    public void StartGame(string sGameSceneState)
    {
        //SceneManager.LoadScene(sGameSceneState, LoadSceneMode.Single);  //關卡用的Scene
        //SceneManager.LoadScene("PlayerDataScene", LoadSceneMode.Additive);   //Data的Scene

        SceneManagerScript.inst.StartGameSceneAction(sGameSceneState);
    }
}