using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] EnemyManager enemyManager; // 敵方管理者
    [SerializeField] StageEventScriptable stageEvents; // 關卡敵人群
    float stageTime;  // 關卡時間
    int stageEventIndex;   // 關卡事件索引
    
    void Start()
    {
    }

    // 計時關卡時間
    void Update()
    {
        stageTime += Time.deltaTime; 
        UpdateEnemyWave();
    }

    void UpdateEnemyWave()
    {
        // 超過事件則不動作
        if(stageEventIndex >= stageEvents.enemyWaves.Count) return;

        // 判定事件達到觸發時間
        if(stageTime > stageEvents.enemyWaves[stageEventIndex].stageTime)
        {
          enemyManager.AddEnemyWave(stageEvents.enemyWaves[stageEventIndex]); // 關卡生成敵人群添加到敵人管理者
          Debug.Log(stageEvents.enemyWaves[stageEventIndex].stageMsg);
          stageEventIndex += 1;  // 關卡事件索引 +1
        }
    }
}
