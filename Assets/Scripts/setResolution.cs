using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setResolution : MonoBehaviour {
    public int width, height, refreshRate;
    public bool fullscreen;
	// Use this for initialization
    void Start()
    {
        // Switch to 640 x 480 fullscreen at 60 hz
        Screen.SetResolution(width, height, fullscreen, refreshRate);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
