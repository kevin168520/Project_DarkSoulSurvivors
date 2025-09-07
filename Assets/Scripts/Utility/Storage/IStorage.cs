using Cysharp.Threading.Tasks;

public interface IStorage<T>
{
    void Save(object data);
    T Load();
    UniTask SaveAsync(object data);
    UniTask<T> LoadAsync();
}
