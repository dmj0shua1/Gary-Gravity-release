using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class achievementMonitor : MonoBehaviour {
    coinReward coinRewardScript;
    [SerializeField]
    int achDemolitionDerby = 0;
	// Use this for initialization
	void Start () {
        coinRewardScript = GameObject.Find("pnlCoinReward").GetComponent<coinReward>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DemolitionDerby()
    {
        if (achDemolitionDerby < 2)
        {
            achDemolitionDerby++;
            print("Derby++");
        }
        else if (achDemolitionDerby >= 2 && !PlayerPrefs.HasKey("CgkI1OXD-eYaEAIQCg"))
        {
            PlayerPrefs.SetInt("CgkI1OXD-eYaEAIQCg", 1);
            PlayGamesManager.UnlockAchievement("CgkI1OXD-eYaEAIQCg");
            GKAchievementReporter.ReportAchievement("CgkI1OXDeYaEAIQCg", 100f, true);
            print("Derby achieved");
            coinRewardScript.giveHardReward();
            
        }
    }
}
