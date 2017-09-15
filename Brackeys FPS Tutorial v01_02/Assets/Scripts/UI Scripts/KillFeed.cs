using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFeed : MonoBehaviour
{
    [SerializeField]
    GameObject killFeedItemPrefab;
	// Use this for initialization
	void Start ()
    {
        GameManager.instance.onPlayerKilledCallBack += OnKill;
	}
	
    public void OnKill(string playername, string sourcename)
    {
        GameObject go = (GameObject)Instantiate(killFeedItemPrefab, this.transform);
        
        go.GetComponent<KillFeedItem>().SetupKFI(playername, sourcename);
        

        Destroy(go, 4f);
    }

}
