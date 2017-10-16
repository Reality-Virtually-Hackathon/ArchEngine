using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAnim : MonoBehaviour {

    public Animator j;
	// Use this for initialization
	void Start () {

	}

	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            j.GetComponent<Animation>().Play();
        }
    }
}
