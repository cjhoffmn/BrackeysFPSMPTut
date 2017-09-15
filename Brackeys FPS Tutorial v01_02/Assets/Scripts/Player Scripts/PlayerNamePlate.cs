using UnityEngine;
using UnityEngine.UI;

public class PlayerNamePlate : MonoBehaviour
{

    [SerializeField]
    private Text usernameText;

    [SerializeField]
    private Player player;

    [SerializeField]
    RectTransform healthBarFill;

    void Update()
    {
        usernameText.text = player.username;
        healthBarFill.localScale = new Vector3(player.GetHealthBarAmt(), .88f, .88f);
    }

}
