using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PlayerScript : MonoBehaviour
{
    [SerializeField] public Transform trans; // 玩家座標
    [SerializeField] public CharacterScript character; // 玩家狀態
    [SerializeField] public PlayerMoveComponent move; // 玩家移動
    [SerializeField] public PlayerPickupComponent pickup; // 玩家撿拾
    [SerializeField] public PlayerAnimationComponent anim; // 玩家Aniamte & Shader控制
    public CharacterScriptable characterData;
    public PlayerStoreData storeData;

    #if UNITY_EDITOR
    void OnValidate() // 編輯器中繪製 attackSize
    {
        if (trans == null)
            trans = base.transform;
        if (character == null)
            character = GetComponent<CharacterScript>();
        if (move == null)
            move = GetComponent<PlayerMoveComponent>();
        if (pickup == null)
            pickup = GetComponent<PlayerPickupComponent>();
        if (anim == null)
            anim = GetComponent<PlayerAnimationComponent>();
    }
    #endif
}
