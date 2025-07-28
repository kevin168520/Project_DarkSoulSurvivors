using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OptionMenuUI : MonoBehaviour
{
    [Header("OptionMenu")]
    [SerializeField] GameObject objOptionMenu;
    public bool objOptionMenuShow { set => objOptionMenu.SetActive(value); }
    [SerializeField] Button btnVolumeSetting;
    public UnityAction btnVolumeSettingOnclick { set => btnVolumeSetting.onClick.AddListener(value); }
    [SerializeField] Button btnWindowResolutionSetting;
    public UnityAction btnWindowResolutionSettingOnclick { set => btnWindowResolutionSetting.onClick.AddListener(value); }
    [SerializeField] Button btnLanguageSetting;
    public UnityAction btnLanguageSettingOnclick { set => btnLanguageSetting.onClick.AddListener(value); }
    [SerializeField] Button btnOptionClose;
    public UnityAction btnOptionCloseOnclick { set => btnOptionClose.onClick.AddListener(value); }
}
