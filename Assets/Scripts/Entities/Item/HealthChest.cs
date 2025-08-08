using UnityEngine;
using System;

[RequireComponent(typeof(Collider2D))]
public class HealthChest : MonoBehaviour
{
    [Header("拾取後回復血量")]
    public int healAmount = 20;

    [HideInInspector]
    public Action<GameObject> onPickedUp; // 寶箱被撿起時通知

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<CharacterScript>();
        if (player != null)
        {
            player.AddHp(healAmount);

            // 觸發回呼
            onPickedUp?.Invoke(gameObject);

            Destroy(gameObject);
        }
    }
}
