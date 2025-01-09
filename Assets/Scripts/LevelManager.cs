using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject level1MapManager; // 第一關 MapManager
    public GameObject level2MapManager; // 第二關 MapManager

    public void LoadLevel(int level)
    {
        // 關閉所有關卡
        level1MapManager.SetActive(false);
        level2MapManager.SetActive(false);

        // 啟動選擇的關卡
        if (level == 1)
        {
            level1MapManager.GetComponent<MapManager>().levelType = MapManager.LevelType.FourDirection;
            level1MapManager.SetActive(true);
        }
        else if (level == 2)
        {
            level2MapManager.GetComponent<MapManager>().levelType = MapManager.LevelType.Horizontal;
            level2MapManager.SetActive(true);
        }
        else
        {
            Debug.LogWarning("無效的關卡選擇：" + level);
        }
    }
}
