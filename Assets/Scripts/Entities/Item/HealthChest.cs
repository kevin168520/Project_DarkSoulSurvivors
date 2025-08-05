using UnityEngine;

/// <summary>
/// 撿到後回復玩家 HP，並自動銷毀自己的寶箱腳本
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class HealthChest : MonoBehaviour
{
    [Header("拾取後回復血量")]
    public int healAmount = 20;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<CharacterScript>();
        if (player != null)
        {
            player.AddHp(healAmount);
            // TODO: 可以在這裡加音效、特效
            Destroy(gameObject);
        }
    }
}
