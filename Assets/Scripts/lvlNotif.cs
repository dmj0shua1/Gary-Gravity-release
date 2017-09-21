using UnityEngine;
using System.Collections;

public class lvlNotif : MonoBehaviour {
    Animator btnCharblinkanim;
	// Use this for initialization
	void Start () {
        btnCharblinkanim = GameObject.Find("Characters").GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void justLeveledUp()
    {
        if (PlayerPrefs.GetInt("justLeveledUp")==1)
        {
            btnCharblinkanim.SetBool("leveledUp", true);
        }
    }
}
