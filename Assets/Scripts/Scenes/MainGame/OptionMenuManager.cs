using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenuManager : ManagerMonoBase
{
    public OptionMenuUI _optionMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        BtnCtrlOptionMenu();
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
    }

    /// <summary> 解析度設定 </summary>
    void OnWindowResolutionSetting()
    {
        Debug.Log("OnWindowResolutionSetting");
    }

    /// <summary> 語言設定 </summary>
    void OnLanguageSetting()
    {
        Debug.Log("OnLanguageSetting");
    }

    /// <summary> UI面板 顯示 </summary>
    public void OptionMenuSetClose()
    {
        // 顯示對應的 UI 面板
        _optionMenuUI.objOptionMenuShow = false;
    }
}
