using UnityEngine;
using System;
using System.Linq;
using baseSys.Audio.Sources;
using baseSys.Audio.Method;
using Cysharp.Threading.Tasks;

public class AudioGlobalManager : GlobalMonoBase<AudioGlobalManager>
{
    /// <summary> BGM 資源設定 </summary>
    [SerializeField]
    Source[] BGMSetting;
    /// <summary> BGM 播放器 </summary>
    PlayerMethod BGM;
    /// <summary> BGM 音量較正值 </summary>
    [Range(0, 1)]
    float BGMValue = 0.5f;

    /// <summary> SFX 資源設定 </summary>
    [SerializeField]
    Source[] SFXSetting;
    /// <summary> SFX 播放器 </summary>
    PlayerMethod SFX;
    /// <summary> SFX 音量較正值 </summary>
    [Range(0, 1)]
    float SFXValue = 0.5f;

    protected override void Awake() 
    private AssetsManager AssetsManager = new();

    private AudioDatabaseScriptable audioDatabase;

    private bool initialized = false;

    {
        base.Awake();
        if(this != Instance)
        {
            Destroy(gameObject);
            return;
        }
        if (!initialized)
        {
            gameObject.AddComponent<AudioListener>();
            // 有載入尚未完成風險
            UniTask.Void(async () =>
            {
                audioDatabase = await AssetsManager.Get<AudioDatabaseScriptable>("AudioDatabase");

                var BGMSetting = audioDatabase?.BGMSetting.Select(t => t.source).ToArray();
                var SFXSetting = audioDatabase?.SFXSetting.Select(t => t.source).ToArray();
                BGM = new PlayerMethod(gameObject, BGMSetting, BGMValue);
                SFX = new PlayerMethod(gameObject, SFXSetting, SFXValue);
                initialized = true;
            });
        }
    }

    void Start () {
        //清空省記憶體
        BGMSetting = null;
        SFXSetting = null;
    }

    void OnDestroy()
    {
        // 釋放資源
        if (audioDatabase != null)
        {
            AssetsManager.Release(audioDatabase);
        }
    }

    #region [BGM播放]
    /// <summary> 播放指定 BGM </summary>
    public void PlayBGM(enAudioBgmData enAudioDataBGM) => BGM.Play(enAudioDataBGM.ToString());

    /// <summary> 停止所有 BGM </summary>
    public void BGMStop() => BGM.StopAll();

    /// <summary> 停止指定 BGM </summary>
    public void BGMStop(string name) => BGM.Stop(name);

    /// <summary> 設置音量 BGM </summary>
    public void BGMReset(float value) => BGM.ResetValue(value);

    /// <summary> 設置靜音 BGM </summary>
    public void BGMMute(bool mute) => BGM.Mute(mute);
    #endregion

    #region [SFX播放]
    /// <summary> 播放指定 SFX </summary>
    public void PlaySFX(enAudioSfxData enAudioSfxData) => SFX.Play(enAudioSfxData.ToString());

    /// <summary> 停止所有 SFX </summary>
    public void SFXStop() => SFX.StopAll();

    /// <summary> 停止指定 SFX </summary>
    public void SFXStop(string name) => SFX.Stop(name);

    /// <summary> 設置音量 SFX </summary>
    public void SFXReset(float value) => SFX.ResetValue(value);

    /// <summary> 設置靜音 SFX </summary>
    public void SFXMute(bool mute) => SFX.Mute(mute);
    #endregion

    #region [ALL播放]
    /// <summary> 停止所有</summary>
    public void Stop() {
        SFX.StopAll();
        BGM.StopAll();
    }

    /// <summary> 停止所有</summary>
    public void Stop(string name) {
        SFX.Stop(name);
        BGM.Stop(name);
    }

    /// <summary> 設置音量 </summary>
    public void Reset(float value) {
        BGM.ResetValue(value);
        SFX.ResetValue(value);
    }

    /// <summary> 設置靜音 </summary>
    public void Mute(bool mute) {        
        BGM.Mute(mute);
        SFX.Mute(mute);
    }
    #endregion
}
