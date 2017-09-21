using UnityEngine;
using System.Collections;

public class claimReward : MonoBehaviour {

	// Use this for initialization
    simpleAd simpleAdScript;
    InternetChecker internetChecker;
    public GameObject internetRequired;
	void Start () {
        simpleAdScript = GameObject.Find("simpleAd").GetComponent<simpleAd>();
        internetChecker = GameObject.Find("InternetChecker").GetComponent<InternetChecker>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void claimRewardAd()

    {
        if (internetChecker.internetConnectBool)
        {
            simpleAdScript.rewardedAd();
        }
        else
        {
            internetRequired.SetActive(true);
        }

        

    }
}
