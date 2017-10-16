using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpToEnlarge : MonoBehaviour {

	public Button feedback;
	public int rayCount = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void scaleUp(){

		Color newColor = new Color( Random.value, Random.value, Random.value, 1.0f );
		this.GetComponent<Renderer>().material.color = newColor;

	}

}
