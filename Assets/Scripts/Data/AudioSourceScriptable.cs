using baseSys.Audio.Sources;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AudioSource", order = 1)]
public class AudioSourceScriptable : ScriptableObject
{
    [SerializeField]
    public Source source;
}
