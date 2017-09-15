using UnityEngine;
using UnityEngine.UI;

public class PerformanceTracking : MonoBehaviour {


 //   [SerializeField]
 //   private int BlltsLeftinMag, BlltsPerMag, BlltsLeftforGun, ShotsInSession, ShotsFired, Hitcount;  //Bullets remaining in a magazine between reloads
 //   public int TriggerPulls;
 //   public Text BlltsLeftinMagText, HitCountText, ReloadText, HitPercText, TriggerPullsText;
 //   public float HitPerc;
 //   bool TrigPulled;

 //   // Use this for initialization

 //   void Start ()
 //   {
 //       ShotsFired = 0;
 //       HitPerc = 0;
 //       HitPercText.text = "Notstarted";
 //       BlltsLeftforGun = 200;
 //       BlltsPerMag = 30;
 //       BlltsLeftinMag = BlltsPerMag;
 //       Hitcount = 0;
 //       ReloadText.text = "";
 //       TriggerPulls = 0;
 //       TriggerPullsText.text = "Not Started";
 //       CountTextUpdate();
 //       TrigPulled = false;
 //   }
	
	//// Update is called once per frame
	//void Update ()
 //   {
 //       CountTextUpdate();

 //   }

 //   void TrackStatsFromShot()
 //   {
 //       //BlltsLeftinMag--;
 //       HitPerc = (float)Hitcount / (float)ShotsFired;
 //       HitPerc *= 100;
 //   }


 //   void CountTextUpdate()
 //   {
 //       TriggerPullsText.text = "Trigger Pulls: " + TriggerPulls.ToString();
 //       HitCountText.text = "Hit: " + Hitcount.ToString();
 //       BlltsLeftinMagText.text = "Mag: " + BlltsLeftinMag.ToString();
 //       HitPercText.text = "Hit%: " + HitPerc.ToString("F") + "%";
 //   }


}
