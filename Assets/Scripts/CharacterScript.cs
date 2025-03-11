using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterScript : MonoBehaviour, IDamageable
{
    public enum StatType{
        Level,
        TotalExp,
        ExpToLevelUp,
        MaxHp,
        Def,
        SpeedMult,
        AttackMult,
        CurrentHp,
        isDead,
        Invincibility
    }
    
    // 內部
    [SerializeField] public int level = 1; // 等級
    [SerializeField] public int levelUpExp = 10; // 升級所需經驗值
    [SerializeField] public int totalExp = 0; // 累積經驗值
    [SerializeField] public int maxHp = 100; // 最大血量
    [SerializeField] public int def = 0; // 防禦
    [SerializeField] public float speedMult = 1; // 速度倍率
    [SerializeField] public float attackMult = 1; // 攻擊倍率
    [SerializeField] public int currentHp = 100; // 當前血量
    bool isDead = false;  // 死亡判定
    [SerializeField] TimeCounter invincibilityCounter = new TimeCounter(1f); // 無敵時間
    public UnityEvent<StatType> dataChangeListener = new UnityEvent<StatType>(); // 資料變更監聽者
    
    public void AddExp(int exp) {
      totalExp += exp;

      // 檢查經驗值達到升級
      if(totalExp >= levelUpExp) {
        totalExp -= levelUpExp;
        LevelUp();
      }

      dataChangeListener.Invoke(StatType.TotalExp);
    }
    public void LevelUp() {
      level++;
      levelUpExp = level * 10;

      dataChangeListener.Invoke(StatType.Level);
    }

    void Update()
    {
        invincibilityCounter.UpdateDelta();
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
        if(invincibilityCounter.IsTiming) 
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
        invincibilityCounter.Reset();
        }
    
        Debug.Log($"Character TakeDamage: {damage} - {def}, currentHp: {currentHp}, CharacterIsDead: {isDead}");
  }

}
