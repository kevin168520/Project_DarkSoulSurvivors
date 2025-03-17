using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBehaviourBase : AttackBehaviourBase
{
    public void ApplyWeaponStats(WeaponBase d) {
      Attack = d.attack;
      FlightSpeed = d.flightSpeed;
      ActiveInterval = d.activeInterval;
    }
    public void ApplyWeaponStats(WeaponScriptable d) {
      Attack = d.attack;
      FlightSpeed = d.flightSpeed;
      ActiveInterval = d.activeInterval;
    }
}
