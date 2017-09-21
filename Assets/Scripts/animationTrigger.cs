using UnityEngine;
using System.Collections;

public class animationTrigger : MonoBehaviour {

    public GameObject objAnim;
    Animator anim;
    public string triggerName;

	// Use this for initialization
	void Start () {
        anim = objAnim.GetComponent<Animator>();
    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setTrigger()
    {
        anim.SetTrigger(triggerName);       
    }
}
