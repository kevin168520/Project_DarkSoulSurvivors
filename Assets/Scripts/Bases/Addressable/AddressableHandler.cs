using System;
using System.Collections.Concurrent;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetsManager
{
    private static string KeyValue(Type type, string key) => $"{type.Name}_{key}";
    /// <summary> (asset, path) </summary>
    private readonly ConcurrentDictionary<string, object> assets = new();
    /// <summary> (path, asset) </summary>
    private readonly ConcurrentDictionary<object, string> assetPaths = new();
    /// <summary> (asset, handle) </summary>
    private readonly ConcurrentDictionary<object, AsyncOperationHandle> handles = new();

    public async UniTask<T> Get<T>(string addressablePath)
    {
        // 資源已經被載入過則回傳
        string keyValue = KeyValue(typeof(T), addressablePath);
        if (assets.TryGetValue(keyValue, out object existAsset))
        {
            Debug.Log($"Get asset {addressablePath} success.");
            return (T)existAsset;
        }
        return await Load<T>(keyValue, addressablePath);
    }

    private async UniTask<T> Load<T>(string keyValue, string addressablePath)
    {
        // 載入指定的資源
        var handle = Addressables.LoadAssetAsync<T>(addressablePath);
        await handle;
        if (handle.Status == AsyncOperationStatus.Failed)
        {
            Debug.LogError($"Load asset {addressablePath} failed.");
            return default;
        }
        Debug.Log($"Load asset {addressablePath} success.");
        // 保存載入資源，避免二次載入
        var asset = handle.Result;
        assets.TryAdd(keyValue, asset);
        assetPaths.TryAdd(asset, keyValue);
        handles.TryAdd(asset, handle);
        return asset;
    }

    /// <summary> 釋放資源 </summary>
    public void Release<T>(string addressablePath)
    {
        string keyValue = KeyValue(typeof(T), addressablePath);
        if (!assets.TryGetValue(keyValue, out object asset))
        {
            Debug.LogWarning($"Release asset {addressablePath} not found.");
            return;
        }
        Release(keyValue, asset);
    }

    /// <summary> 釋放資源本身 </summary>
    public void Release(object asset)
    {
        if (!assetPaths.TryGetValue(asset, out string addressablePath))
        {
            Debug.LogWarning($"Release asset {asset} not found.");
            return;
        }
        string keyValue = KeyValue(asset.GetType(), addressablePath);
        Release(keyValue, asset);
    }

    private void Release(string keyValue, object asset)
    {
        if (asset == null) return;
        if (!handles.TryRemove(asset, out var handle)) return;
        assets.TryRemove(keyValue, out _);
        Addressables.Release(handle);
        Debug.Log($"Release asset {asset} success.");
    }
}