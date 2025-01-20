using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour, IDamageable
{
  
  // 內部
  [SerializeField] int maxHp = 100;     // 最大血量
  int currentHp; // 當前血量
  bool isDead = false;  // 死亡判定

  // 受攻擊時間
  [SerializeField] protected float attackedTimerToDisable; // 無敵時間
  float attackedTimer; // 計時

  void Awake()
  {
      currentHp = maxHp;
  }

  void Start()
  {
  }

  void Update()
  {
    if(attackedTimer > 0f)  attackedTimer -= Time.deltaTime;
  }


  // 執行受傷
  public void TakeDamage(int damage)
  {
    // 如果已死亡 則不動作
    if(isDead) {
      Debug.Log($"Character isDead: {isDead}");
      return; 
    }

    // 如果還在無敵時間 則不動作
    if(attackedTimer > 0f ) {
      // Debug.Log($"Character is attackedTimerToDisable: {attackedTimer}");
      return; 
    }

    // 當前血量扣損
    currentHp -= damage;
    
    // 死亡處理
    if(currentHp <= 0){
      isDead = true;
      GameManager.instance.GameOver();
    } else { // 受攻擊時間處理
      attackedTimer = attackedTimerToDisable;
    }
    
    Debug.Log($"Character TakeDamage: {damage}, currentHp: {currentHp}, CharacterIsDead: {isDead}");
  }

}
