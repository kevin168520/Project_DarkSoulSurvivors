using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class WeaponUpgradeUI : MonoBehaviour
{
    public List<UpgradeOptions> options; // 內部選項
    public bool Active {set{ // 設置啟用
      gameObject.SetActive(value);
    }}

    public void SetOptionsNumber(int n){  // 設置顯示數量
      for(int i = 0; i < options.Count; i++)
        options[i].Active = i < n;
    }
}

[System.Serializable]
public class UpgradeOptions
{
    [SerializeField] GameObject _OptionPanel; // 面板物件
    public bool Active {set{  // 設置啟用
      _OptionPanel.SetActive(value);
    }}
    [SerializeField] Button _OptionButton;  // 按鈕物件
    public UnityAction<Object> Listener {set{  // 選項監聽
      _OptionButton.onClick.RemoveAllListeners();
      _OptionButton.onClick.AddListener(() => value(ReturnValue));
    }}
    [System.NonSerialized] public Object ReturnValue; // 回傳資料
    [SerializeField] Image _IconImage; // 圖像
    public Sprite IconImage {set{
      _IconImage.sprite = value;
    }}
    [SerializeField] TextMeshProUGUI _TitleText; // 名稱
    public string TitleText {set{
      _TitleText.text = value;
    }}
    [SerializeField] TextMeshProUGUI _DescriptionText; // 說明文
    public string DescriptionText {set{
      _DescriptionText.text = value;
    }}

}