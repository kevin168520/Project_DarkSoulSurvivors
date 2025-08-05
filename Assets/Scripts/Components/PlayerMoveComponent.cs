using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveComponent : MonoBehaviour, IDirection
{
    [Header("Movement")]
    public float moveSpeed;
    private Rigidbody2D rb;
    private Vector2 moveDir;    // 移動方向
    private Vector2 prevMoveDir = Vector2.zero; // 上一幀移動方向

    Vector3 currentPosition = Vector3.right; // 當前面對的方向
    public Vector3 Length {get; private set;} = Vector3.zero; // 移動的向量長度
    public Vector3 Normalized {get; private set;} = Vector3.right; // 最終的方向，預定定義為right，設為zero武器在角色未移動時會無法射出

    [Header("Animation")]
    [SerializeField] private Animator ani; // Animator元件
    [SerializeField] private SpriteRenderer spriteRenderer; // 角色圖像
    [SerializeField] private bool isFacingLeft = false; // 紀錄動畫面向

    void Start() {
        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(WaitForCharacterModel());
    }
    /// <summary> 撈取Charater身上的Animator與SpriteRender資訊 </summary>
    private IEnumerator WaitForCharacterModel() {
        while (true) {
            foreach (Transform child in transform) {
                if (child.name.StartsWith("Character_")) {
                    spriteRenderer = child.GetComponent<SpriteRenderer>();
                    ani = child.GetComponent<Animator>();
                    yield break;    // 找到後結束
                }
            }
            yield return null;      // 等下一幀再檢查
        }
    }

    void Update() {
        bool bDirX = Mathf.Approximately(moveDir.x, prevMoveDir.x);
        bool bDirY = Mathf.Approximately(moveDir.y, prevMoveDir.y);
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDir = new Vector2(moveX, moveY);

        // 兩幀之間都沒有方便變化，則不進行方向更新
        if (!bDirX || !bDirY) {
            HandleInputAndDirection();
            prevMoveDir = moveDir; // 更新上一幀方向資訊
        }
    }
    
    void FixedUpdate() {
        Move();
    }

    /// <summary> 角色最後面對的方向判斷與Animator動作處理 </summary>
    private void HandleInputAndDirection()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDir = new Vector2(moveX, moveY);

        // 角色最後面對的方向判斷
        if (moveDir.sqrMagnitude > 0.01f)
        {
            Vector3 currentDir = new Vector3(moveDir.x, moveDir.y, 0f);
            Length = currentDir;

            // 僅在 moveX != 0 時更新方向
            if (Mathf.Abs(moveX) > 0.01f)
                currentPosition = new Vector3(moveX, 0f, 0f).normalized;
        }
        Normalized = currentPosition;

        // Animator鏡像處理
        bool isFacingLeftNow = currentPosition.x < 0f;
        if (isFacingLeftNow != isFacingLeft) {
            isFacingLeft = isFacingLeftNow;

            if (spriteRenderer != null)
                spriteRenderer.flipX = isFacingLeft;    // 向左為true
        }
    }

    private void Move() {
        // 沒有變化就立即停止角色防止滑行
        if (moveDir.sqrMagnitude > 0.01f) {
            rb.velocity = moveDir * moveSpeed;
        }
        else {
            rb.velocity = Vector2.zero;
        }
    }
}