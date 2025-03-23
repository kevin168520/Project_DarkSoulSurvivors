using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SummaryScoreUI : MonoBehaviour
{
    [SerializeField] Image CharacterImage;
    public Sprite characterImage { set => CharacterImage.sprite = value; }
    [SerializeField] Button BackToMenuButton;
    public UnityAction backToMenuButton {set{  // 選項監聽
      BackToMenuButton.onClick.RemoveAllListeners();
      BackToMenuButton.onClick.AddListener(value);
    }}
    [SerializeField] RectTransform DetailScrollContent;
    [SerializeField] RectTransform DetailScrollItem;

    public void AddDetailScrollItem(Sprite iconWeapon, string text1, string text2){
      RectTransform itme = Instantiate(DetailScrollItem, DetailScrollContent);

      itme.GetComponentInChildren<Image>().sprite = iconWeapon;
      itme.GetComponentsInChildren<TMPro.TMP_Text>()[0].text = text1;
      itme.GetComponentsInChildren<TMPro.TMP_Text>()[1].text = text2;

      itme.gameObject.SetActive(true);
    }
}
