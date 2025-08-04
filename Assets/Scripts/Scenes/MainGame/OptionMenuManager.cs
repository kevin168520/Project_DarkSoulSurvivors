using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenuManager : ManagerMonoBase
{    enum enSettingPanelState
    {
        Volume, WindowResolution, Language
    }

    public OptionMenuUI _optionMenuUI;


    private void OnEnable()
    {
        OnUIPanelRenew(enSettingPanelState.Volume);
    }

    // Start is called before the first frame update
    void Start()
    {
        BtnCtrlOptionMenu();
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
        _optionMenuUI.btnVolumeSettingOnclick = () => OnValumeSetting();
        _optionMenuUI.btnWindowResolutionSettingOnclick = () => OnWindowResolutionSetting();
        _optionMenuUI.btnLanguageSettingOnclick = () => OnLanguageSetting();
        _optionMenuUI.btnOptionCloseOnclick = () => OptionMenuSetClose();
    }

    /// <summary> 音效設定 </summary>
    void OnValumeSetting()
    {
        Debug.Log("OnValumeSetting");
        OnUIPanelRenew(enSettingPanelState.Volume);
    }

    /// <summary> 解析度設定 </summary>
    void OnWindowResolutionSetting()
    {
        Debug.Log("OnWindowResolutionSetting");
        OnUIPanelRenew(enSettingPanelState.WindowResolution);
    }

    /// <summary> 語言設定 </summary>
    void OnLanguageSetting()
    {
        Debug.Log("OnLanguageSetting");
        OnUIPanelRenew(enSettingPanelState.Language);
    }

    /// <summary> UI面板 顯示 </summary>
    public void OptionMenuSetClose()
    {
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
