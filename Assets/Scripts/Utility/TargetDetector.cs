using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TargetDetector {
    private bool overlapType;
    [SerializeField] private Vector2 range; // 攻擊範圍

    public TargetDetector() {
        this.range = new Vector2(1, 1);
    }
    public TargetDetector(Vector2 size, bool overlapType = true) {
        this.range = size;
        this.overlapType = overlapType;
    }

    /// <summary>
    /// 在範圍內偵測目標，並對每個符合的目標執行回調函式
    /// </summary>
    public void DetectTargets(Transform detectorOrigin, System.Action<Collider2D> onTargetDetected) {
        if (detectorOrigin == null || onTargetDetected == null) return;

        Vector2 adjustedSize = range * detectorOrigin.localScale;
        Collider2D[] targets = overlapType ? 
          Physics2D.OverlapBoxAll(detectorOrigin.position, adjustedSize, 0f):
          Physics2D.OverlapCircleAll(detectorOrigin.position, adjustedSize.x);


        foreach (var target in targets) onTargetDetected(target);
    }

#if UNITY_EDITOR
    public void DrawDetectionGizmo(Transform origin) => DrawDetectionGizmo(origin, Color.red);
    public void DrawDetectionGizmo(Transform origin, Color color) {
        if (origin == null) return;

        Gizmos.color = color;
        if(overlapType)
          Gizmos.DrawWireCube(origin.position, range * origin.localScale);
        else
          Gizmos.DrawWireSphere(origin.position, range.x * origin.localScale.x);
    }
#endif
}
