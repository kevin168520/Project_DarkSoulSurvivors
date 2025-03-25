using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour, IDamageable
{
  
    [SerializeField] private GameObject target; // 移動目標
    public void SetTarget(GameObject gb) => target = gb;
    [SerializeField] private string targetAttack; // 攻擊目標
    [SerializeField] private ItemDropComponent dropItem; // 掉落物

    // 內部物件
    [SerializeField] private Rigidbody2D rgdbd; // 剛體
    [SerializeField] private EnemyScriptable data; // 數據
    public void SetEnemyData(EnemyScriptable enemyData) => data = enemyData;

    private int hp = 100; //血量
    private int damage = 10; // 攻擊力
    [SerializeField, Range(0, 10)] private float speed; // 移速

    void Awake()
    {
        hp = data.hp;
        damage = data.damage;
        speed = data.speed;
        dropItem = GetComponent<ItemDropComponent>();
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
        
        Vector3 direction = (target.transform.position - transform.position).normalized;
        rgdbd.velocity = direction * speed;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
      
        if(collision.gameObject.CompareTag("Player"))
        {
            Attack(GameManager.instance.playerCharacter);
        }
    }

    // 攻擊動作
    void Attack(GameObject traget) => Attack(target.GetComponent<IDamageable>());
    void Attack(IDamageable targetDamageable)
    {
        targetDamageable?.TakeDamage(damage);
    }

    // 受到攻擊
    public void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log($"Enemy TakeDamage: {damage}, HP: {hp}");
        
        // 血量歸零 死亡處理
        if(hp < 1) 
        {
          Destroy(gameObject);
          dropItem.HandleDropItem();
          Debug.Log($"Enemy is Dead!!!");
        }
    }
}
