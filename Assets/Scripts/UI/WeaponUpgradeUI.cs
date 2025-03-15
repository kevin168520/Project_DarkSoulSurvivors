using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class WeaponUpgradeUI : MonoBehaviour
{
    public List<UpgradeOptions> options;
    public bool Active {set{
      gameObject.SetActive(value);
    }}

    public void SetOptionsNumber(int n){
      for(int i = 0; i < options.Count; i++)
        options[i].Active = i < n;
    }
}

[System.Serializable]
public class UpgradeOptions
{
    [SerializeField] GameObject _OptionPanel;
    public bool Active {set{
      _OptionPanel.SetActive(value);
    }}
    [SerializeField] Button _OptionButton;
    public UnityAction<Object> Listener {set{
      _OptionButton.onClick.RemoveAllListeners();
      _OptionButton.onClick.AddListener(() => value(ReturnValue));
    }}
    [System.NonSerialized] public Object ReturnValue;
    [SerializeField] Image _IconImage;
    public Sprite IconImage {set{
      _IconImage.sprite = value;
    }}
    [SerializeField] TextMeshProUGUI _TitleText;
    public string TitleText {set{
      _TitleText.text = value;
    }}
    [SerializeField] TextMeshProUGUI _DescriptionText;
    public string DescriptionText {set{
      _DescriptionText.text = value;
    }}

}