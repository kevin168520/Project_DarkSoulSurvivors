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
    [SerializeField, Range(0, 10)] private float spawnTimer; // 波生成時間
    private float timer; // 計時器

    void Awake()
    {
    }

    void Start()
    {
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0f)
        {
          SpawnEnemy();
          timer = spawnTimer;
        }
    }

    // 生成敵人
    private void SpawnEnemy()
    {
      
        Vector3 position = GenerateRandomPosition();
        position += player.transform.position;

        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = position;
        newEnemy.GetComponent<EnemyScript>().SetTarget(player);

        newEnemy.transform.parent = transform; // 非必要 用於保持場景層級
    }

    // 計算隨機生成位置
    private Vector3 GenerateRandomPosition()
    {
        Vector3 position = new Vector3();
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
