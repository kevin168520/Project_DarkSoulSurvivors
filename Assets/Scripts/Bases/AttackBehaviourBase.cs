using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 實作攻擊範圍內的捕抓對象，子類實現對象的處理
/// </summary>
public abstract class AttackBehaviourBase : MonoBehaviour
{
    // 屬性
    public int Attack; // 攻擊值
    public float AttackInterval; // 攻擊間格
    public float ActiveInterval{ // 攻擊持續
      get => activeCounter.GetTimeInterval(); 
      set => activeCounter.SetTimeInterval(value);
    }
    TimeCounter activeCounter = new TimeCounter(1f); // 攻擊持續計時
    public float FlightSpeed; // 飛行攻擊速度
    public Vector3 FlightDirection; // 飛行攻擊方向

    // 功能
    TargetDetector targetDetector = new TargetDetector(new Vector2(1, 1)); // 捕抓範圍
    TimeCounter frameCounter = new TimeCounter(6f, true); // 跳偵優化處理
    public AudioSource audioSource; // 音效
    
    // 播放音樂
    public void PlaySound() {
        if (audioSource != null) audioSource.Play();
    }

    // 開啟攻擊
    virtual public void OnEnable() {
      OnAttackStart();
      activeCounter.Reset();
    }

    // 攻擊計時
    virtual public void LateUpdate() {
      if (activeCounter.UpdateDelta()) {
        OnAttackEnd();
      }
    }

    // 攻擊時間結束
    public void ActiveStop() => activeCounter.Update(activeCounter.GetTimeInterval());

    // 捕抓敵人
    virtual public void Update(){
      OnUpdateStart();
      if (frameCounter.UpdateFrame())
      {
        targetDetector.DetectTargets(transform, HandleTargets);
      }
    }

    // 提供子類處理捕抓
    virtual protected void HandleTargets(Collider2D collider) => ApplyAttack(collider); 

    // 提供子類 Update()
    virtual protected void OnUpdateStart(){}

    // 子類實作結束
    abstract protected void OnAttackStart();

    // 子類實作攻擊
    abstract protected void ApplyAttack(Collider2D collider);

    // 子類實作結束
    abstract protected void OnAttackEnd();
    

#if DEBUG
    private void OnDrawGizmosSelected() // 編輯器中繪製 attackSize
    {
        targetDetector.DrawDetectionGizmo(transform);
    }
#endif
}
