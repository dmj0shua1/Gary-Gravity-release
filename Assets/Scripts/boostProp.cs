using UnityEngine;
using System.Collections;

public class boostProp : MonoBehaviour {
    public GameObject objBoostProp,objIdleProp,swipeLeftProp,swipeRightProp;
    Animator animleftProp, animrightProp;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void redisableGaryPropLeft()
    {
        animleftProp = swipeLeftProp.GetComponent<Animator>();
        Invoke("disableSwipeLeftProp", 0.7f);
        swipeLeftProp.SetActive(true);
        animleftProp.Play("puff_anim", -1, 0f);
    }

    public void redisableGaryPropRight()
    {

        animrightProp = swipeRightProp.GetComponent<Animator>();
        Invoke("disableSwipeRightProp", 0.7f);
        swipeRightProp.SetActive(true);
        animrightProp.Play("puff_animSwipeRight", -1, 0f);
    }

    void disableSwipeLeftProp()
    {
        swipeLeftProp.SetActive(false);
    }

    void disableSwipeRightProp()
    {

        swipeRightProp.SetActive(false);
    }
}
