using UnityEngine;

public class GoldItemScript : ItemPickupableComponent
{
    [SerializeField] int gold = 1;

    void Start()
    {
        // 設定金幣物品的音效
        pickupSfx = enAudioSfxData.GoldPickup;
    }

    override protected void OnPickup(Collider2D collision)
    {
        PlaySound(pickupSfx); // 播放音效
        EventGlobalManager.Instance.InvokeEvent(new GoldEvent() { gold = gold });
        Destroy(gameObject);
    }
}