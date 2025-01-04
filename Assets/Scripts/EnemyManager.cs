using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public GameObject player; // 玩家目標
    [SerializeField] private GameObject enemy; // 敵人物件

    // 內部物件
    [SerializeField] private Vector2 spawnArea; // 生成範圍 通常螢幕範圍

    
    List<EnemyWave> enemyWaves = new List<EnemyWave>(); // 待生成敵人柱列

    void Awake()
    {
    }

    void Start()
    {
    }


    // 每偵生成一個待生成敵人
    void Update() {
        ProcessSpawn();
    }

    // 添加敵人群
    public void AddEnemyWave(EnemyWave enemyWave) {
        enemyWaves.Add(enemyWave.Clone()); // 複製避免更動到資源檔
    }


    // 判定待生成敵人
    void ProcessSpawn() {
        if(enemyWaves.Count == 0) return; // 沒有待生成

        SpawnEnemy(enemyWaves[0].enemyData);
        enemyWaves[0].enemyCount -= 1;

        if(enemyWaves[0].enemyCount <= 0) // 該敵人群已沒有待生成則移除
          enemyWaves.RemoveAt(0);
    }

    // 執行生成敵人
    void SpawnEnemy(EnemyScriptable enemyData = null)
    {

        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = GenerateRandomPosition(); // 設置座標座標
        newEnemy.transform.parent = transform; // 除錯用 保持場景層級

        EnemyScript newEnemyScript = newEnemy.GetComponent<EnemyScript>();
        newEnemyScript.SetTarget(player); // 設置目標
        if(enemyData) newEnemyScript.SetEnemyData(enemyData); // 設置敵人數據
    }

    // 計算隨機生成位置
    private Vector3 GenerateRandomPosition()
    {
        // 定位玩家位置
        Vector3 position = new Vector3();
        position += player.transform.position;

        switch(UnityEngine.Random.value)
        {
          case < 0.25f: // 下方
            position.x = UnityEngine.Random.Range(-spawnArea.x, spawnArea.x);
            position.y = spawnArea.y * -1f;
            position.z = 0f;
            break;
          case < 0.5f: // 左方
            position.y = UnityEngine.Random.Range(-spawnArea.y, spawnArea.y);
            position.x = spawnArea.x * -1f;
            position.z = 0f;
            break;
          case < 0.75f: // 上方
            position.x = UnityEngine.Random.Range(-spawnArea.x, spawnArea.x);
            position.y = spawnArea.y * 1f;
            position.z = 0f;
            break;
          default: // 右方
            position.y = UnityEngine.Random.Range(-spawnArea.y, spawnArea.y);
            position.x = spawnArea.x * 1f;
            position.z = 0f;
            break;
        }
        return position;
    }
}
