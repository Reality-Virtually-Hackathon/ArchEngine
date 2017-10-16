using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithObject : MonoBehaviour {

	public bool objectEnabled;
	// Use this for initialization
	void Start () {
		objectEnabled = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	public void ToggleEnable()
	{
		objectEnabled = !objectEnabled;		
		this.gameObject.SetActive (objectEnabled);
	}
	
	public void ChangeColor()
	{
		// Get a random color
		Color newColor = new Color( Random.value, Random.value, Random.value, 1.0f );
		
		// Assign it to the cube
		this.GetComponent<Renderer>().material.color = newColor;
	}
		
}
