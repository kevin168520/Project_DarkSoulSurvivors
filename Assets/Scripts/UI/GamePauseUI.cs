using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel; // �C������UI
    [SerializeField] private GameObject gameCompletePanel; // �C���q��UI
    [SerializeField] private GameObject gamePauseMenu; // �C���Ȱ�UI

    [SerializeField] private Button btnReturnToGame; // �^��C���~��i��
    [SerializeField] private Button btnSetting; // �|�L�\��
    [SerializeField] private Button btnBackToMenu; // �^��LoginScene

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// �C���������s���� BtnCtrlGameScene()
    /// </summary>
    private void BtnCtrlGameScene()
    {
        btnReturnToGame.onClick.AddListener(delegate ()
        {
            gamePauseMenu.SetActive(false);
            GameManager.instance.UnPauseGame();
        });

        btnSetting.onClick.AddListener(delegate ()
        {
            Debug.Log("Not Finish");
        });

        btnBackToMenu.onClick.AddListener(delegate ()
        {
            gamePauseMenu.SetActive(false);
            GameManager.instance.UnPauseGame();
            SceneManagerScript.inst.EndGameSceneAction();
        });
    }
}
