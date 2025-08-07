using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 角色Aniamtor、SpriteRender的Interface </summary>
public interface ICharacterVisual
{
    Animator Animator { get; }
    SpriteRenderer SpriteRenderer { get; }
}