using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SummaryScoreUI : MonoBehaviour
{
    [SerializeField] Image CharacterImage;
    public Sprite characterImage { set => CharacterImage.sprite = value; }
    [SerializeField] Button BackToHomeButton;
    public UnityAction backToHomeButton {set{  // 選項監聽
      BackToHomeButton.onClick.RemoveAllListeners();
      BackToHomeButton.onClick.AddListener(value);
    }}
    [SerializeField] RectTransform DetailScrollContent;
    [SerializeField] RectTransform DetailScrollItem;

    public void AddDetailScrollItem(Sprite icon, string text1, string text2){
      RectTransform itme = Instantiate(DetailScrollItem, DetailScrollContent);

      itme.GetComponentInChildren<Image>().sprite = icon;
      itme.GetComponentsInChildren<TMPro.TMP_Text>()[0].text = text1;
      itme.GetComponentsInChildren<TMPro.TMP_Text>()[1].text = text2;

      itme.gameObject.SetActive(true);
    }
}
