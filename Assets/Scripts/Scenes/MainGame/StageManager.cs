using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : ManagerMonoBase
{
    [SerializeField] StageEventScriptable stageEvent; // 關卡敵人群

    void Start()
    {
      // 註冊 關卡時間 監聽
      LevelTimerManager.AddListener(OnLevelTimer);
    }

    void OnLevelTimer(int time)
    {
        // 判定事件觸發
        if(stageEvent.Check(time)) return;
        
        // 取得事件
        StageEvent e = stageEvent.Current();

        // 事件執行
        OnLevelEvent(e);
        
        // 事件進展
        stageEvent.Next();
    }

    void OnLevelEvent(StageEvent stageEvent)
    {
        switch(stageEvent.type){
          case StageEventType.SpawnEnemy:
            SpawnEnemy(stageEvent);
            break;
          case StageEventType.WinStage:
            WinStage();
            break;
        }
    }
    
    // 生成怪物群
    void SpawnEnemy(StageEvent stageEvent){
        EnemyManager.AddEnemyWave(stageEvent); // 關卡生成敵人群添加到敵人管理者
        if(stageEvent.message.Length != 0) Debug.Log(stageEvent.message);
    }
    
    // 關卡通關
    void WinStage(){
      GameManager.GameComplete();
    }
}
