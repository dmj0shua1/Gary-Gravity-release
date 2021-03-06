﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class playerStats : MonoBehaviour
{

    GameObject btnCharacters;
    public GameObject objmainmenu, objtopicons, btnObj;
    public GameObject ftStep0, ftStep1, back_shop, upgradedView;
    SfxPlayer sfxPlayerScript;
    animationBoolChanger boolChangerScript;
    AudioSource btnPush, insufficient;
    public string skillName;
    public Text txtSkill, txtSP;
    Text lvlBoost, lvlShield, lvlAttack, txtlvlInfo;
    Slider swipeSlider;
    public int baseUpgradePercentage;
    public Text txtUpgrade, txtNextUpgrade;
    GPGScript gpgScript;

    // Use this for initialization
    void Start()
    {
        gpgScript = GetComponent<GPGScript>();
        
        if (!PlayerPrefs.HasKey("expEarned")) PlayerPrefs.SetFloat("expEarned", 0.0f);
        if (!PlayerPrefs.HasKey("playerLevel")) PlayerPrefs.SetInt("playerLevel", 1);
        if (!PlayerPrefs.HasKey("skillPoints")) PlayerPrefs.SetInt("skillPoints", 1);
        if (!PlayerPrefs.HasKey("swipeCap")) PlayerPrefs.SetInt("swipeCap", 20);

        insufficient = GameObject.Find("sfxInsufficientGold").GetComponent<AudioSource>();

        btnPush = GameObject.Find("sfxBtnPush").GetComponent<AudioSource>();
        btnCharacters = GameObject.Find("Characters");
        try
        {
            boolChangerScript = btnCharacters.GetComponent<animationBoolChanger>();
        }
        catch (System.Exception)
        {
        }

        if (!PlayerPrefs.HasKey("firstSkill") && !ftStep1.activeSelf)
        {
            objmainmenu.SetActive(false);
            objtopicons.SetActive(false);
            try
            { boolChangerScript.changeBoolean(); }
            catch (Exception)
            { }
            ftStep0.SetActive(true);
            back_shop.SetActive(true);
            back_shop.GetComponent<Button>().interactable = false;
        }

        //load SP and skills
        loadSPSkills();
    }

    public void loadSPSkills()
    {
        lvlBoost = GameObject.Find("lvlBoost").GetComponent<Text>();
        lvlShield = GameObject.Find("lvlShield").GetComponent<Text>();
        lvlAttack = GameObject.Find("lvlAttack").GetComponent<Text>();
        txtSP = GameObject.Find("SP").GetComponent<Text>();
        swipeSlider = GameObject.Find("swipeSlider").GetComponent<Slider>();
        if (PlayerPrefs.HasKey("skillPoints")) txtSP.text = "SP " + PlayerPrefs.GetInt("skillPoints").ToString();
        if (PlayerPrefs.HasKey("boostSkillLvl")) lvlBoost.text = PlayerPrefs.GetInt("boostSkillLvl").ToString();
        if (PlayerPrefs.HasKey("shieldSkillLvl")) lvlShield.text = PlayerPrefs.GetInt("shieldSkillLvl").ToString();
        if (PlayerPrefs.HasKey("attackSkillLvl")) lvlAttack.text = PlayerPrefs.GetInt("attackSkillLvl").ToString();
        swipeSlider.value = PlayerPrefs.GetInt("swipeCap");

        try
        {
            txtlvlInfo = GameObject.Find("txt" + txtSkill.name.ToString() + "Info").GetComponent<Text>();
            txtlvlInfo.text = (baseUpgradePercentage * PlayerPrefs.GetInt(skillName + "Lvl")).ToString() + "% -> " + (baseUpgradePercentage * (PlayerPrefs.GetInt(skillName + "Lvl") + 1)).ToString() + "%";
        }
        catch (Exception)
        {
            print("upgraded Info loaded");
        }

        int curLvl = Convert.ToInt32(txtSkill.text.ToString());
        if (curLvl >= 5)
        {
            maxOut();

        }


    }

    // Update is called once per frame
    public void upgradeSkill()
    {   //check if first time

        if (PlayerPrefs.GetInt("skillPoints") >= 1 && PlayerPrefs.GetInt(skillName + "Lvl") < 5)
        {
            //decrease skillpoint
            PlayerPrefs.SetInt("skillPoints", PlayerPrefs.GetInt("skillPoints") - 1);
            txtSP.text = "SP " + (Convert.ToInt32(txtSP) - 1).ToString();
            //upgrade
            PlayerPrefs.SetInt(skillName + "Lvl", PlayerPrefs.GetInt(skillName + "Lvl") + 1);
            loadSPSkills();



            if (!PlayerPrefs.HasKey("firstSkill"))
            {
                //add chosen skill to db
                PlayerPrefs.SetString("firstSkill", skillName);

            }
            else
            {
                upgradedView.SetActive(true);
                txtUpgrade.text = "cooldown speed upgraded by " + (baseUpgradePercentage * PlayerPrefs.GetInt(skillName + "Lvl")).ToString() + "%";
                txtNextUpgrade.text = "Next upgrade -> " + (baseUpgradePercentage * (PlayerPrefs.GetInt(skillName + "Lvl") + 1)).ToString() + "%";
                if (PlayerPrefs.GetInt(skillName + "Lvl") >= 5) PlayGamesManager.UnlockAchievement("CgkI1OXD-eYaEAIQDg");
                PlayGamesManager.UnlockAchievement("CgkI1OXD-eYaEAIQAQ");
            }

        }
        else
        {
            insufficient.Play();
        }



    }

    void maxOut()
    {
        txtlvlInfo.text = "MAX";
        btnObj.SetActive(false);

    }
}
