using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private Rigidbody2D rb;
    private Vector2 moveDir;//移動方向
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        InputManagement();
    }
    private void FixedUpdate()
    {
        Move();
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
