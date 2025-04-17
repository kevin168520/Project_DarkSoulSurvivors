using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupComponent : MonoBehaviour
{
    [Header("撿拾時間")]
    [SerializeField] TimeCounter timer = new TimeCounter(0.2f, true);
    [Header("撿拾範圍")]
    [SerializeField] TargetDetector detector = new TargetDetector(new Vector2(2f, 2f), false); // 捕抓範圍

    void Update(){
      // 跳禎觸發撿拾範圍
      if(timer.UpdateDelta()){
        HandlePickup();
      }
    }

    void HandlePickup(){
      // 捕抓範圍內的碰撞
      detector.DetectTargets(transform, (collider) => {
        if(collider.CompareTag("Item")){
          if(collider.GetComponent<ItemPickupableComponent>() is ItemPickupableComponent e){
            // 啟動對象的撿拾中
            e.EnPickupable(transform);
          }
        }
      });
    }


#if DEBUG
    private void OnDrawGizmosSelected() // 編輯器中繪製
    {
        detector.DrawDetectionGizmo(transform, Color.green);
    }
#endif
}
