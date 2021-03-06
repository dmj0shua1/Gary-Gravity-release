﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class scoreCounter : MonoBehaviour
{


    public float MoveSpeed = 1f;
    public int count;
    Text countText1, countText2;
    bool stopScore;
    int freeCoinCounter;
    private GameObject obstacles;
    levelUp lvlUp;

    public bool LavidaLoca = true;

    RdmObjGen rdmObjScript;
    GameObject objRdmobj;
    // Use this for initialization
    void Start()
    {
        countText1 = GameObject.Find("ScoreText").GetComponent<Text>();
        countText2 = GameObject.Find("txtScr").GetComponent<Text>();
        //   bonusCoinsText = GameObject.Find("txtBc").GetComponent<Text>();
        objRdmobj = GameObject.Find("RdmGen");
        rdmObjScript = objRdmobj.GetComponent<RdmObjGen>();
        lvlUp = GetComponent<levelUp>();

        count = 0;
        setCountText();
    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnTriggerEnter(Collider obstacles)
    {
        if (obstacles.gameObject.CompareTag("Obstacles") && stopScore == false)
        {
            count = count + 1;

            //achievements
            if (count == 50) PlayGamesManager.UnlockAchievement("CgkI1OXD-eYaEAIQAw");
            if (count == 100 && LavidaLoca) PlayGamesManager.UnlockAchievement("CgkI1OXD-eYaEAIQBQ");
            if (count == 150) PlayGamesManager.IncrementAchievement("CgkI1OXD-eYaEAIQDw", 5);

            setCountText();

            lvlUp.checkReachedScore();

            if (freeCoinCounter >= 10)
            {
                freeCoinCounter = 0;
                /*  int tempCoins = PlayerPrefs.GetInt("tempGoldCoins");
                  PlayerPrefs.SetInt("tempGoldCoins", tempCoins + 1);
               
                  int Bc = int.Parse(bonusCoinsText.text);
                  Bc += 1;
                  bonusCoinsText.text = Bc.ToString();*/

                //increase difficulty         
                if (rdmObjScript.spawnTimeObs > 0.3f)
                {
                    MoveSpeed += 0.2f;
                    rdmObjScript.spawnTimeObs -= 0.1f;
                    rdmObjScript.spawnTimeCol -= 0.1f;
                }
             /*   else
                {
                    rdmObjScript.spawnTimeObs = 0.3f;
                      MoveSpeed = 2.4f;
                }*/

            }
            else
            {
               
                freeCoinCounter += 1;
            }
        }

    }



    void setCountText()
    {

        countText1.text = count.ToString();
        countText2.text = count.ToString();
    }

    public void stopScoring(bool playerIsDead)
    {
        if (playerIsDead == true)
        {
            stopScore = true;
        }
    }

}
