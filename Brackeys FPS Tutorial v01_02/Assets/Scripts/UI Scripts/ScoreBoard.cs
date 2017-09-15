using System.Collections;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{

    [SerializeField]
    GameObject playerScoreboardItem;

    [SerializeField]
    Transform playerScoreboardList;

    public static bool SBisOn = false;

    private void OnEnable()
    {
        Player[] players = GameManager.GetAllPlayers();

        
        foreach  (Player player in players)
        {
            GameObject itemGO = (GameObject)Instantiate(playerScoreboardItem, playerScoreboardList);
            PlayerScoreBoardItem item = itemGO.GetComponent<PlayerScoreBoardItem>();
            if (item != null)
            {
                item.Setup(player.username, player.kills, player.deaths);
            }
        }
    }

    private void OnDisable()
    {
        foreach (Transform child in playerScoreboardList)
        {
            Destroy(child.gameObject);
        }
    }
}
