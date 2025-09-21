using System;
using UnityEngine;

public class EnemyScript : MonoBehaviour, IDamageable
{
    public IDamageable player;
    public void SetTargetDamageable(IDamageable gb) => player = gb;
    [NonSerialized] public Action<GameObject> OnDeath;

    [SerializeField] private Transform target; // 移動目標
    public void SetTarget(Transform gb) => target = gb;
    [SerializeField] private string targetAttack; // 攻擊目標
    [SerializeField] private ItemDropComponent dropItem; // 掉落物

    // 內部物件
    [SerializeField] private Rigidbody2D rgdbd; // 剛體
    [SerializeField] private EnemyScriptable data; // 數據
    public void SetEnemyData(EnemyScriptable enemyData) => data = enemyData;
    [SerializeField] private BoxCollider2D boxCollider; // 碰撞體
    [SerializeField] private SpriteRenderer sprite; // 圖片
    [SerializeField] private Animator animator; // 動畫

    private int hp = 100; //血量
    private int damage = 10; // 攻擊力
    [SerializeField, Range(0, 10)] private float speed; // 移速

    void Awake()
    {
        if (data) LoadEnemyData();
    }

    void FixedUpdate()
    {
        if (target == null) return; // 不存在目標時

        Vector3 direction = (target.transform.position - transform.position).normalized;
        rgdbd.velocity = direction * speed;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Attack(player);
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
        if (hp < 1)
        {
            OnDeath?.Invoke(gameObject);
            dropItem.HandleDropItem();
            Debug.Log($"Enemy is Dead!!!");
        }
    }

    public void LoadEnemyData()
    {
        SpriteRenderer newEnemySprite = sprite;
        newEnemySprite.sprite = data.sprite; // 設置敵人圖片

        Animator newEnemyAnimator = animator;
        newEnemyAnimator.runtimeAnimatorController = data.animator; // 設置敵人動畫

        BoxCollider2D newEnemyCollider = boxCollider;
        newEnemyCollider.offset = data.offset; // 設置敵人碰撞偏移
        newEnemyCollider.size = data.size; // 設置敵人碰撞大小

        ItemDropComponent newEnemyDrop = dropItem;
        newEnemyDrop.dropItemPrefab = data.drop; // 設置敵人掉落物

        hp = data.hp;
        damage = data.damage;
        speed = data.speed;
    }
}
