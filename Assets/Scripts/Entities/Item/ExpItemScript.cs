using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpItemScript : ItemPickupableComponent
{
  [SerializeField] int exp = 1;
  override protected void OnPickup(Collider2D collision){
    GameManager.instance.playerCharacter.AddExp(exp);
    Destroy(gameObject);
  }
}
