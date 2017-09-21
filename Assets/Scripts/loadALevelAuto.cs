using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadALevelAuto : MonoBehaviour {
    public string GoTo;
	// Use this for initialization
	void Start () {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(GoTo);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
