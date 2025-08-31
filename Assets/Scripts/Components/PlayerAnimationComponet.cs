using System.Collections;
using UnityEngine;

public class PlayerAnimationComponent : MonoBehaviour
{
    // 從全域 PlayerManager.PLAYER 取得方向元件 (IDirection)
    private IDirection direction => PlayerManager.PLAYER.move;

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private Vector3 originalScale;
    private bool isFacingLeft = false;

    [Header("被擊閃爍設定")]
    public Color flickerColor = Color.white;
    public float flickerStep; // 閃爍間隔幾秒一次
    public int flickerCount;  // 閃爍次數 (一次 = 亮 + 暗)
    public float flickStrengthSetting;  // 閃爍強度(0 ~ 1f)
    private Material materialInstance;
    private int flickerStrengthID;
    private int flickerColorID;
    private bool isFlicker = false; // 當前材質是否具備 flicker 參數

    private void Start()
    {
        SetMaterial(spriteRenderer.material);
    }

    public void SetMaterial(Material material)
    {
        materialInstance = material;

        if (materialInstance != null)
        {
            // 資料連結ShaderGraph參數
            flickerStrengthID = Shader.PropertyToID("_FlickerStrength");
            flickerColorID = Shader.PropertyToID("_FlickerColor");

            // 檢查Shader資料是否有對應到
            isFlicker =
                materialInstance.HasProperty(flickerStrengthID) &&
                materialInstance.HasProperty(flickerColorID);

            if (isFlicker)
            {
                materialInstance.SetFloat(flickerStrengthID, 0f);
                materialInstance.SetColor(flickerColorID, flickerColor);
            }
            else
            {
                Debug.LogWarning("[PlayerAnimationComponent] 材質未包含 _FlickerStrength / _FlickerColor，將跳過閃爍控制。");
            }
        }
        else
        {
            Debug.LogWarning("[PlayerAnimationComponent] 材質設定失敗");
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

    /// <summary> 遭受攻擊時執行無敵閃爍 </summary>
    public void PlayHitFlicker()
    {
        if (!isFlicker || materialInstance == null) return;

        // 如果已經在跑，先停掉再重新跑
        StopCoroutine(nameof(FlickerCoroutine));
        StartCoroutine(FlickerCoroutine());
    }

    /// <summary> 閃爍執行 </summary>
    private IEnumerator FlickerCoroutine()
    {
        for (int i = 0; i < flickerCount; i++)
        {
            materialInstance.SetFloat(flickerStrengthID, flickStrengthSetting); // 亮
            yield return new WaitForSeconds(flickerStep * 0.5f);

            materialInstance.SetFloat(flickerStrengthID, 0f); // 暗（回原色）
            yield return new WaitForSeconds(flickerStep * 0.5f);
        }
    }

    /// <summary> 避免材質實例洩漏當物件被刪除時自動卸載 </summary>
    private void OnDestroy()
    {
        if (materialInstance != null)
        {
            // 避免材質實例洩漏
            if (Application.isPlaying)
                Destroy(materialInstance);
            else
                DestroyImmediate(materialInstance);
        }
    }
}