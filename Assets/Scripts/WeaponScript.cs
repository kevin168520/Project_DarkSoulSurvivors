using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] private float timeToAttack = 1f; // 攻擊間隔
    [SerializeField] private Vector2 attackSize = new Vector2(1, 1); // 攻擊範圍
    private float timer; // 計時用
    
    void Start()
    {
        
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0f)
        {
            Attack();
            timer = timeToAttack;
        }
    }

    void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(  // 捕抓範圍內的碰撞
            transform.position, attackSize, 0f);
        ApplyDamage(colliders);
    }
    
    void ApplyDamage(Collider2D[] colliders)
    {
        foreach(Collider2D collision in colliders)
        {
            IDamageable e = collision.gameObject.GetComponent<IDamageable>();
            if(e != null)
            {
              e.TakeDamage(10);
            }
        }
    }

#if DEBUG
    private void OnDrawGizmosSelected() // 編輯器中繪製 attackSize
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, attackSize);
    }
#endif
}
