using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grandmaAttack : MonoBehaviour {
    Animation hampas;
	// Use this for initialization
	void Start () {
        hampas = GameObject.Find("angry grandma").GetComponent<Animation>();
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void killGary()
    {
        hampas.Play("granda_attack_animation");
    }

    void OnTriggerEnter(Collider grandmaCollider)
    {
        if (grandmaCollider.tag == "character")
        {
            killGary();
        }
    }

  
}
