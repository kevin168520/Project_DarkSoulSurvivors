using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour, IDamageable
{
    // 內部
    [SerializeField] public int maxHp = 100; // 最大血量
    [SerializeField] public int def = 0; // 防禦
    [SerializeField] public float speedMult = 1; // 速度倍率
    [SerializeField] public float attackMult = 1; // 攻擊倍率
    [SerializeField] public int currentHp = 100; // 當前血量
    bool isDead = false;  // 死亡判定

    // 受攻擊時間
    [SerializeField] protected float attackedTimerToDisable; // 無敵時間
    TimeCounter attackedTimer = new TimeCounter(1f);

    void Update()
    {
        attackedTimer.UpdateDelta();
    }


  // 執行受傷
    public void TakeDamage(int damage)
    {
        // 如果已死亡 則不動作
        if(isDead) 
        {
        Debug.Log($"Character isDead: {isDead}");
        return; 
        }

        // 如果還在無敵時間 則不動作
        if(attackedTimer.IsTiming) 
        {
        // Debug.Log($"Character is attackedTimerToDisable: {attackedTimer}");
        return; 
        }

        // 如果傷害低於防禦則不損傷
        if(def >= damage ) {
        // Debug.Log($"Character no harm caused: {def} >= {damage}");
        return; 
        }

        // 當前血量扣損
        currentHp -= damage;
    
        // 死亡處理
        if(currentHp <= 0)
        {
        isDead = true;
        GameManager.instance.GameOver();
        }
        else
        { 
        // 受攻擊時間處理
        attackedTimer.Reset();
        }
    
        Debug.Log($"Character TakeDamage: {damage} - {def}, currentHp: {currentHp}, CharacterIsDead: {isDead}");
  }

}
