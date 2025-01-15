using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 實作攻擊範圍內的捕抓對象，子類實現對象的處理
/// </summary>
public abstract class AttackBehaviourBase : MonoBehaviour
{

    // 範圍
    [SerializeField] protected Vector2 attackSize = new Vector2(1, 1); // 攻擊範圍
    Vector2 localAttackSize {get => attackSize * transform.localScale;} // 攻擊範圍 * 物件縮放 配合放大用

    // 頻率
    [SerializeField] protected float timeToDisable; //攻擊持續時間
    float timer; // 維持時間
    int frameInterval = 6; // 跳偵處理
    int frameCounter = 0;


    // 開啟攻擊
    virtual public void OnEnable() {
      timer = timeToDisable;
    }

    // 攻擊計時
    virtual public void LateUpdate() {
      timer -= Time.deltaTime;
      if(timer < 0f)  
        gameObject.SetActive(false);
    }

    // 捕抓敵人
    virtual public void Update(){
      BeforeUpdate();
      frameCounter++;
      if (frameCounter >= frameInterval)
      {
        Collider2D[] collisions = Physics2D.OverlapBoxAll(  // 捕抓範圍內的碰撞
            transform.position, localAttackSize, 0f);

        foreach(Collider2D collision in collisions)
          if(CheckCollider(collision))ApplyDamage(collision); 
        
        frameCounter = 0;
      }
    }
    // 子類實作捕抓敵人前動作
    virtual protected void BeforeUpdate(){}

    // 子類實作攻擊
    abstract protected void ApplyDamage(Collider2D collider);
    abstract protected bool CheckCollider(Collider2D collider);
    

#if DEBUG
    private void OnDrawGizmosSelected() // 編輯器中繪製 attackSize
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, localAttackSize);
    }
#endif
}
