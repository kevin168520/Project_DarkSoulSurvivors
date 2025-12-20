using System.Collections.Generic;
using UnityEngine;

public class DamageEffectsManager : ManagerMonoBase, IEvent<DamageEvent>
{
    const int poolSize = 20;

    [SerializeField] DamageTextScript prefab;

    Transform parent;

    Camera mainCamera;

    Queue<DamageTextScript> pool = new();

    void Start()
    {
        mainCamera = Camera.main;
        parent = prefab.transform.parent;
        for (int i = 0; i < poolSize; i++)
        {
            var instance = Instantiate(prefab, parent);
            pool.Enqueue(instance);
        }
    }

    void OnEnable()
    {
        EventGlobalManager.Instance.RegisterEvent(this);
    }

    void OnDisable()
    {
        EventGlobalManager.Instance?.DeregisterEvent(this);
    }

    public void Execute(DamageEvent parameters)
    {
        var instance = GetFromPool(); ;
        string damage = parameters.damage.ToString();
        Vector3 pos = mainCamera.WorldToScreenPoint(parameters.worldPos);
        instance.Play(damage, pos, () => ReturnToPool(instance));
    }

    private DamageTextScript GetFromPool() =>
        pool.Count switch
        {
            > 0 => pool.Dequeue(),
            _ => Instantiate(prefab, parent)
        };

    public void ReturnToPool(DamageTextScript item)
    {
        pool.Enqueue(item);
        item.GetOut();
    }
}
