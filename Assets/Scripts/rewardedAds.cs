using UnityEngine;
using System.Collections;

public class rewardedAds : MonoBehaviour {

    public GameObject objRewardedAds;
    scoreCounter scoreCounterScript;
    simpleAd simpleAdScript;
	// Use this for initialization
	void Start () {
	    scoreCounterScript = GameObject.Find("subScoreCounter").GetComponent<scoreCounter>();
        simpleAdScript = GameObject.Find("simpleAd").GetComponent<simpleAd>();

        if (!PlayerPrefs.HasKey("rewardedAdCounter")) PlayerPrefs.SetInt("rewardedAdCounter", 0);
        if (!PlayerPrefs.HasKey("rewardClaimed")) PlayerPrefs.SetInt("rewardClaimed", 1);
       
        }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void rewardedAd()
    {
        if (PlayerPrefs.GetInt("rewardedAdCounter") <= 1 && scoreCounterScript.count >= 50)
        {
            PlayerPrefs.SetInt("rewardedAdCounter", PlayerPrefs.GetInt("rewardedAdCounter") + 1);
        }
        else if (PlayerPrefs.GetInt("rewardedAdCounter") >= 2 && scoreCounterScript.count >= 50)
        {
            
                PlayerPrefs.SetInt("rewardClaimed", 0);
                objRewardedAds.SetActive(true);
        }
    }

    
}
