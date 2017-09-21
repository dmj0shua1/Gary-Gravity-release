using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chooseStage : MonoBehaviour {
    ChangeScene changeSceneScript;
    public string goTo;
    public GameObject coin,price,imgPlay;
    AudioSource purchase, insufficient,btnSfxPush;
    public int itmPrice;
    public GameObject mainMenu, stagesBackBtn;
    animationBoolChanger boolChangerScript;
	// Use this for initialization
	void Start () {
        changeSceneScript = GameObject.Find("Play").GetComponent<ChangeScene>();
        purchase = GameObject.Find("sfxPurchasing").GetComponent<AudioSource>();
        insufficient = GameObject.Find("sfxInsufficientGold").GetComponent<AudioSource>();
        btnSfxPush = GameObject.Find("sfxBtnPush").GetComponent<AudioSource>();
        boolChangerScript = GameObject.Find("scrollViewStages").GetComponent<animationBoolChanger>();
        PlayerPrefs.SetInt("stageGame_unlocked", 1);

        if (PlayerPrefs.HasKey("stage" + goTo + "_unlocked"))
        {
            coin.SetActive(false);
            price.SetActive(false);
            imgPlay.SetActive(true);
        }
        

	}
	
	// Update is called once per frame
	void Update () {
		
	}



    public void choose()
    {
        if (PlayerPrefs.HasKey("stage" + goTo + "_unlocked"))
        {
            PlayerPrefs.SetString("chosenStage", goTo);
            changeSceneScript.GoTo = goTo;
            mainMenu.SetActive(true);
            stagesBackBtn.SetActive(false);
            btnSfxPush.Play();
            boolChangerScript.changeBoolean();

        }
        else
        { 
     
        if (PlayerPrefs.GetInt("PlayerGold") >= itmPrice)
            {
                //buy it
                PlayerPrefs.SetInt("PlayerGold", PlayerPrefs.GetInt("PlayerGold") - itmPrice);
                PlayerPrefs.SetInt("stage" + goTo + "_unlocked",1);
                purchase.Play();
                coin.SetActive(false);
                price.SetActive(false);
                imgPlay.SetActive(true);
                changeSceneScript.GoTo = goTo;
                
            }
            else
            {
                //insufficient gold
                insufficient.Play();
            }
        }

      
    }
}
