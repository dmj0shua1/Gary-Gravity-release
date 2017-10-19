using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coinReward : MonoBehaviour {

    
    [SerializeField]
    private int easyAmount, mediumAmount, hardAmount;
    [SerializeField]
    Animator animCoinReward;
    [SerializeField]
    string triggerName;
    [SerializeField]
    Text txtCoinReward;
    [SerializeField]
    AudioSource sfxCoin;
    uigoldUpdater uigoldScript;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void giveEasyReward()
    {
        PlayerPrefs.SetInt("PlayerGold", PlayerPrefs.GetInt("PlayerGold") + easyAmount);
        txtCoinReward.text = "You received "+easyAmount.ToString() +" coins!";
        animCoinReward.SetTrigger(triggerName);
        if (PlayerPrefs.GetInt("Sfx") == 1) sfxCoin.Play();
        uigoldScript.updateGoldCoin();

    }

    public void giveMediumReward()
    {
        PlayerPrefs.SetInt("PlayerGold", PlayerPrefs.GetInt("PlayerGold") + mediumAmount);
        txtCoinReward.text = "You received " + mediumAmount.ToString() + " coins!";
        animCoinReward.SetTrigger(triggerName);
        if (PlayerPrefs.GetInt("Sfx") == 1) sfxCoin.Play();
        uigoldScript.updateGoldCoin();
    }

    public void giveHardReward()
    {
        PlayerPrefs.SetInt("PlayerGold", PlayerPrefs.GetInt("PlayerGold") + hardAmount);
        txtCoinReward.text = "You received " + hardAmount.ToString() + " coins!";
        animCoinReward.SetTrigger(triggerName);
        if (PlayerPrefs.GetInt("Sfx") == 1) sfxCoin.Play();
        uigoldScript.updateGoldCoin();
    }
}
