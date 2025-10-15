using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HealthChest : MonoBehaviour
{
    

    // 新增事件
    public event Action PickedUp;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<CharacterScript>();
        if (player == null) return;

        // 改為回滿HP
         int hpToRestore = player.maxHp - player.currentHp;
        if (hpToRestore > 0)
        {
            player.AddHp(hpToRestore);
        }

        // 播放血包專用音效
        if (AudioGlobalManager.Instance != null)
            AudioGlobalManager.Instance.PlaySFX(enAudioSfxData.HealthPickup);

        // 通知外部
        PickedUp?.Invoke();

        Destroy(gameObject);
    }

    public void onPickedUp()
    {
        if (AudioGlobalManager.Instance != null)
            AudioGlobalManager.Instance.PlaySFX(enAudioSfxData.HealthPickup);

        PickedUp?.Invoke();
    }
}