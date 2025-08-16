using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatUI : MonoBehaviour
{
    [SerializeField] Slider _HpBar;
    public float HpBar {set{
      _HpBar.value = value;
    }}
    [SerializeField] Slider _ExpBar;
    public float ExpBar {set{
      _ExpBar.value = value;
    }}
    [SerializeField] TextMeshProUGUI _ExpLevel;
    public int ExpLevel {set{
      _ExpLevel.text = $"Level:{value}";
    }}
    [SerializeField] TextMeshProUGUI _GoldCount;
    public int GoldCount {set{
      _GoldCount.text = $"Gold:{value}";
    }}

}
