using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleText : MonoBehaviour {

	public GameObject enabled;
	public GameObject disabled;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	int disableCount = 0;

	public void toggleButtonText(){
		
		if (this.name == "Locked") {
			if (enabled.active)
				enabled.SetActive (false);
			else
				enabled.SetActive(true);
		}

		else if (this.name == "Enable/Disable") {
			if (this.GetComponentInChildren<Text> ().text == "Enable Model") {
				disableCount++;
				this.GetComponentInChildren<Text> ().text = "Disable Count: " + disableCount;
			} else {
				disableCount++;
				this.GetComponentInChildren<Text> ().text = "Enable Count: " + disableCount;
			}
		}
	}
}
