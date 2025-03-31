using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ManagerMonoBase : InstanceMonoBase
{
  // 通用管理者成員
  GameManager _GameManager;
  protected GameManager GameManager => _GameManager ??= InstanceGlobalManager.Get<GameManager>();
  WeaponManager _WeaponManager;
  protected WeaponManager WeaponManager => _WeaponManager ??= InstanceGlobalManager.Get<WeaponManager>();
  PlayerManager _PlayerManager;
  protected PlayerManager PlayerManager => _PlayerManager ??= InstanceGlobalManager.Get<PlayerManager>();
  EnemyManager _EnemyManager;
  protected EnemyManager EnemyManager => _EnemyManager ??= InstanceGlobalManager.Get<EnemyManager>();
  StageManager _StageManager;
  protected StageManager StageManager => _StageManager ??= InstanceGlobalManager.Get<StageManager>();
  LevelTimerManager _LevelTimerManager;
  protected LevelTimerManager LevelTimerManager => _LevelTimerManager ??= InstanceGlobalManager.Get<LevelTimerManager>();
  
  // 全球管理者成員
  protected DataGlobalManager DataGlobalManager => DataGlobalManager.Instance;
  protected SceneGlobalManager SceneGlobalManager => SceneGlobalManager.Instance;
}