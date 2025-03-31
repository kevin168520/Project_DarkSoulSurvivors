using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InstanceMonoBase : MonoBehaviour
{
  protected InstanceGlobalManager InstanceGlobalManager => InstanceGlobalManager.Instance;

  protected virtual void Awake()
  {
    InstanceGlobalManager?.Register(this);
  }

  protected virtual void OnDestroy()
  {
    InstanceGlobalManager?.Unregister(this);
  }
}
