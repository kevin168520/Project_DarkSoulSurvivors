using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SummaryScoreUI : MonoBehaviour
{
    [SerializeField] Image characterImage;
    public Sprite CharacterImage { set => characterImage.sprite = value; }
    [SerializeField] Button backToMenuButton;
    public UnityAction BackToMenuButtonOnClick {set => backToMenuButton.onClick.AddListener(value);}
    [SerializeField] RectTransform detailScrollContent;
    [SerializeField] RectTransform detailScrollItem;
    [SerializeField] TextMeshProUGUI goldText;
    public int GoldCount {set => goldText.text = $"Gold：{value}";}

    public void AddDetailScrollItem(Sprite iconWeapon, string text1, string text2){
      RectTransform item = Instantiate(detailScrollItem, detailScrollContent);

      item.GetComponentInChildren<Image>().sprite = iconWeapon;
      item.GetComponentsInChildren<TMPro.TMP_Text>()[0].text = text1;
      item.GetComponentsInChildren<TMPro.TMP_Text>()[1].text = text2;

      item.gameObject.SetActive(true);
    }
}
