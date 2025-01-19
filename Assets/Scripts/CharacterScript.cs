using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour, IDamageable
{
  
  // 內部
  [SerializeField] int maxHp = 100;     // 最大血量
  int currentHp; // 當前血量
  bool isDead = false;  // 死亡判定

  void Awake()
  {
      currentHp = maxHp;
  }

  void Start()
  {
  }

  void Update()
  {
  }


  // 執行受傷
  public void TakeDamage(int damage)
  {
    // 如果已死亡 則不動作
    if(isDead) {
      Debug.Log($"Character TakeDamage: {damage}, currentHp: {currentHp}, CharacterIsDead: {isDead}");
      return; 
    }

    // 當前血量扣損
    currentHp -= damage;
    
    // 死亡處理
    if(currentHp <= 0) isDead = true;
    
    Debug.Log($"Character TakeDamage: {damage}, currentHp: {currentHp}, CharacterIsDead: {isDead}");
  }

}
