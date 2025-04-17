using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceRegistry<T> where T : class
{
  public Dictionary<Type, T> pool = new Dictionary<Type, T>(); // 物件池
  private readonly object lockObj = new object(); // 用於執行緒安全


  /// <summary> 註冊實例 </summary>
  public bool Register(Type type, T obj)
  {
    lock (lockObj) {
      return pool.TryAdd(type, obj);
    }
  }
  public bool Register(T obj) => Register(obj.GetType(), obj);
  // public bool Register<S>() where S : class, T  => Register(typeof(S), default(S)); // 保留 實體必須要由外部提供

  /// <summary> 取得實例 </summary>
  public bool Get(Type type, out T obj)
  {
    lock (lockObj) {
      return pool.TryGetValue(type, out obj);
    }
  }
  public T Get(Type type) => Get(type, out T obj) ? obj : null;
  public bool Get<S>(out T obj) where S : class, T => Get(typeof(S), out obj);
  public S Get<S>() where S : class, T => Get(typeof(S), out T obj) ? obj as S: null;

  /// <summary> 移除實例 </summary>
  public bool Unregister(Type type)
  {
    lock (lockObj) {
      return pool.Remove(type);
    }
  }
  public bool Unregister(T obj) => Unregister(obj.GetType());
  public bool Unregister<S>() where S : class, T => Unregister(typeof(S));
}