using System.Collections;
using UnityEngine;

public class PlayerAnimationComponent : MonoBehaviour, ICharacterVisual
{
    // 從全域 PlayerManager.PLAYER 取得方向元件 (IDirection)
    private IDirection direction => PlayerManager.PLAYER.move;

    // ICharacterVisual 介面實作
    public Animator Animator {get; private set;}
    public SpriteRenderer SpriteRenderer {get; private set;}

    private Vector3 originalScale;
    private bool isFacingLeft = false;

    private void Awake()
    {
        StartCoroutine(WaitForCharacterModel());
    }

    private IEnumerator WaitForCharacterModel()
    {
        while (true) {
            foreach (Transform child in transform) {
                if (child.name.StartsWith("Character_")) {
                    Animator = child.GetComponent<Animator>();
                    SpriteRenderer = child.GetComponent<SpriteRenderer>();
                    if (Animator != null)
                        originalScale = Animator.transform.localScale;
                    yield break;
                }
            }
            yield return null;
        }
    }

    private void Update()
    {
        if (direction != null && Animator != null)
            AnimatorAction();        
    }

    /// <summary> Animator動作處理 </summary>
    private void AnimatorAction()
    {
        Vector2 currentPosition = direction.Normalized;

        bool isFacingLeftNow = currentPosition.x < 0f;
        if (isFacingLeftNow != isFacingLeft) {
            isFacingLeft = isFacingLeftNow;

            SpriteRenderer.flipX = isFacingLeft; // 僅翻轉 sprite，無需操作 scale
        }
    }
}