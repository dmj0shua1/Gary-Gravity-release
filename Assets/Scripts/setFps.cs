using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setFps : MonoBehaviour {
    [SerializeField]
    private int Fps = 30;
    [SerializeField]
    private int vSync = 0;
	// Use this for initialization
	void Start () {
        QualitySettings.vSyncCount = vSync;
        Application.targetFrameRate = Fps;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
