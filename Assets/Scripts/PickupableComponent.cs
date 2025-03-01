using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupableComponent : MonoBehaviour
{
    
    Vector3 target => GameManager.instance.playerTransform.position; // 目標
    bool pickupable; // 啟動是否被撿拾
    public void EnPickupable() => pickupable = true;

    void Start()
    {
    }

    void Update(){
      if(pickupable){
        // 被撿拾 持續靠近目標
        transform.position = Vector3.MoveTowards(transform.position, target, 0.1f);
      }
    }
    
    void OnTriggerEnter2D(Collider2D collision){
      if(collision.tag == "Player") {
        // 被撿拾的處理
        Destroy(gameObject);
      }
    }
}
