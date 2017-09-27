using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SocialPlatforms;

public class levelUp : MonoBehaviour {

    public int score;
    public int[] lvl;
    public int lvlNum;
    scoreCounter scoreCounter;
    Text level,txtlvlup;
    Text skillpoints;
    Text txtLvl, txtExpReq,txtExpCur;
    public float expTotal,tempExpTotal;
    Slider expSlider;
    Animator lvlUpAnim;
    levelUpAnimation lvlUpAnimationScript;
    public int swipeBaseValue = 20,swipeMultiplier = 1;
	// Use this for initialization
	void Start () {
        scoreCounter = GameObject.Find("subScoreCounter").GetComponent<scoreCounter>();
        if (!PlayerPrefs.HasKey("expEarned")) PlayerPrefs.SetFloat("expEarned", 0.0f);  
        if (!PlayerPrefs.HasKey("playerLevel")) PlayerPrefs.SetInt("playerLevel", 1);
        if (!PlayerPrefs.HasKey("skillPoints")) PlayerPrefs.SetInt("skillPoints", 1);
      //  if (!PlayerPrefs.HasKey("swipeCap")) PlayerPrefs.SetInt("swipeCap", swipeBaseValue);
        lvlNum = 0;


        txtlvlup = GameObject.Find("txtlvlup2").GetComponent<Text>();
        lvlUpAnim = GameObject.Find("lvlUpPanel").GetComponent<Animator>();

        //display temporary code  

        skillpoints = GameObject.Find("skillpoints").GetComponent<Text>();
        level= GameObject.Find("level").GetComponent<Text>();
        level.text = "Level: " + PlayerPrefs.GetInt("playerLevel").ToString() ;
        skillpoints.text = "Skillpoints: " + PlayerPrefs.GetInt("skillPoints").ToString();

        //print required xp and current level
        txtLvl = GameObject.Find("txtLvl").GetComponent<Text>();
        txtExpReq = GameObject.Find("txtExpReq").GetComponent<Text>();
        expSlider = GameObject.Find("expSlider").GetComponent<Slider>();
        txtExpCur = GameObject.Find("txtExpCur").GetComponent<Text>();

        lvlUpAnimationScript = GameObject.Find("subScoreCounter").GetComponent<levelUpAnimation>();
     //   PlayerPrefs.SetFloat("expEarned", lvl[lvl.Length - 1] - 1);
        checkReachedScore();

       
       
	}

	// Update is called once per frame
	void Update () {
	
	}

    public void checkReachedScore()
    {

        var expEarned = PlayerPrefs.GetFloat("expEarned");
        if (expEarned < lvl[lvl.Length-1])
        {
            score = scoreCounter.count;
            var expReq = lvl[lvlNum];
            float scoreToExp = (score / 10.0f);
            expTotal = scoreToExp + expEarned;
            tempExpTotal = expTotal;
            int tempExpTotalInt = Convert.ToInt32(tempExpTotal);
            txtExpCur.text = tempExpTotalInt.ToString();
            print("expTotal: " + expTotal.ToString());
            if (scoreToExp == expReq)
            {
                lvlNum += 1;
                //display temporary code
                level.text = "Level: " + PlayerPrefs.GetInt("playerLevel").ToString();
                skillpoints.text = "Skillpoints: " + PlayerPrefs.GetInt("skillPoints").ToString();
            }

        }
        
    }
    public void lvlUp()
    {

        if (expTotal >= lvl[PlayerPrefs.GetInt("playerLevel") - 1])
        {
            PlayerPrefs.SetInt("playerLevel", PlayerPrefs.GetInt("playerLevel") + 1);
             
            //increase swipe
            PlayerPrefs.SetInt("swipeCap", swipeBaseValue + (PlayerPrefs.GetInt("playerLevel") * swipeMultiplier));

            //notify code here
            PlayerPrefs.SetInt("skillPoints", PlayerPrefs.GetInt("skillPoints") + 1);

            //achievement
            if (PlayerPrefs.GetInt("playerLevel") >= 15)
            {
                PlayGamesManager.UnlockAchievement("CgkI1OXD-eYaEAIQEQ");
                GKAchievementReporter.ReportAchievement("CgkI1OXDeYaEAIQEQ",100f,true);
            }

            txtlvlup.text = "LVL " + PlayerPrefs.GetInt("playerLevel").ToString() + "!";
            lvlUpAnim.SetBool("leveledUp", true);

            PlayerPrefs.SetInt("justLeveledUp", 1);
        }

        print("expreq: "+(PlayerPrefs.GetInt("playerLevel") - 1).ToString());
        PlayerPrefs.SetFloat("expEarned", expTotal);
        txtLvl.text = "lvl "+ PlayerPrefs.GetInt("playerLevel").ToString();
        txtExpReq.text = lvl[PlayerPrefs.GetInt("playerLevel") - 1].ToString();
        int txtExpReqInt = Convert.ToInt32(txtExpReq.text.ToString());
       // txtExpCur.text = expTotal.ToString();

        //tempslider

        if (txtExpReqInt > lvl[0])
        {
            expSlider.maxValue = txtExpReqInt / 2;
            expSlider.value = tempExpTotal - (txtExpReqInt/2);
            lvlUpAnimationScript.invokeAnimateLevelUpPreceding();
        }
        else
        {
            expSlider.maxValue = lvl[PlayerPrefs.GetInt("playerLevel") - 1];
            expSlider.value = tempExpTotal;
            lvlUpAnimationScript.invokeAnimateLevelUp();
        }
        
    }

    void lvlUpNotifClose()
    {
        lvlUpAnim.SetBool("leveledUp", false);
    }

   
}
