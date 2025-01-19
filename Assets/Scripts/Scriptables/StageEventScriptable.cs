using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/StageEvent", order = 1)]
public class StageEventScriptable : ScriptableObject
{
    public List<EnemyWave> enemyWaves; // 敵人波
}

[Serializable]
public class EnemyWave
{
    [Range(0, 1800)] public float stageTime;  // 出現時間
    public string stageMsg;                   // 後台訊息
    public EnemyScriptable enemyData;         // 敵人數據
    [Range(0, 200)] public int enemyCount;    // 敵人數量

    public EnemyWave Clone() {
      EnemyWave c = new EnemyWave();
      c.stageTime = this.stageTime;
      c.stageMsg = this.stageMsg;
      c.enemyData = this.enemyData;
      c.enemyCount = this.enemyCount;
      return c;
    }
}