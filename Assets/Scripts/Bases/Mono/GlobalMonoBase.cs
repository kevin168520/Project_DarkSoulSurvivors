using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public abstract class GlobalMonoBase<T> : MonoBehaviour where T : MonoBehaviour
{
  private static T _instance;
  private static bool _isApplicationQuitting = false;

  public static T Instance {
    get {
      if (_isApplicationQuitting) return null;
      
      if (_instance == null) {
        GameObject obj = new GameObject(typeof(T).Name);
        _instance = obj.AddComponent<T>();
        DontDestroyOnLoad(obj);
      }
      return _instance;
    }
  }

  protected virtual void Awake() {
    if (_instance != null) {
      Destroy(this);
      return;
    }

    _instance = this as T;
    DontDestroyOnLoad(this);
  }

  private void OnApplicationQuit() {
    _isApplicationQuitting = true;
  }
}
