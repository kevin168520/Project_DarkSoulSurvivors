using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterScript : MonoBehaviour, IDamageable, IEvent<GoldEvent>, IEvent<ExpEvent>
{
    // 角色資料監聽用型態
    public enum StatType
    {
        Level, TotalExp, ExpToLevelUp, MaxHp, Def, SpeedMult, AttackMult, CurrentHp, isDead, Invincibility, Gold
    }

    // 資料變更監聽者
    public UnityEvent<Actor, IActorAttribute> onActorEvent = new();
    public UnityEvent onInvincibleEvent = new();
    public UnityEvent onDeadEvent = new();

    // 角色資料
    [SerializeField] public Actor actor = new();
    [SerializeField] public int level => (int)actor.Get(LevelAttribute.ID).Value; // 等級
    [SerializeField] public float levelUpExp => actor.Get(ExpAttribute.ID).OrigValue; // 升級所需經驗值
    [SerializeField] public float totalExp => actor.Get(ExpAttribute.ID).Value; // 累積經驗值
    [SerializeField] public float maxHp => actor.Get(HpAttribute.ID).OrigValue; // 當前血量
    [SerializeField] public float def => actor.Get(DefAttribute.ID).Value; // 防禦
    [SerializeField] public float moveMult => actor.Get(MoveAttribute.ID).Value; // 移動速度倍率
    [SerializeField] public float speedMult => actor.Get(SpeedAttribute.ID).Value; // 攻擊速度倍率
    [SerializeField] public float attackMult => actor.Get(AttAttribute.ID).Value; // 攻擊倍率
    [SerializeField] public float currentHp => actor.Get(HpAttribute.ID).Value; // 當前血量
    [SerializeField] public int gold => (int)actor.Get(GoldAttribute.ID).Value;  // 當前金幣
    bool isDead = false; // 死亡判定
    [SerializeField] TimeCounter invincibilityCounter = new TimeCounter(1f); // 無敵時間

    void OnEnable()
    {
        EventGlobalManager.Instance.RegisterEvent(this);
    }

    void OnDisable()
    {
        EventGlobalManager.Instance?.DeregisterEvent(this);
    }

    void Update()
    {
        invincibilityCounter.UpdateDelta();
    }

    public Actor CreateActor()
    {
        actor = new();
        actor.Add(LevelAttribute.Create());
        actor.Add(ExpAttribute.Create());
        actor.Add(HpAttribute.Create());
        actor.Add(DefAttribute.Create());
        actor.Add(MoveAttribute.Create());
        actor.Add(SpeedAttribute.Create());
        actor.Add(AttAttribute.Create());
        actor.Add(GoldAttribute.Create());
        return actor;
    }


    public void AddHp(int hp)
    {
        actor[HpAttribute.ID] += hp;

        onActorEvent.Invoke(actor, actor.Get<HpAttribute>());
    }

    public void LevelUp()
    {
        actor[LevelAttribute.ID]++;
        float exp = actor[ExpAttribute.ID];
        actor.Set(ExpAttribute.Create(actor[LevelAttribute.ID] * 10));
        actor[ExpAttribute.ID] = exp;

        onActorEvent.Invoke(actor, actor.Get<LevelAttribute>());
    }

    /// <summary> 死亡檢查 </summary>
    public bool CheckDead()
    {
        if (actor[HpAttribute.ID] <= 0)
        {
            isDead = true;
            onDeadEvent.Invoke();
        }
        return isDead;
    }

    /// <summary> 無敵時間啟用 </summary>
    public void OnHitInvincibility()
    {
        invincibilityCounter.Reset();

        onInvincibleEvent.Invoke();
    }

    /// <summary> 傷害判斷執行 </summary>
    public void TakeDamage(int damage)
    {
        // 如果已死亡 則不動作
        if (isDead)
        {
            Debug.Log($"Character isDead: {isDead}");
            return;
        }

        // 如果還在無敵時間 則不動作
        if (invincibilityCounter.IsTiming)
        {
            // Debug.Log($"Character is attackedTimerToDisable: {attackedTimer}");
            return;
        }

        // 如果傷害低於防禦則不損傷
        if (actor[DefAttribute.ID] >= damage)
        {
            // Debug.Log($"Character no harm caused: {def} >= {damage}");
            return;
        }

        // 當前血量扣損
        AddHp(-damage);

        // 死亡處理
        if (!CheckDead())
        {
            // 受攻擊時間處理
            OnHitInvincibility();
        }

        Debug.Log($"Character TakeDamage: {damage} - {actor[DefAttribute.ID]}, currentHp: {actor[HpAttribute.ID]}, CharacterIsDead: {isDead}");
    }

    public void Execute(GoldEvent parameters)
    {
        actor[GoldAttribute.ID] += parameters.gold;

        onActorEvent.Invoke(actor, actor.Get<GoldAttribute>());
    }

    public void Execute(ExpEvent parameters)
    {
        var exp = actor.Get<ExpAttribute>();
        exp.Value += parameters.exp;

        // 檢查經驗值達到升級
        if (exp.Value >= exp.OrigValue)
        {
            exp.Value -= exp.OrigValue;
            LevelUp();
        }

        onActorEvent.Invoke(actor, actor.Get<ExpAttribute>());
    }
}
