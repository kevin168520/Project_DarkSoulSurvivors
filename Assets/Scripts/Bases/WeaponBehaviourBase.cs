using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBehaviourBase : AttackBehaviourBase
{
    public void SetWeaponData(WeaponScriptable d) {
      Attack = d.attack;
      FlightSpeed = d.flightSpeed;
      ActiveInterval = d.activeInterval;
    }

    // 檢查對象
    override protected bool CheckCollider(Collider2D collider){
      if(collider.CompareTag("Enemy")) return true;
      return false;
    }
}
