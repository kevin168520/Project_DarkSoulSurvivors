using UnityEngine;

public class GoldItemScript : ItemPickupableComponent
{
  [SerializeField] int gold = 1;
  override protected void OnPickup(Collider2D collision){
    EventGlobalManager.Instance.InvokeEvent(new GoldEvent(){ gold = gold});
    Destroy(gameObject);
  }
}