using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class LocalStorage<T> : IStorage<T> where T : new()
{
    private static string PathCombine(string file) =>
        Path.Combine(Application.persistentDataPath, file);

    private string path;

    public LocalStorage(string file)
    {
        path = PathCombine(file);
        var directory = Path.GetDirectoryName(path);
        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
    }

    public void Save(object data)
    {
        using var writer = new StreamWriter(path, false);
        string json = JsonUtility.ToJson(data);
        writer.Write(json);
    }

    public T Load()
    {
        if (!File.Exists(path)) return new();

        using var reader = new StreamReader(path);
        string json = reader.ReadToEnd();
        return JsonUtility.FromJson<T>(json);
    }

    public async UniTask SaveAsync(object data)
    {
        using var writer = new StreamWriter(path, false);
        string json = JsonUtility.ToJson(data);
        await writer.WriteAsync(json);
    }

    public async UniTask<T> LoadAsync()
    {
        if (!File.Exists(path)) return new();

        using var reader = new StreamReader(path);
        string json = await reader.ReadToEndAsync();
        return JsonUtility.FromJson<T>(json);
    }
}
