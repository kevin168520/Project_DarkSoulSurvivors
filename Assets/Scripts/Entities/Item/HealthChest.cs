using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HealthChest : MonoBehaviour
{
    public int healAmount = 20;

    // 新增事件（避免與方法同名）
    public event Action PickedUp;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<CharacterScript>();
        if (player == null) return;

        player.AddHp(healAmount);

        // 播放既有全域音效
        if (AudioGlobalManager.Instance != null)
            AudioGlobalManager.Instance.PlaySFX(enAudioSfxData.ItemPickup);

        // 通知外部
        PickedUp?.Invoke();

        Destroy(gameObject);
    }

    // 保留你之前需要的「可被外部直接呼叫」的方法
    public void onPickedUp()
    {
        if (AudioGlobalManager.Instance != null)
            AudioGlobalManager.Instance.PlaySFX(enAudioSfxData.ItemPickup);

        PickedUp?.Invoke();
    }
}
