using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour
{
    CharacterScriptable character { // 跨場景用角色資料
        get => PlayerDataSavingScript.inst._characterData;
        set => PlayerDataSavingScript.inst._characterData = value;
    }
    public List<CharacterScriptable> _characterList;
    public void SetCharacter(int num)
    {
        if(_characterList[num] == null) {
          Debug.LogWarning("Lost Character Data!!!");
          return;
        }
        character = _characterList[num];
    }
    public void StartGame(string sGameSceneState)
    {
        //SceneManager.LoadScene(sGameSceneState, LoadSceneMode.Single);  //關卡用的Scene
        //SceneManager.LoadScene("PlayerDataScene", LoadSceneMode.Additive);   //Data的Scene

        SceneManagerScript.inst.StartGameSceneAction(sGameSceneState);
    }
}