using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class achPowerTripper : MonoBehaviour {

    public int achPowerTrip = 0;
    coinReward coinRewardScript;
	// Use this for initialization
	void Start () {
        coinRewardScript = GameObject.Find("pnlCoinReward").GetComponent<coinReward>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void powerTrip()
    {
        if (achPowerTrip == 3 && !PlayerPrefs.HasKey("CgkI1OXD-eYaEAIQEw"))
        {
            PlayGamesManager.UnlockAchievement("CgkI1OXD-eYaEAIQEw");
            GKAchievementReporter.ReportAchievement("CgkI1OXDeYaEAIQEw", 100f, true);
            coinRewardScript.giveHardReward();
            PlayerPrefs.SetInt("CgkI1OXD-eYaEAIQEw", 1);
        }
    }
}
