using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TargetDetector {
    [SerializeField] private Vector2 detectionSize; // 攻擊範圍

    public TargetDetector() {
        this.detectionSize = new Vector2(1, 1);
    }
    public TargetDetector(Vector2 detectionSize) {
        this.detectionSize = detectionSize;
    }

    /// <summary>
    /// 在範圍內偵測目標，並對每個符合的目標執行回調函式
    /// </summary>
    public void DetectTargets(Transform detectorOrigin, System.Action<Collider2D> onTargetDetected) {
        if (detectorOrigin == null || onTargetDetected == null) return;

        Vector2 adjustedSize = detectionSize * detectorOrigin.localScale;
        Collider2D[] targets = Physics2D.OverlapBoxAll(detectorOrigin.position, adjustedSize, 0f);

        foreach (var target in targets) onTargetDetected(target);
    }

#if UNITY_EDITOR
    public void DrawDetectionGizmo(Transform detectorOrigin) {
        if (detectorOrigin == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(detectorOrigin.position, detectionSize * detectorOrigin.localScale);
    }
#endif
}
