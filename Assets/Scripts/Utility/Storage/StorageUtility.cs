public class StorageUtility
{
    public static IStorage<PlayerStoreData> PlayerStoreData()
    {
        return new LocalStorage<PlayerStoreData>("PlayerDataSaving.json");
    }
    public static IStorage<UserStoreData> UserStoreData()
    {
        return new LocalStorage<UserStoreData>("UserSettingSaving.json");
    }
}
