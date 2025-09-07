using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Steamworks;

public class SteamStorage<T> : IStorage<T> where T : new()
{
    private string fileName;

    public SteamStorage(string file)
    {
        fileName = file;
        if (!SteamManager.Initialized)
        {
            Debug.LogError("Steam 未初始化！");
        }
    }

    public void Save(object data)
    {
        string json = JsonUtility.ToJson(data);
        byte[] bytes = Encoding.UTF8.GetBytes(json);
        bool success = SteamRemoteStorage.FileWrite(fileName, bytes, bytes.Length);

        if (!success)
        {
            Debug.LogError($"Steam Cloud 儲存失敗: {fileName}");
        }
    }

    public T Load()
    {
        if (!SteamRemoteStorage.FileExists(fileName)) return new T();

        int size = SteamRemoteStorage.GetFileSize(fileName);
        byte[] bytes = new byte[size];
        SteamRemoteStorage.FileRead(fileName, bytes, size);

        string json = Encoding.UTF8.GetString(bytes);
        return JsonUtility.FromJson<T>(json);
    }

    public async UniTask SaveAsync(object data)
    {
        await UniTask.Create(() =>
        {
            Save(data);
            return UniTask.CompletedTask;
        });
    }

    public async UniTask<T> LoadAsync()
    {
        return await UniTask.Create(() =>
        {
            T result = Load();
            return UniTask.FromResult(result);
        });
    }
}