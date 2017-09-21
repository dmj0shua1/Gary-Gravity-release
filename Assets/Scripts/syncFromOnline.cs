using UnityEngine;
using System.Collections;

public class syncFromOnline : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void syncData() {

        PlayerPrefs.DeleteAll();
        //download from backend here
    }
}
