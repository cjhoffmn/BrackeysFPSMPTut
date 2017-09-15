using UnityEngine;
using UnityEngine.UI;

public class KillFeedItem : MonoBehaviour
{
    [SerializeField]
    Text text;

    public void SetupKFI (string playername, string sourcename)
    {
        GetComponentInParent<RectTransform>().SetAsFirstSibling();
        text.text = "<b>" + sourcename + "</b>" + " killed " + "<color=red>" + playername + "</color>";
    }

}
