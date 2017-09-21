using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class levelUpAnimation : MonoBehaviour {

    levelUp lvlUpScript;
    Slider expSlider;
    Text txtExpCur, txtExpReq;
    float expTotal,tempExpTotal;
	// Use this for initialization
	void Start () {
        expSlider = GameObject.Find("expSlider").GetComponent<Slider>();
        lvlUpScript = GameObject.Find("subScoreCounter").GetComponent<levelUp>();
        txtExpCur = GameObject.Find("txtExpCur").GetComponent<Text>();
        txtExpReq = GameObject.Find("txtExpReq").GetComponent<Text>();
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void invokeAnimateLevelUp()
    {
        InvokeRepeating("animateLevelUp",1.3f,0.01f);
    }

    void animateLevelUp()
    {
        expTotal = lvlUpScript.expTotal;
     
        if (expSlider.value >= expTotal)
        {
            CancelInvoke("animateLevelUp");
        }
        else
        {
            expSlider.value += 1f;
            int exp = Convert.ToInt32(expSlider.value);
            txtExpCur.text = exp.ToString();
        }
    }

    public void invokeAnimateLevelUpPreceding()
    {
        InvokeRepeating("animateLevelUpPreceding", 1.3f, 0.01f);
    }

    void animateLevelUpPreceding()
    {
        expTotal = lvlUpScript.expTotal;
        int txtExpReqInt = Convert.ToInt32(txtExpReq.text.ToString());
        float offsetExpTotal = expTotal - (txtExpReqInt / 2);
        if (expSlider.value >= offsetExpTotal)
        {
            CancelInvoke("animateLevelUpPreceding");
        }
        else
        {
            expSlider.value += 1f;
            int exp = Convert.ToInt32(expSlider.value) + Convert.ToInt32((txtExpReqInt /2 ));
            txtExpCur.text = exp.ToString();
        }
    }
}
