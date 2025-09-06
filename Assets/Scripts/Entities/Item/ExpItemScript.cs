using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpItemScript : ItemPickupableComponent
{
    [SerializeField] int exp = 1;

    void Start()
    {
        // 設定經驗值物品的音效
        pickupSfx = enAudioSfxData.ExpPickup;
    }

    override protected void OnPickup(Collider2D collision)
    {
        PlaySound(pickupSfx); // 播放音效
        EventGlobalManager.Instance.InvokeEvent(new ExpEvent() { exp = exp });
        Destroy(gameObject);
    }
}