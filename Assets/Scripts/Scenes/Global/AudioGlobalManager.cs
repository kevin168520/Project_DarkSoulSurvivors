using UnityEngine;
using System;
using System.Linq;
using baseSys.Audio.Method;
using Cysharp.Threading.Tasks;

public class AudioGlobalManager : GlobalMonoBase<AudioGlobalManager>
{
    /// <summary> BGM 播放器 </summary>
    PlayerMethod BGM;
    /// <summary> BGM 音量較正值 </summary>
    [Range(0, 1)]
    float BGMValue = 0.5f;

    /// <summary> SFX 播放器 </summary>
    PlayerMethod SFX;
    /// <summary> SFX 音量較正值 </summary>
    [Range(0, 1)]
    float SFXValue = 0.5f;

    /// <summary> Addressable 載入器 </summary>
    AssetsManager AssetsManager = new();
    /// <summary> 音樂資料庫 </summary>
    AudioDatabaseScriptable audioDatabase;
    /// <summary> 判定初始化 </summary>
    bool initialized = false;

    protected override void Awake()
    {
        base.Awake();
        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }

        if (!initialized)
        {
            // 靜態生成需要主動添加音樂播放器
            gameObject.AddComponent<AudioListener>();
            // 異步載入 有尚未載入風險
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

    void OnDestroy()
    {
        if (audioDatabase != null) AssetsManager.Release(audioDatabase);
    }

    #region [BGM播放]
    public void PlayBGM(enAudioBgmData enAudioDataBGM) =>
        UniTask.Void(async () =>
        {
            await UniTask.WaitUntil(() => initialized);
            BGM.Play(enAudioDataBGM.ToString());
        });

    public void BGMStop() =>
        UniTask.Void(async () =>
        {
            await UniTask.WaitUntil(() => initialized);
            BGM.StopAll();
        });

    public void BGMStop(string name) =>
        UniTask.Void(async () =>
        {
            await UniTask.WaitUntil(() => initialized);
            BGM.Stop(name);
        });

    public void BGMReset(float value) =>
        UniTask.Void(async () =>
        {
            await UniTask.WaitUntil(() => initialized);
            BGM.ResetValue(value);
        });

    public void BGMMute(bool mute) =>
        UniTask.Void(async () =>
        {
            await UniTask.WaitUntil(() => initialized);
            BGM.Mute(mute);
        });
    #endregion

    #region [SFX播放]
    public void PlaySFX(enAudioSfxData enAudioSfxData) =>
        UniTask.Void(async () =>
        {
            await UniTask.WaitUntil(() => initialized);
            SFX.Play(enAudioSfxData.ToString());
        });

    public void SFXStop() =>
        UniTask.Void(async () =>
        {
            await UniTask.WaitUntil(() => initialized);
            SFX.StopAll();
        });

    public void SFXStop(string name) =>
        UniTask.Void(async () =>
        {
            await UniTask.WaitUntil(() => initialized);
            SFX.Stop(name);
        });

    public void SFXReset(float value) =>
        UniTask.Void(async () =>
        {
            await UniTask.WaitUntil(() => initialized);
            SFX.ResetValue(value);
        });

    public void SFXMute(bool mute) =>
        UniTask.Void(async () =>
        {
            await UniTask.WaitUntil(() => initialized);
            SFX.Mute(mute);
        });
    #endregion

    #region [ALL播放]
    /// <summary> 停止所有</summary>
    public void Stop() =>
        UniTask.Void(async () =>
        {
            await UniTask.WaitUntil(() => initialized);
            SFX.StopAll();
            BGM.StopAll();
        });

    /// <summary> 停止所有</summary>
    public void Stop(string name) =>
        UniTask.Void(async () =>
        {
            await UniTask.WaitUntil(() => initialized);
            SFX.Stop(name);
            BGM.Stop(name);
        });

    /// <summary> 設置音量 </summary>
    public void Reset(float value) =>
        UniTask.Void(async () =>
        {
            await UniTask.WaitUntil(() => initialized);
            BGM.ResetValue(value);
            SFX.ResetValue(value);
        });

    /// <summary> 設置靜音 </summary>
    public void Mute(bool mute) =>
        UniTask.Void(async () =>
        {
            await UniTask.WaitUntil(() => initialized);
            BGM.Mute(mute);
            SFX.Mute(mute);
        });
    #endregion
}