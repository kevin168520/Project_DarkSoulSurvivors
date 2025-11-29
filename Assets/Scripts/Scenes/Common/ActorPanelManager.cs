using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActorPanelManager : ManagerMonoBase
{
    [SerializeField] ActorPanelUI ActorPanelUI;

    public void UpdateUI()
    {
        var actor = PlayerManager.Player.character.actor;
        UpdateActor(actor);
        var weapons = WeaponManager.GetWeapons();
        UpdateWeapons(weapons);
    }

    void UpdateActor(Actor actor)
    {
        ActorPanelUI?.SetAbility(actor);
    }

    void UpdateWeapons(List<WeaponHandlerBase> weapons)
    {
        Sprite[] weaponIcon = weapons.Select(w => w.weaponIcon).ToArray();
        ActorPanelUI.SetWeapon(weaponIcon);
    }
}
