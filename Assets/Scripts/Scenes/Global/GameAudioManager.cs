using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using baseSys.Audio;
using baseSys.Audio.Sources;

public class GameAudioManager : MonoBehaviour
{
    public static GameAudioManager inst;
    /// <summary>
    /// ���ָ귽�]�w
    /// </summary>
    [SerializeField]
    Source[] BGMSetting;
    /// <summary>
    /// ���ĸ귽�]�w
    /// </summary>
    [SerializeField]
    Source[] SFXSetting;

    /// <summary>
    /// ���ּ���
    /// </summary>
    AudioPlayer BGM;
    /// <summary>
    /// ���ļ���
    /// </summary>
    AudioPlayer SFX;

    /// <summary>
    /// ���֭��q������
    /// </summary>
    [Range(0, 1)]
    float BGMValue = 0.5f;
    /// <summary>
    /// ���ĭ��q������
    /// </summary>
    [Range(0, 1)]
    float SFXValue = 0.5f;

    void Awake()
    {
        //inst = this;
        Debug.Log("AudioManager: " + BGMSetting.Length + " / " + SFXSetting.Length);

        BGM = new AudioPlayer(gameObject, "BGMPlayer", BGMSetting, BGMValue);
        SFX = new AudioPlayer(gameObject, "SFXPlayer", SFXSetting, SFXValue);
    }

    void Start()
    {
        //�M�ŬٰO����
        BGMSetting = null;
        SFXSetting = null;
    }

    #region [BGM����]
    /// <summary>
    /// ����]�wBGM
    /// </summary>
    /// <param name="name"></param>
    public void PlayBGM(string name)
    {
        BGM.Play(name);
    }

    /// <summary>
    /// StopAll
    /// </summary>
    public void BGMStop()
    {
        BGM.StopAll();
    }

    /// <summary>
    /// Stop Name
    /// </summary>
    /// <param name="name"></param>
    public void BGMStop(string name)
    {
        BGM.Stop(name);
    }

    /// <summary>
    /// ���]���q
    /// </summary>
    /// <param name="value"></param>
    public void BGMReset(float value)
    {
        BGM.ResetValue(value);
    }
    #endregion

    #region [SFX����]
    /// <summary>
    /// ���񭵮�
    /// </summary>
    /// <param name="name"></param>
    public void PlaySFX(string name)
    {
        SFX.Play(name);
    }

    /// <summary>
    /// Stop All
    /// </summary>
    public void SFXStop()
    {
        SFX.StopAll();
    }

    /// <summary>
    /// Stop Name
    /// </summary>
    /// <param name="name"></param>
    public void SFXStop(string name)
    {
        SFX.Stop(name);
    }

    /// <summary>
    /// ���]���q
    /// </summary>
    /// <param name="value"></param>
    public void SFXReset(float value)
    {
        SFX.ResetValue(value);
    }
    #endregion

    /// <summary>
    /// �R���ﶵ
    /// </summary>
    /// <param name="setAct"></param>
    public void Mute(bool setAct)
    {
        BGM.Mute(setAct);
        SFX.Mute(setAct);
    }
}
