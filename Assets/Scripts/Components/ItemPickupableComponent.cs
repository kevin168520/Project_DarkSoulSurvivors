using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupableComponent : MonoBehaviour
{
    Transform target; // 玩家目標
    bool pickupable; // 啟動是否被撿拾
    
    [Header("Audio Settings")]
    [SerializeField] protected enAudioSfxData pickupSfx = enAudioSfxData.ItemPickup; // 預設撿拾音效
    
    public void EnPickupable(Transform trans) {
      target = trans;
      pickupable = true;
    }

    void Start()
    {
    }

    void Update(){
      if(pickupable){
        // 被撿拾 持續靠近目標
        transform.position = Vector3.MoveTowards(transform.position, target.position, 0.1f);
      }
    }
    
    void OnTriggerEnter2D(Collider2D collision){
      if(collision.tag == "Player") {
        // 被撿拾的處理
        OnPickup(collision);
      }
    }

    // 子類實作被撿拾後的動作
    virtual protected void OnPickup(Collider2D collision){
        // 播放撿拾音效
        PlaySound(pickupSfx);
        Destroy(gameObject);
    }
    
    // 播放撿拾音效的方法
    public void PlaySound(enAudioSfxData enAudioSfxData)
    {
      AudioGlobalManager.Instance.PlaySFX(enAudioSfxData);
    }

}