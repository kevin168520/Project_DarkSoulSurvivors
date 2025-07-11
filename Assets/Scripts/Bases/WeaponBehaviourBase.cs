using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class WeaponBehaviourBase : AttackBehaviourBase
{
    // 攻擊監聽
    public System.Func<Collider2D, bool> OnAttackEvent;
    // 武器能力
    public float FlightSpeed; // 飛行攻擊速度
    public Vector3 FlightDirection; // 飛行攻擊方向

    //播放音樂
    public void PlaySound(enAudioSfxData enAudioSfxData)
    {
      AudioGlobalManager.Instance.PlaySFX(enAudioSfxData);
    }

    // 設置武器能力 透過 WeaponBase
    public void ApplyWeaponStats(WeaponHandlerBase d) {
      FlightSpeed = d.flightSpeed;
      activeInterval = d.activeInterval;
    }

    // 設置武器能力 透過 WeaponScriptable
    public void ApplyWeaponStats(WeaponScriptable d) {
      FlightSpeed = d.flightSpeed;
      activeInterval = d.activeInterval;
    }
}
