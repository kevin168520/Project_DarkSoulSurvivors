using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBehaviourBase : AttackBehaviourBase
{
    // protected string targetTag; // 目標
    protected int damage = 10; // 攻擊傷害
    protected float projectileSpeed; // 彈丸速度
    protected Vector3 projectileDirection; // 移動方向

    public void SetDirection(Vector3 direction) {
      projectileDirection = direction;
    }

    public void SetWeaponData(WeaponScriptable weaponData) {
      damage = weaponData.damage;
      projectileSpeed = weaponData.projectileSpeed;
      timeToDisable = weaponData.timeToDisable;
    }

    // 檢查對象
    override protected bool CheckCollider(Collider2D collider){
      if(collider.CompareTag("Enemy")) return true;
      return false;
    }
}
