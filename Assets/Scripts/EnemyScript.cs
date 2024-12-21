using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour, IDamageable
{
  
    [SerializeField] private Transform target; // 移動目標
    public void SetTarget(GameObject gb) => target = gb.transform;

    // 內部物件
    [SerializeField] private Rigidbody2D rgdbd; // 剛體


    [SerializeField] private EnemyScriptable data; // 數據
    private int hp; //血量
    private int damage; // 攻擊力
    [SerializeField, Range(0, 10)] private float speed; // 移速

    void Awake()
    {
        hp = data.hp;
        damage = data.damage;
        speed = data.speed;
    }

    void Start()
    {
    }

    void Update()
    {
        
    }

    
    void FixedUpdate()
    {
        if(target == null) return; // 不存在目標時
        
        Vector3 direction = (target.position - transform.position).normalized;
        rgdbd.velocity = direction * speed;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Attack();
        }
    }

    // 攻擊動作
    void Attack()
    {
        Debug.Log("Enemy Attack!!!");
    }

    // 受到攻擊
    public void TakeDamage(int damage)
    {
        Debug.Log("Enemy TakeDamage!!!");
    }
}
