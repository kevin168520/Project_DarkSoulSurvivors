using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponScript : WeaponBase
{
    [SerializeField] private GameObject projectile;

    // 執行攻擊
    override protected void Attack()
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
      GameObject projectileGO = Instantiate(projectile);
      projectileGO.transform.position = transform.position;
      projectileGO.transform.parent = transform;
      projectileGO.SetActive(true);
      
      WeaponBehaviourBase projectileBehaviour = projectileGO.GetComponent<WeaponBehaviourBase>();
      projectileBehaviour.SetWeaponData(weaponData);
      projectileBehaviour.SetDirection(direction.Normalized);

      float angle = Mathf.Atan2(direction.Normalized.y, direction.Normalized.x) * Mathf.Rad2Deg;
      projectileBehaviour.transform.rotation = Quaternion.Euler(0, 0, angle);

    }
}
