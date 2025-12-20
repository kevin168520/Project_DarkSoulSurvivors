using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamageTextScript : MonoBehaviour
{
    [SerializeField] float time = 1;
    [SerializeField] Vector2 offset = new Vector2(0, 10f);
    Vector2 offscreen = new Vector2(9999, 9999);
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] RectTransform rectTransform;
    [SerializeField] TextMeshProUGUI text;

    void OnDestroy()
    {
        rectTransform.DOKill();
        canvasGroup.DOKill();
    }

    public void Play(string damage, Vector3 pos, Action Completed)
    {
        // 中斷
        rectTransform.DOKill();
        canvasGroup.DOKill();

        // 文字
        text.text = damage;

        // 位移
        rectTransform.position = pos;
        rectTransform.DOMoveX(rectTransform.position.x + offset.x, time);
        rectTransform.DOMoveY(rectTransform.position.y + offset.y, time);
        // 淡出
        canvasGroup.alpha = 1f;
        canvasGroup.DOFade(0f, time);

        // 完成
        DOVirtual.DelayedCall(time, () => Completed?.Invoke());
    }

    public void GetOut()
    {
        rectTransform.position = offscreen;
    }
}
