using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActorPanelUI : MonoBehaviour
{
    [field: SerializeField]
    public TextMeshProUGUI hpText { get; private set; }
    [field: SerializeField]
    public TextMeshProUGUI defText { get; private set; }
    [field: SerializeField]
    public TextMeshProUGUI atkText { get; private set; }

    [field: SerializeField]
    public GridLayoutGroup gridLayout { get; private set; }
    [field: SerializeField]
    public Image[] gridItems { get; private set; }

    public void SetAbility(Actor actor)
    {
        hpText.text = string.Format("HP: {0}", Mathf.CeilToInt(actor[HpAttribute.ID]));
        defText.text = string.Format("DEF: {0}", Mathf.CeilToInt(actor[DefAttribute.ID]));
        atkText.text = string.Format("ATK: {0}", Mathf.CeilToInt(actor[AttAttribute.ID]));
    }

    public void SetWeapon(params Sprite[] weapons)
    {
        for (int i = 0; i < gridItems.Length; i++)
        {
            var item = gridItems[i];
            if (i < weapons.Length)
            {
                item.sprite = weapons[i];
                item.gameObject.SetActive(true);
            }
            else
            {
                item.sprite = null;
                item.gameObject.SetActive(false);
            }
        }
    }
}
