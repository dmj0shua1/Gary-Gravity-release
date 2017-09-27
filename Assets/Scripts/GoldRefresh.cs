using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SocialPlatforms;

public class GoldRefresh : MonoBehaviour {

    public Text goldText;
	// Use this for initialization
	void Start () {

      
	}
	
	// Update is called once per frame
	void Update () {
        goldText.text = PlayerPrefs.GetInt("PlayerGold").ToString();

        //achievement
        if (PlayerPrefs.GetInt("PlayerGold") >= 1000)
        {
            PlayGamesManager.UnlockAchievement("CgkI1OXD-eYaEAIQEg");
            GKAchievementReporter.ReportAchievement("CgkI1OXDeYaEAIQEg", 100f, true);
        }
	}
}
