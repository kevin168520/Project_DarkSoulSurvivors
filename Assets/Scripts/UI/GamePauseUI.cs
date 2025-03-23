using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    public GameObject gameOverPanel; // �C������UI
    public GameObject gameCompletePanel; // �C���q��UI
    public GameObject gamePauseMenu; // �C���Ȱ�UI

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
    public void BtnCtrlGameScene()
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
            GameManager.instance.SummaryEvent();
        });
    }
}
