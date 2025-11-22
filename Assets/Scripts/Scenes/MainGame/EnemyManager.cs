using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : ManagerMonoBase
{
    PlayerScript player => PlayerManager.Player; // 玩家目標

    // 內部物件
    [SerializeField] Vector2 spawnArea; // 生成範圍 通常螢幕範圍

    [SerializeField] GameObject enemyPrefab; // 怪物模板

    List<(EnemyScriptable enemy, int enemyCount)> enemyWaves = new(); // 待生成敵人柱列

    Queue<GameObject> enemyPool = new Queue<GameObject>(); // 敵人池

    public int enemyKilledCount; // 臨時殺敵計數

    void Start()
    {
        Camera cam = Camera.main;
        spawnArea = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.nearClipPlane));
    }

    // 每偵生成一個待生成敵人
    void Update()
    {
        ProcessSpawn();
    }

    // 添加敵人群
    public void AddEnemyWave(EnemyScriptable enemy, int enemyCount)
    {
        enemyWaves.Add((enemy, enemyCount)); // 複製避免更動到資源檔
    }

    // 判定待生成敵人
    void ProcessSpawn()
    {
        if (enemyWaves.Count == 0) return; // 沒有待生成

        SpawnEnemy(enemyWaves[0].enemy);
        enemyWaves[0] = (enemyWaves[0].enemy, enemyWaves[0].enemyCount - 1);

        if (enemyWaves[0].enemyCount <= 0) // 該敵人群已沒有待生成則移除
            enemyWaves.RemoveAt(0);
    }

    // 執行生成敵人
    void SpawnEnemy(EnemyScriptable enemyData = null)
    {
        if (enemyData == null) return;

        GameObject newEnemy = AcquireEnemy();
        newEnemy.transform.position = GenerateRandomPosition(); // 設置座標座標

        EnemyScript newEnemyScript = newEnemy.GetComponent<EnemyScript>();
        newEnemyScript.SetTarget(player.transform); // 設置移動目標
        newEnemyScript.SetTargetDamageable(player.character); // 設置傷害目標
        newEnemyScript.SetEnemyData(enemyData); // 設置敵人數據
        newEnemyScript.LoadEnemyData(); // 載入敵人資料
    }

    // 從敵人池取出
    GameObject AcquireEnemy()
    {
        GameObject enemy;
        if (enemyPool.Count > 0)
        {
            enemy = enemyPool.Dequeue(); // 從池子取出敵人
            enemy.SetActive(true);
        }
        else
        {
            enemy = Instantiate(enemyPrefab); // 生成敵人
            enemy.transform.parent = transform; // 除錯用 保持場景層級
            enemy.GetComponent<EnemyScript>().OnDeath = RecycleEnemy; // 回收對象
        }
        return enemy;
    }

    // 回收到敵人池
    void RecycleEnemy(GameObject enemy)
    {
        enemyKilledCount++;
        enemy.SetActive(false);
        enemyPool.Enqueue(enemy);
    }

    // 計算隨機生成位置
    Vector3 GenerateRandomPosition()
    {
        // 定位玩家位置
        Vector3 position = new Vector3();
        position += player.transform.position;

        switch (Random.value)
        {
            case < 0.25f: // 下方
                position.x += Random.Range(-spawnArea.x, spawnArea.x);
                position.y += spawnArea.y * -1f;
                position.z += 0f;
                break;
            case < 0.5f: // 左方
                position.y += Random.Range(-spawnArea.y, spawnArea.y);
                position.x += spawnArea.x * -1f;
                position.z += 0f;
                break;
            case < 0.75f: // 上方
                position.x += Random.Range(-spawnArea.x, spawnArea.x);
                position.y += spawnArea.y * 1f;
                position.z += 0f;
                break;
            default: // 右方
                position.y += Random.Range(-spawnArea.y, spawnArea.y);
                position.x += spawnArea.x * 1f;
                position.z += 0f;
                break;
        }
        return position;
    }
}
