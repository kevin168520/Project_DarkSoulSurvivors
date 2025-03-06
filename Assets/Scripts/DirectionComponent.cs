using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DirectionComponent
{
    Vector3 Length { get; } // 移動的向量長度
    Vector3 Normalized { get; } // 移動的方向 (單位向量)
}
