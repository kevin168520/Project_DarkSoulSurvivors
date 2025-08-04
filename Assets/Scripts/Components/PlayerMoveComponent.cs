using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveComponent : MonoBehaviour, IDirection
{
    [Header("Movement")]
    public float moveSpeed;
    private Rigidbody2D rb;
    private Vector2 moveDir;    //移動方向
    
    Vector3 currentPosition = Vector3.zero;
    //public Vector3 Length { get; private set; } = Vector3.right; // 移動的向量長度
    public Vector3 Length { get; private set; } = Vector3.zero; // 移動的向量長度
    public Vector3 Normalized { get; private set; } = Vector3.zero; // 移動的方向 (單位向量)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //currentPosition = transform.position;
    }

    void Update()
    {
        InputManagement();
    }
    
    void FixedUpdate()
    {
        Move();
    }
    
    //void LateUpdate ()
    //{
    //    // 紀錄物件的移動面向
    //    Vector3 delta = transform.position - currentPosition;

    //    if (delta.sqrMagnitude > Mathf.Epsilon)
    //    {
    //        Length = delta;
    //        Normalized = delta.normalized;
    //    }
    //    currentPosition = transform.position;
    //}

    private void InputManagement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDir = new Vector2(moveX, moveY).normalized;

        if (moveDir.sqrMagnitude > 0.01f)
        {
            // 立即更新方向紀錄
            Length = new Vector3(moveDir.x, moveDir.y, 0f);
            Normalized = Length.normalized;
        }
    }
    private void Move()
    {
        //rb.velocity = new Vector2(moveDir.x*moveSpeed, moveDir.y*moveSpeed);

        // 如果有輸入，推動角色；否則停止角色（防止滑行）
        if (moveDir.sqrMagnitude > 0.01f)
        {
            rb.velocity = moveDir * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
