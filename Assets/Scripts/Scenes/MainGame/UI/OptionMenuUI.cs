using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class OptionMenuUI : MonoBehaviour
{
    [SerializeField] GameObject objOptionMenu;
    [SerializeField] GameObject objVolumeSetting;
    [SerializeField] GameObject objWindowResolutionSetting;
    [SerializeField] GameObject objLanguageSetting;
    [SerializeField] GameObject objFullScreenImage;

    [SerializeField] TMP_Dropdown dropWindowResolutionSettings;
    [SerializeField] TMP_Dropdown dropLanguageSettings;

    public Slider sliVolumeALL;
    public Slider sliVolumeBGM;
    public Slider sliVolumeSFX;

    public bool objOptionMenuShow {set => objOptionMenu.SetActive(value);}
    public bool objVolumeSettingShow {set => objVolumeSetting.SetActive(value);}
    public bool objWindowResolutionSettingShow {set => objWindowResolutionSetting.SetActive(value);}
    public bool objLanguageSettingShow {set => objLanguageSetting.SetActive(value);}

    [SerializeField] Button btnVolumeSetting;
    public UnityAction btnVolumeSettingOnclick {set => btnVolumeSetting.onClick.AddListener(value);}

    [SerializeField] Button btnFullSreen;
    public UnityAction btnFullSreenOnclick { set => btnFullSreen.onClick.AddListener(value); }
    public bool objFullScreen { set => objFullScreenImage.SetActive(value); }

    [SerializeField] Button btnWindowResolutionSetting;
    public UnityAction btnWindowResolutionSettingOnclick {set => btnWindowResolutionSetting.onClick.AddListener(value);}
    public int iWindowResolutionRenew {
        set {
            int clampedIndex = Mathf.Clamp(value, 0, dropWindowResolutionSettings.options.Count - 1);
            dropWindowResolutionSettings.value = clampedIndex;
            dropWindowResolutionSettings.RefreshShownValue();
        }
    }
    public UnityAction<int> iDropdownValueChange {set => dropWindowResolutionSettings.onValueChanged.AddListener(value);}

    [SerializeField] Button btnLanguageSetting;
    public UnityAction btnLanguageSettingOnclick {set => btnLanguageSetting.onClick.AddListener(value);}
    public int iLanguageRenew {
        set {
            int clampedIndex = Mathf.Clamp(value, 0, dropLanguageSettings.options.Count - 1);
            dropLanguageSettings.value = clampedIndex;
            dropLanguageSettings.RefreshShownValue();
        }
    }
    public UnityAction<int> iLanguageChange {set => dropLanguageSettings.onValueChanged.AddListener(value);}

    [SerializeField] Button btnOptionClose;
    public UnityAction btnOptionCloseOnclick {set => btnOptionClose.onClick.AddListener(value);}
}
