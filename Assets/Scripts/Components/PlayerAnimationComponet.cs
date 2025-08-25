using System.Collections;
using UnityEngine;

public class PlayerAnimationComponent : MonoBehaviour
{
    // 從全域 PlayerManager.PLAYER 取得方向元件 (IDirection)
    private IDirection direction => PlayerManager.PLAYER.move;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

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
                    animator = child.GetComponent<Animator>();
                    spriteRenderer = child.GetComponent<SpriteRenderer>();
                    if (animator != null)
                        originalScale = animator.transform.localScale;

                    yield break;
                }
            }
            yield return null;
        }
    }

    private void Update()
    {
        if (direction != null && animator != null)
            AnimatorAction();
    }

    /// <summary> Animator動作處理 </summary>
    private void AnimatorAction()
    {
        Vector2 currentPosition = direction.Normalized;

        bool isFacingLeftNow = currentPosition.x < 0f;
        if (isFacingLeftNow != isFacingLeft)
        {
            isFacingLeft = isFacingLeftNow;

            if (spriteRenderer != null)
            {
                // 不用 flipX，改用 localScale
                var scale = animator.transform.localScale;
                scale.x = isFacingLeft ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
                animator.transform.localScale = scale;
            }
        }
    }
}