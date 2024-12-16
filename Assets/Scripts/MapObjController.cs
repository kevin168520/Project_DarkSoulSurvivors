using UnityEngine;

public class MapObjController : MonoBehaviour
{
    public void RegisterTile(GameObject tile)
    {
        Debug.Log($"註冊地塊：{tile.name}");
        // 註冊邏輯，例如檢查互動式物件
    }

    public void UnregisterTile(GameObject tile)
    {
        Debug.Log($"解除註冊地塊：{tile.name}");
        // 解除註冊邏輯
    }
}
