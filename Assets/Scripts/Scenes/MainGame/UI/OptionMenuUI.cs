using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OptionMenuUI : MonoBehaviour
{
    [Header("OptionMenu")]
    [SerializeField] GameObject objOptionMenu;
    [SerializeField] GameObject objVolumeSetting;
    [SerializeField] GameObject objWindowResolutionSetting;
    [SerializeField] GameObject objLanguageSetting;

    public Slider sliVolumeALL;
    public Slider sliVolumeBGM;
    public Slider sliVolumeSFX;

    public bool objOptionMenuShow {set => objOptionMenu.SetActive(value);}
    public bool objVolumeSettingShow {set => objVolumeSetting.SetActive(value);}
    public bool objWindowResolutionSettingShow {set => objWindowResolutionSetting.SetActive(value);}
    public bool objLanguageSettingShow {set => objLanguageSetting.SetActive(value);}

    //public float fVolumeALL {
    //    get => sliVolumeALL.value;
    //    set => sliVolumeALL.value = value;
    //}
    //public float fVolumeBGM {
    //    get => sliVolumeBGM.value;
    //    set => sliVolumeBGM.value = value;
    //}
    //public float fVolumeSFX {
    //    get => sliVolumeSFX.value;
    //    set => sliVolumeSFX.value = value;
    //}

    [SerializeField] Button btnVolumeSetting;
    public UnityAction btnVolumeSettingOnclick {set => btnVolumeSetting.onClick.AddListener(value);}
    [SerializeField] Button btnWindowResolutionSetting;
    public UnityAction btnWindowResolutionSettingOnclick {set => btnWindowResolutionSetting.onClick.AddListener(value);}
    [SerializeField] Button btnLanguageSetting;
    public UnityAction btnLanguageSettingOnclick {set => btnLanguageSetting.onClick.AddListener(value);}
    [SerializeField] Button btnOptionClose;
    public UnityAction btnOptionCloseOnclick {set => btnOptionClose.onClick.AddListener(value);}
}
