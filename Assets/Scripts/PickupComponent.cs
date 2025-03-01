using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupComponent : MonoBehaviour
{

    float timeCounter; // 計時器
    float timeInterval = 0.2f; // 觸發時間
    [Range(0f, 20f)]public float range = 4f; // 撿拾距離


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update(){
      timeCounter -= Time.deltaTime; // 跳禎觸發撿拾範圍
      if(timeCounter <= 0){
        timeCounter = timeInterval;
        CheckPickup();
      }
    }

    void CheckPickup(){
      // 捕抓範圍內的碰撞
      Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, range);
      
      // 判定對象是否為撿拾
      foreach(var collider in collisions){
        if(collider.CompareTag("Item")){
          if(collider.GetComponent<PickupableComponent>() is PickupableComponent e){
            // 啟動對象的撿拾中
            e.EnPickupable();
          }
        }
      }
    }


#if DEBUG
    private void OnDrawGizmosSelected() // 編輯器中繪製
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
#endif
}
