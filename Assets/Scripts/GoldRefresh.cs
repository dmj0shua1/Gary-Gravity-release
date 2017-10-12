using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoldRefresh : MonoBehaviour {

    public Text goldText;
    coinReward coinRewardScript;
	// Use this for initialization
	void Start () {

        coinRewardScript = GameObject.Find("pnlCoinReward").GetComponent<coinReward>();
	}
	
	// Update is called once per frame
	void Update () {
        goldText.text = PlayerPrefs.GetInt("PlayerGold").ToString();

        //achievement
        if (PlayerPrefs.GetInt("PlayerGold") >= 1000 && !PlayerPrefs.HasKey("CgkI1OXD-eYaEAIQEg"))
        {
            PlayGamesManager.UnlockAchievement("CgkI1OXD-eYaEAIQEg");
            coinRewardScript.giveEasyReward();
            PlayerPrefs.SetInt("CgkI1OXD-eYaEAIQEg", 1);
        }
	}
}
