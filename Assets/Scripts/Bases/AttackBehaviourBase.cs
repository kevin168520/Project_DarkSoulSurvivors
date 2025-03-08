using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 實作攻擊範圍內的捕抓對象，子類實現對象的處理
/// </summary>
public abstract class AttackBehaviourBase : MonoBehaviour
{
    // 屬性
    private int _attack; // 攻擊值
    public int Attack{ get => _attack; set => _attack = value;}
    private float _attackInterval; // 攻擊間格
    public float AttackInterval{ get => _attackInterval; set => _attackInterval = value;}
    private float _activeInterval; // 攻擊持續
    public float ActiveInterval{ get => _activeInterval; set {_activeInterval = value; activeCounter.SetTimeInterval(value);}}
    private float _flightSpeed; // 飛行攻擊速度
    public float FlightSpeed{ get => _flightSpeed; set => _flightSpeed = value;}
    private Vector3 _flightDirection; // 飛行攻擊方向
    public Vector3 FlightDirection{ get => _flightDirection; set => _flightDirection = value;}

    // 範圍
    [SerializeField] TargetDetector targetDetector = new TargetDetector(new Vector2(1, 1));

    // 頻率
    TimeCounter activeCounter = new TimeCounter(1f); // 攻擊持續計時
    TimeCounter frameCounter = new TimeCounter(6f, true); // 跳偵優化處理

    // 音效
    public AudioSource audioSource;
    
    // 播放音樂
    public void PlaySound() {
        if (audioSource != null) audioSource.Play();
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
        targetDetector.DetectTargets(transform, collision => {
            if(CheckCollider(collision))ApplyDamage(collision); 
        });
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
        targetDetector.DrawDetectionGizmo(transform);
    }
#endif
}
