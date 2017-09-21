using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class simpleAd : MonoBehaviour {

	// Use this for initialization
    public GameObject objRewardedAds;
    Text txtFreeCoinAmount;
    AudioSource coinAudio;
    Text txtuigold;
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            PlayerPrefs.SetInt("adsCounter",0);
        }

        if (!PlayerPrefs.HasKey("adsDisabled"))
        {
            PlayerPrefs.SetInt("adsDisabled", 0);
        }
        txtFreeCoinAmount = GameObject.Find("freeCoinsAmount").GetComponent<Text>();
        coinAudio = GameObject.Find("sfxBuySound").GetComponent<AudioSource>();
        txtuigold = GameObject.Find("txtuigold").GetComponent<Text>();
    }

    public void gameOverAd()
    {
        if (PlayerPrefs.GetInt("adsCounter") <= 3 )
        {
            PlayerPrefs.SetInt("adsCounter",PlayerPrefs.GetInt("adsCounter") + 1);
        }
        else
        {
            PlayerPrefs.SetInt("adsCounter", 0);
            Invoke("showAdInvoked", 1.0f);
        }
    }

    private void showAdInvoked()
    {
        ShowAd();
    }

	 public void ShowAd()
  {
      if (Advertisement.IsReady())
    {
        Advertisement.Show();
    }
  }

     public void DisableAds()
     {
         PlayerPrefs.SetInt("adsDisabled", 1);
     }

     public void rewardedAd()
     {
         if (Advertisement.IsReady("rewardedVideo"))
         {
             Advertisement.Show("rewardedVideo");

        /*     PlayerPrefs.SetInt("rewardedAdCounter", 0);
             PlayerPrefs.SetInt("rewardClaimed",1);*/
             objRewardedAds.SetActive(false);
             Invoke("displayCoinReceived", 1.5f);
          
         }
     }

     void displayCoinReceived()
     {
         int randomCoin = UnityEngine.Random.Range(10, 15);
         PlayerPrefs.SetInt("PlayerGold", PlayerPrefs.GetInt("PlayerGold") + randomCoin);
         txtFreeCoinAmount.text = "You got " + randomCoin.ToString() + " free coins!";
         coinAudio.PlayDelayed(1f);

         int curGold = Convert.ToInt32(txtuigold.text.ToString());
         txtuigold.text = (curGold + randomCoin).ToString();
     }
	}

