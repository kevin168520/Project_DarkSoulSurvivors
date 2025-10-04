using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionMenuManager : ManagerMonoBase
{
    enum enSettingPanelState { Volume, WindowResolution, Language }
    public OptionMenuUI _optionMenuUI;
    private UserStoreData data;

    // OptioMenu用的參數
    private int _volumeALL;
    private int _volumeBGM;
    private int _volumeSFX;
    private bool _isFullScreen;
    private int _iWindowResolution;
    private int _iLanguage;

    // 選單UI用的參數
    private FullScreenMode mode;
    private int iResolutionDropdownValue;
    private int iLanguageDropdownValue;

    void Start()
    {
        // 將數值從UserStoreData讀出
        data = DataGlobalManager._userData;
        if (data == null)
        {
            data = StorageUtility.UserStoreData().Load();
            DataGlobalManager._userData = data;
        }
        OptionMenuInformationRenew();
        OnUIPanelRenew(enSettingPanelState.Volume); // 設定預設畫面在Volume

        CtrlOptionMenu();
        OnSetupSlider();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OptionMenuSetClose();
        }
    }

    /// <summary> UI控制選單 </summary>
    private void CtrlOptionMenu()
    {
        _optionMenuUI.btnVolumeSettingOnclick = () => OnUIPanelRenew(enSettingPanelState.Volume);
        _optionMenuUI.btnWindowResolutionSettingOnclick = () => OnUIPanelRenew(enSettingPanelState.WindowResolution);
        _optionMenuUI.btnLanguageSettingOnclick = () => OnUIPanelRenew(enSettingPanelState.Language);
        _optionMenuUI.btnOptionCloseOnclick = () => OptionMenuSetClose();

        _optionMenuUI.btnFullSreenOnclick = () => OnWindowFullScreenSetting(_isFullScreen, true);
        _optionMenuUI.iDropdownValueChange = (int index) => OnResolutionSetting(_isFullScreen, index);
        _optionMenuUI.iLanguageChange = (int index) => OnLanguageSetting(index);
    }

    /// <summary> Option介面資訊更新 - 載入UserStoreData 與 起始畫面初始化 </summary>
    private void OptionMenuInformationRenew()
    {
        _volumeALL = data.iVolumeALL;
        _volumeBGM = data.iVolumeBGM;
        _volumeSFX = data.iVolumeSFX;
        _isFullScreen = data.bFullScreen;
        _iWindowResolution = data.iWondowsResolution;
        _iLanguage = data.iLanguage;

        // 畫面參數套值
        iResolutionDropdownValue = _iWindowResolution;
        iLanguageDropdownValue = _iLanguage;

        _optionMenuUI.sliVolumeALL.value = _volumeALL;
        _optionMenuUI.sliVolumeBGM.value = _volumeBGM;
        _optionMenuUI.sliVolumeSFX.value = _volumeSFX;

        OnWindowFullScreenSetting(_isFullScreen, false); // FullScreeng按鈕初始化
        _optionMenuUI.iWindowResolutionRenew = iResolutionDropdownValue;
        _optionMenuUI.iLanguageRenew = iLanguageDropdownValue;
    }

    /// <summary> 建立Slider的監聽事件 </summary>
    private void OnSetupSlider()
    {
        AddValumeSliderListener(_optionMenuUI.sliVolumeALL, "ALL");
        AddValumeSliderListener(_optionMenuUI.sliVolumeBGM, "BGM");
        AddValumeSliderListener(_optionMenuUI.sliVolumeSFX, "SFX");
    }

    #region AudioVloume設定
    private void AddValumeSliderListener(Slider slider, string key)
    {
        // 取得或添加 EventTrigger 元件
        EventTrigger trigger = slider.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = slider.gameObject.AddComponent<EventTrigger>();

        // 建立滑竿放開事件
        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerUp
        };

        // 當放開滑桿時觸發
        entry.callback.AddListener((eventData) =>
        {
            int value = Mathf.RoundToInt(slider.value);
            VolumeSetting(key, value);
        });

        trigger.triggers.Add(entry);    // 加入事件到 EventTrigger
    }

    private void VolumeSetting(string key, int value)
    {
        switch (key)
        {
            case "ALL":
                _volumeALL = value;
                break;
            case "BGM":
                _volumeBGM = value;
                break;
            case "SFX":
                _volumeSFX = value;
                break;
        }

        float fBGM = (_volumeALL * _volumeBGM) / 10000f;
        float fSFX = (_volumeALL * _volumeSFX) / 10000f;

        AudioGlobalManager.BGMReset(fBGM);
        AudioGlobalManager.SFXReset(fSFX);
    }
    #endregion

    #region WindowResolution設定
    /// <summary> FullScreen是否被開啟 </summary>
    private void OnWindowFullScreenSetting(bool FullScreen, bool Onclick)
    {
        if (Onclick)
            _isFullScreen = !FullScreen; // 有被點擊才改變狀態

        _optionMenuUI.objFullScreen = _isFullScreen; // 開啟/關閉勾選
        OnResolutionSetting(_isFullScreen, _iWindowResolution); // 觸發解析度調整以便確認解析度是否合適
    }

    /// <summary> 解析度調整 </summary>
    private void OnResolutionSetting(bool FullScreen, int IndexWindowResolution)
    {
        mode = (FullScreen) ? FullScreenMode.MaximizedWindow : FullScreenMode.Windowed;
        switch (IndexWindowResolution)
        {
            case 0: Screen.SetResolution(1920, 1080, mode); break;
            case 1: Screen.SetResolution(2560, 1440, mode); break;
            default: Screen.SetResolution(1920, 1080, mode); break;
        }
        iResolutionDropdownValue = IndexWindowResolution;
    }
    #endregion

    #region Language設定
    private void OnLanguageSetting(int IndexLanguage)
    {
        switch (IndexLanguage)
        {
            case 0:
                //Language English
                break;
            default:
                //Language Else
                break;
        }
        iLanguageDropdownValue = IndexLanguage;
    }
    #endregion

    /// <summary> Option關閉 </summary>
    private void OptionMenuSetClose()
    {
        _iWindowResolution = iResolutionDropdownValue;
        _iLanguage = iLanguageDropdownValue;

        data.iVolumeALL = _volumeALL;
        data.iVolumeBGM = _volumeBGM;
        data.iVolumeSFX = _volumeSFX;
        data.bFullScreen = _isFullScreen;
        data.iWondowsResolution = _iWindowResolution;
        data.iLanguage = _iLanguage;

        data = StorageUtility.UserStoreData().Load();

        // 顯示對應的 UI 面板
        _optionMenuUI.objOptionMenuShow = false;
    }

    /// <summary> Option面板 顯示 </summary>
    void OnUIPanelRenew(enSettingPanelState state)
    {
        // 顯示對應的 UI 面板
        _optionMenuUI.objVolumeSettingShow = state == enSettingPanelState.Volume;
        _optionMenuUI.objWindowResolutionSettingShow = state == enSettingPanelState.WindowResolution;
        _optionMenuUI.objLanguageSettingShow = state == enSettingPanelState.Language;
    }

}
