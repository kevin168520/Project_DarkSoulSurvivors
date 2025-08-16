using System.Collections.Generic;
using baseSys.Audio.Sources;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AudioDatabaseScriptable", order = 1)]
public class AudioDatabaseScriptable : ScriptableObject
{
    /// <summary> BGM 資源設定 </summary>
    [SerializeField]
    public List<AudioSourceScriptable> BGMSetting;

    /// <summary> SFX 資源設定 </summary>
    [SerializeField]
    public List<AudioSourceScriptable> SFXSetting;
}
