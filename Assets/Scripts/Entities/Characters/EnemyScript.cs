using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyScript : MonoBehaviour, IDamageable
{
    [NonSerialized] private IDamageable player;
    [NonSerialized] public Action<GameObject> OnDeath;

    // 內部物件
    [SerializeField] private SpriteRenderer sprite; // 圖片
    [SerializeField] private Animator animator; // 動畫
    [SerializeField] private Rigidbody2D rgdbd; // 剛體
    [SerializeField] private BoxCollider2D boxCollider; // 碰撞體
    [SerializeField] private ItemDropComponent dropItem; // 掉落物

    [Title("敵人數據")]
    [SerializeField] private EnemyScriptable data; // 數據
    [SerializeField] private int hp = 100; //血量
    [SerializeField] private int damage = 10; // 攻擊力
    [SerializeField] private float speed; // 移速
    [SerializeField] private Transform target; // 移動目標

    public void SetTargetDamageable(IDamageable gb) => player = gb;
    public void SetTarget(Transform gb) => target = gb;
    public void SetEnemyData(EnemyScriptable enemyData) => data = enemyData;

    void Awake()
    {
        if (data) LoadEnemyData(); // 提供測試用
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

    // 載入敵人資料
    [Button("載入 Scriptable 資料")]
    public void LoadEnemyData()
    {
        // 圖片
        sprite.sprite = data.sprite;
        // 動畫
        animator.runtimeAnimatorController = data.animator;
        // 碰撞
        boxCollider.offset = data.offset;
        boxCollider.size = data.size;
        // 掉落
        dropItem.dropItemPrefab = data.drop;
        // 數值
        hp = data.hp;
        damage = data.damage;
        speed = data.speed;
    }

#if UNITY_EDITOR
    [Button("保存 Scriptable 資料")]
    private void SaveData()
    {
        if (data == null) return;

        data.hp = hp;
        data.damage = damage;
        data.speed = speed;
        data.sprite = sprite.sprite;
        data.animator = animator.runtimeAnimatorController;
        data.offset = boxCollider.offset;
        data.size = boxCollider.size;
        data.drop = dropItem.dropItemPrefab;

        UnityEditor.EditorUtility.SetDirty(data);
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
        Debug.Log($"已保存 {data.name} 到資產區");
    }
#endif
}
