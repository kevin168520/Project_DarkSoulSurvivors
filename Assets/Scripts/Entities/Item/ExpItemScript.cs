using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpItemScript : ItemPickupableComponent
{
  [SerializeField] int exp = 1;
  override protected void OnPickup(Collider2D collision){
    EventGlobalManager.Instance.InvokeEvent(new ExpEvent(){ exp = exp});
    Destroy(gameObject);
  }
}
