using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uigoldUpdater : MonoBehaviour {
    int tempGold, pGold;
    Text txtUiGold;
	// Use this for initialization
	void Start () {
        
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void updateGoldCoin(){
        txtUiGold = GameObject.Find("txtuigold").GetComponent<Text>();
        pGold = Convert.ToInt32(PlayerPrefs.GetInt("PlayerGold"));
        tempGold = Convert.ToInt32(PlayerPrefs.GetInt("tempGoldCoins"));
        txtUiGold.text = (pGold + tempGold).ToString();
        
    }

   
}
