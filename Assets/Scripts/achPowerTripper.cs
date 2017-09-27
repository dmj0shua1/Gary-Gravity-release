using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class achPowerTripper : MonoBehaviour {

    public int achPowerTrip = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void powerTrip()
    {
        if (achPowerTrip == 3)
        {
            PlayGamesManager.UnlockAchievement("CgkI1OXD-eYaEAIQEw");
            GKAchievementReporter.ReportAchievement("CgkI1OXDeYaEAIQEw", 100f, true);
        }
    }
}
