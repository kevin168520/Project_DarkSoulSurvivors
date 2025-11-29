using UnityEngine;
using UnityEngine.Events;

/// <summary> 負責角色狀態與畫面等級 </summary>
public class PlayerManager : ManagerMonoBase
{
    UnityAction WeaponUpgrade => WeaponManager.InitWeaponUpgradeUI;
    [Header("玩家物件")]
    [SerializeField] private PlayerScript _player;
    public PlayerScript Player => _player;
    public static PlayerScript PLAYER; // 暫時用來對接 非 Manager 相關腳本
    [Header("玩家狀態 UI")]
    [SerializeField] private PlayerStatUI _playerStatUI;

    void Start()
    {
        // 載入角色資料
        CharacterScriptable characterData = DataGlobalManager._characterData;
        PlayerStoreData storeData = DataGlobalManager._playerData;
        Player.characterData = characterData;
        Player.storeData = storeData;
        PlayerManager.PLAYER = _player;

        // 載入角色圖片
        GameObject spritePrefab = Instantiate(characterData.spritePrefab);
        spritePrefab.transform.position = Player.trans.position;
        spritePrefab.transform.parent = Player.trans;
        spritePrefab.SetActive(true);
        Player.anim = spritePrefab.GetComponent<PlayerAnimationComponent>(); // 暫時使用組合方式載入

        // 載入角色單位
        var actor = Player.character.CreateActor();
        actor.Set(HpAttribute.Create(characterData.hp + (storeData.iPlayerItemLevel_HP * 10)));
        actor.Set(DefAttribute.Create(characterData.def + (storeData.iPlayerItemLevel_DEF * 1)));
        actor.Set(MoveAttribute.Create(characterData.speedMult + (storeData.iPlayerItemLevel_moveSpeed * 1)));
        actor.Set(AttAttribute.Create(characterData.attackMult + (storeData.iPlayerItemLevel_ATK * 1)));

        // 註冊角色監聽
        Player.character.onActorEvent.AddListener(OnActorEvent);
        Player.character.onInvincibleEvent.AddListener(Player.anim.PlayHitFlicker);
        Player.character.onDeadEvent.AddListener(GameManager.GameOver);
    }

    public void OnActorEvent(Actor actor, IActorAttribute attr)
    {
        switch (attr)
        {
            case LevelAttribute lv:
                _playerStatUI.ExpLevel = (int)lv.Value;
                WeaponUpgrade();
                break;
            case ExpAttribute exp:
                _playerStatUI.ExpBar = exp.Value / exp.OrigValue;
                break;
            case HpAttribute hp:
                _playerStatUI.HpBar = hp.Value / hp.OrigValue;
                break;
            case GoldAttribute gold:
                _playerStatUI.GoldCount = (int)gold.Value;
                break;
            default:
                break;
        }
    }
}
