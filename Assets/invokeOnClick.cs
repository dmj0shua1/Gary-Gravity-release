using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class invokeOnClick : MonoBehaviour {
    public Button btnToClick;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && btnToClick.interactable == true && btnToClick.gameObject.activeSelf)
        {
            btnToClick.onClick.Invoke();
        }
	}


}
