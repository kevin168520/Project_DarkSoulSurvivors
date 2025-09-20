using UnityEngine;

public class StageManager : ManagerMonoBase, IEvent<LevelTimerEvent>
{
    [SerializeField] StageEventScriptable stageEvent; // 關卡敵人群

    void OnEnable()
    {
        EventGlobalManager.Instance.RegisterEvent(this);
    }

    void OnDisable()
    {
        EventGlobalManager.Instance?.DeregisterEvent(this);
    }

    public void Execute(LevelTimerEvent parameters)
    {
        OnLevelTimer(parameters.time);
    }

    // 關卡時間
    void OnLevelTimer(int time)
    {
        // 判定事件觸發
        if (!stageEvent.Check(time)) return;

        // 取得事件
        StageEvent e = stageEvent.Current();

        // 事件執行
        OnLevelEvent(e);

        // 事件進展
        stageEvent.Next();
    }

    // 關卡事件
    void OnLevelEvent(StageEvent stageEvent)
    {
        switch (stageEvent.command)
        {
            case SpawnEnemyCommand spawnEnemyCommand:
                SpawnEnemy(spawnEnemyCommand);
                break;
            case WinStageCommand winStage:
                WinStage(winStage);
                break;
        }
    }

    // 生成怪物群
    void SpawnEnemy(SpawnEnemyCommand enemyWave)
    {
        EnemyManager.AddEnemyWave(enemyWave.enemy, enemyWave.enemyCount); // 關卡生成敵人群添加到敵人管理者
        enemyWave.Execute();
    }

    // 關卡通關
    void WinStage(WinStageCommand winStage)
    {
        GameManager.GameComplete();
        winStage.Execute();
    }
}
