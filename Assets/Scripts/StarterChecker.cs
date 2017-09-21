﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StarterChecker : MonoBehaviour {

	// Use this for initialization
    GameObject objBoost, objAttack, objShield, maincam;
    Button btnBoost,btnAttack,btnShield;
    RdmObjGen rdmobj;
	void Start () {
        if (PlayerPrefs.HasKey("endTimeboost"))
        {
            objBoost = GameObject.Find("btnBoost");
            btnBoost = objBoost.GetComponent<Button>();
            btnBoost.interactable = true;
        }

        if (PlayerPrefs.HasKey("endTimeshield"))
        {
            objShield = GameObject.Find("btnShield");
            btnAttack = objShield.GetComponent<Button>();
            btnAttack.interactable = true;
        }

        if (PlayerPrefs.HasKey("endTimeattack"))
        {
            objShield = GameObject.Find("btnAttack");
            btnAttack = objShield.GetComponent<Button>();
            btnAttack.interactable = true;
        }

       
	}
	
	// Update is called once per frame
	void Update () {
	        
	}
}
