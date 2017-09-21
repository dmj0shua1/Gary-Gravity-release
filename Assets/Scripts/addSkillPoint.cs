using UnityEngine;
using System.Collections;

public class addSkillPoint : MonoBehaviour {

    playerStats pStatsScript;

	// Use this for initialization
	void Start () {
        pStatsScript = GameObject.Find("Main Camera").GetComponent<playerStats>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void addSP()
    {
        PlayerPrefs.SetInt("skillPoints",PlayerPrefs.GetInt("skillPoints")+1);
        pStatsScript.loadSPSkills();
    }
}
