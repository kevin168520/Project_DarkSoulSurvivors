using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary> 負責關卡時間與畫面時間 </summary>
public class LevelTimerManager : ManagerMonoBase
{
    [Header("關卡時間UI")]
    [SerializeField]
    TextMeshProUGUI _LevelTimerText;

    [Header("關卡時間")]
    [SerializeField]
    float stageTime;

    /// <summary> 關卡時間 秒數 </summary>
    public int Sec { get; private set; }

    /// <summary> 關卡時間監聽 每秒觸發一次 </summary>
    UnityEvent<int> _listen = new UnityEvent<int>();
    public void AddListener(UnityAction<int> callback) => _listen.AddListener(callback);

    void Update()
    {
        stageTime += Time.deltaTime;
        // 每秒觸發一次
        if (Sec <= stageTime)
        {
            Sec = Mathf.CeilToInt(stageTime);
            _LevelTimerText.text = FormatLevelTimerText(Sec / 60, Sec % 60);
            _listen.Invoke(Sec);
        }
    }

    string FormatLevelTimerText(int m, int s) => $"{m:00}：{s:00}";
}
