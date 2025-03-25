using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponScript : WeaponBase
{
    IDirection direction => GameManager.instance.playerDirection; // 方向組件
    [SerializeField] private WeaponBehaviourBase projectilePrefab;

    // 執行攻擊
    override protected void HandleAttack()
    {
        StartCoroutine(AttackProcess());
    }

    // 攻擊過程 會有一次攻擊複數子彈
    IEnumerator AttackProcess() {
      SpawnProjectile();
      yield return new WaitForSeconds(0.3f);
    }

    // 生成子彈物件
    void SpawnProjectile() {
      // 子彈場景狀態
      WeaponBehaviourBase projectile = Instantiate(projectilePrefab);
      projectile.transform.position = transform.position;
      // projectile.transform.parent = transform;
      projectile.gameObject.SetActive(true);
      
      // 子彈屬性
      projectile.ApplyWeaponStats(this);
      projectile.OnAttackEvent = ApplyDamage;
      projectile.FlightDirection = direction.Normalized;

      // 子彈方向
      float angle = Mathf.Atan2(direction.Normalized.y, direction.Normalized.x) * Mathf.Rad2Deg;
      projectile.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
