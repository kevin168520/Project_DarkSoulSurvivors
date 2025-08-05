using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionMenuManager : ManagerMonoBase
{
    enum enSettingPanelState {Volume, WindowResolution, Language}
    public OptionMenuUI _optionMenuUI;
    private UserStoreData data;

    private float _volumeALL;
    private float _volumeBGM;
    private float _volumeSFX;

    private void OnEnable()
    {
        _volumeBGM = DataGlobalManager._userData.fVolumeBGM;
        _volumeSFX = DataGlobalManager._userData.fVolumeSFX;

        OnUIPanelRenew(enSettingPanelState.Volume); // 設定預設畫面在Volume
    }

    // Start is called before the first frame update
    void Start()
    {
        data = DataGlobalManager._userData;

        _volumeALL = data.fVolumeALL;
        _volumeBGM = data.fVolumeBGM;
        _volumeSFX = data.fVolumeSFX;

        BtnCtrlOptionMenu();
        SliderValueChange();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OptionMenuSetClose();
        }
    }

    /// <summary> 控制選單 </summary>
    private void BtnCtrlOptionMenu()
    {
        _optionMenuUI.btnVolumeSettingOnclick = () => OnUIPanelRenew(enSettingPanelState.Volume);
        _optionMenuUI.btnWindowResolutionSettingOnclick = () => OnUIPanelRenew(enSettingPanelState.WindowResolution);
        _optionMenuUI.btnLanguageSettingOnclick = () => OnUIPanelRenew(enSettingPanelState.Language);
        _optionMenuUI.btnOptionCloseOnclick = () => OptionMenuSetClose();
    }

    /// <summary> 將VolumeMenu中各個Slider的變化值套入對應的參數 </summary>
    private void SliderValueChange()
    {
        AddReleaseListener(_optionMenuUI.sliVolumeALL, "ALL");
        AddReleaseListener(_optionMenuUI.sliVolumeBGM, "BGM");
        AddReleaseListener(_optionMenuUI.sliVolumeSFX, "SFX");
    }

    /// <summary> 建立Slider的監聽事件 </summary>
    private void AddReleaseListener(Slider slider, string key)
    {
        // 取得或添加 EventTrigger 元件
        EventTrigger trigger = slider.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = slider.gameObject.AddComponent<EventTrigger>();        

        // 建立 PointerUp 事件
        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerUp
        };

        // 當放開滑桿時觸發
        entry.callback.AddListener((eventData) =>
        {
            float value = slider.value;
            Debug.Log(key + " : " + value);
            VolumeSetting(key, value);
        });

        trigger.triggers.Add(entry);    // 加入事件到 EventTrigger
    }

    private void VolumeSetting(string key, float value)
    {
        switch (key) {
            case "ALL":
                _volumeALL = value / 100;
                break;
            case "BGM":
                _volumeBGM = value / 100;
                break;
            case "SFX":
                _volumeSFX = value / 100;
                break;
        }

        float fBGM = _volumeALL * _volumeBGM;
        float fSFX = _volumeALL * _volumeSFX;

        AudioGlobalManager.BGMReset(fBGM);
        AudioGlobalManager.SFXReset(fSFX);
    }


    /// <summary> UI面板 顯示 </summary>
    private void OptionMenuSetClose()
    {
        data.fVolumeALL = _volumeALL;
        data.fVolumeBGM = _volumeBGM;
        data.fVolumeSFX = _volumeSFX;

        StoreDataRepository.UserDataSaving(ref data);

        // 顯示對應的 UI 面板
        _optionMenuUI.objOptionMenuShow = false;
    }

    /// <summary> UI面板 顯示 </summary>
    void OnUIPanelRenew(enSettingPanelState state)
    {
        // 顯示對應的 UI 面板
        _optionMenuUI.objVolumeSettingShow = state == enSettingPanelState.Volume;
        _optionMenuUI.objWindowResolutionSettingShow = state == enSettingPanelState.WindowResolution;
        _optionMenuUI.objLanguageSettingShow = state == enSettingPanelState.Language;
    }

}
