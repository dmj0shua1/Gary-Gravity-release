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
        if (PlayerPrefs.GetInt("PlayerGold") >= 1000)
        {
            PlayGamesManager.UnlockAchievement("CgkI1OXD-eYaEAIQEg");
            coinRewardScript.giveEasyReward();
        }
	}
}
