﻿using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
public class objectdestroyer : MonoBehaviour {

	// Use this for initialization
    public GameObject explosion,btnShieldObj;
    public Boolean isDestroyer = false;
    shieldToggle shieldToggleScript;
    cooldownShield cdShieldScript;
    int achTv = 0;
    spawnProjectile spawnProjectileScript;
    coinReward coinRewardScript;
    achievementMonitor achMonitorScript;
	void Start () {
        cdShieldScript = GameObject.Find("cooldownShield").GetComponent<cooldownShield>();
        spawnProjectileScript = GameObject.Find("btnAttack").GetComponent<spawnProjectile>();
        coinRewardScript = GameObject.Find("pnlCoinReward").GetComponent<coinReward>();
        achMonitorScript = GameObject.Find("achievementMonitor").GetComponent<achievementMonitor>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider obj)
    {

    
        if (isDestroyer && (obj.tag == "Foreground"|| obj.tag == "Obstacles"))
        {
            obj.gameObject.SetActive(false);
        }
        else if(obj.tag == "Obstacles")
        {

            if (gameObject.name == "playerShield")
            {
                try
                { Instantiate(explosion, transform.position, transform.rotation); }
                catch (Exception e) { }
                btnShieldObj = GameObject.Find("btnShield");
                /*if (SceneManager.GetActiveScene().name == "Game") */shieldToggleScript = btnShieldObj.GetComponent<shieldToggle>();
                shieldToggleScript.deactivateShield();
                obj.gameObject.SetActive(false);
                GameObject objSfxDestByShield = GameObject.Find("sfxDestroyedByShield");
                AudioSource asSfxDestByShield = objSfxDestByShield.GetComponent<AudioSource>();
                if (PlayerPrefs.GetInt("Sfx") == 1) asSfxDestByShield.Play();
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                BoxCollider colPlayer = player.GetComponent<BoxCollider>();
                cdShieldScript.regenShield();

                //achievement
                if (obj.gameObject.name == "Tv(Clone)")
                {
                    if (achTv < 4)
                    {
                        achTv++;
                    }
                    else if (achTv >= 4 && !PlayerPrefs.HasKey("CgkI1OXD-eYaEAIQEA"))
                    {
                        PlayerPrefs.SetInt("CgkI1OXD-eYaEAIQEA", 1);
                        PlayGamesManager.UnlockAchievement("CgkI1OXD-eYaEAIQEA");
                        coinRewardScript.giveMediumReward();
                   
                    }
                }
            }
            else if (gameObject.tag == "PowerAttack")
            {
                GameObject objSfxDestByAttack = GameObject.Find("sfxDestroyedByAttack");
                AudioSource asSfxDestByAttack = objSfxDestByAttack.GetComponent<AudioSource>();
                if (PlayerPrefs.GetInt("Sfx") == 1) asSfxDestByAttack.Play();
              
                Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);

                Invoke("destroyExp", 0.5f);
                Destroy(gameObject);
                obj.gameObject.SetActive(false);

                //achievement
                

                if (spawnProjectileScript.achBlindsight >=1 ) spawnProjectileScript.achBlindsight--;

                if (obj.gameObject.name == "Tv(Clone)")
                {
                    if (achTv < 4)
                    {
                        achTv++;
                    }
                    else if (achTv >= 4 && !PlayerPrefs.HasKey("CgkI1OXD-eYaEAIQEA"))
                    {
                        PlayerPrefs.SetInt("CgkI1OXD-eYaEAIQEA", 1);
                        PlayGamesManager.UnlockAchievement("CgkI1OXD-eYaEAIQEA");
                        coinRewardScript.giveMediumReward();
                       
                    }
                }

                achMonitorScript.DemolitionDerby();
            }
          
          
           
        }
    }

    void destroyExp()
    {
        Destroy(explosion);
    }

    
}
