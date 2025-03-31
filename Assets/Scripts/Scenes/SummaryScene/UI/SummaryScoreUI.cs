using System.Collections;
using System.Collections.Generic;
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

    public void AddDetailScrollItem(Sprite iconWeapon, string text1, string text2){
      RectTransform itme = Instantiate(detailScrollItem, detailScrollContent);

      itme.GetComponentInChildren<Image>().sprite = iconWeapon;
      itme.GetComponentsInChildren<TMPro.TMP_Text>()[0].text = text1;
      itme.GetComponentsInChildren<TMPro.TMP_Text>()[1].text = text2;

      itme.gameObject.SetActive(true);
    }
}
