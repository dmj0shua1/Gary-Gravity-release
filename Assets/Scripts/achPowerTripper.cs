using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            coinRewardScript.giveHardReward();
            PlayerPrefs.SetInt("CgkI1OXD-eYaEAIQEw", 1);
        }
    }
}
