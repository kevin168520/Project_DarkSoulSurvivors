using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionComponent : MonoBehaviour
{
    Vector3 currentPosition;
    public Vector3 Length { get; private set; } = Vector3.right; // 移動的向量長度
    public Vector3 Normalized { get; private set; } = Vector3.right; // 移動的方向 (單位向量)

    void OnEnable()
    {
      currentPosition = transform.position;
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
}
