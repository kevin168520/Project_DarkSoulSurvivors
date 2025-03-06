using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    // 外部
    [SerializeField] protected WeaponScriptable weaponData; // 武器數據
    [System.NonSerialized] public IDirection direction; // 方向組件

    // 內部
    float timer; // 計時用

    void Start()
    {
        Transform parent = transform.parent;

        // 父物件中查找 DirectionComponent
        direction = parent.GetComponent<IDirection>();
    }

    
    // 計時攻擊頻率
    void Update(){
      timer -= Time.deltaTime;
      if(timer < 0f)
      {
        Attack();
        timer = weaponData.timeToAttack;
      }
    }

    // 子類取得當前傷害倍率
    // void GetDamage() {
    //   // virtual SetData(WeaponData wd) 設定武器數據
    //   weaponData = wd;
    //   timeToAttack = weaponData.stats.timeToAttack;
    //   weaponStats = new WeaponStats(wd.stats.damage, wd.stats.timeToAttack)
    // }

    // 子類實作攻擊
    abstract protected void Attack();
}