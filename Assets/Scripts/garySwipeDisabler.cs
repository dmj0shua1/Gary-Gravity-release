using UnityEngine;
using System.Collections;

public class garySwipeDisabler : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("disableSelf", 0.8f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void disableSelf()
    {
        gameObject.SetActive(false);
    }
}
