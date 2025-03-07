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
    TimerUtility activeCounter = new TimerUtility(1f); // 攻擊持續計時
    TimerUtility frameCounter = new TimerUtility(6f, true); // 跳偵優化處理

    public void SetActiveInterval(float f) {
      activeCounter.SetTimeInterval(f);
    }

    // 開啟攻擊
    virtual public void OnEnable() {
      activeCounter.Reset();
    }

    // 攻擊計時
    virtual public void LateUpdate() {
      if (activeCounter.UpdateDelta()) {
        gameObject.SetActive(false);
      }
    }

    // 捕抓敵人
    virtual public void Update(){
      BeforeUpdate();
      if (frameCounter.UpdateFrame())
      {
        Collider2D[] collisions = Physics2D.OverlapBoxAll(  // 捕抓範圍內的碰撞
            transform.position, localAttackSize, 0f);

        foreach(Collider2D collision in collisions)
          if(CheckCollider(collision))ApplyDamage(collision); 
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
