using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, DirectionComponent
{
    [Header("Movement")]
    public float moveSpeed;
    private Rigidbody2D rb;
    private Vector2 moveDir;//移動方向
    
    Vector3 currentPosition = Vector3.zero;
    public Vector3 Length { get; private set; } = Vector3.right; // 移動的向量長度
    public Vector3 Normalized { get; private set; } = Vector3.right; // 移動的方向 (單位向量)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        InputManagement();
    }
    
    void FixedUpdate()
    {
        Move();
    }
    
    void LateUpdate ()
    {

        // 取得玩家輸入方向
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector3 v  = new Vector2(moveX, moveY);

        // 紀錄物件的移動面向
        // Vector3 v = transform.position - currentPosition;
        if(v.sqrMagnitude > Mathf.Epsilon){
          Length = v;
          Normalized = v.normalized;
        }
        currentPosition = transform.position;
    }

    private void InputManagement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        moveDir = new Vector2(moveX, moveY).normalized;
        
    }
    private void Move()
    {
        rb.velocity = new Vector2(moveDir.x*moveSpeed, moveDir.y*moveSpeed);
    }
}
