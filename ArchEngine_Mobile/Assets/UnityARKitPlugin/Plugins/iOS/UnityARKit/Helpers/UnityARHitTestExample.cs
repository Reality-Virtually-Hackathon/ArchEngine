using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


namespace UnityEngine.XR.iOS
{
	public class UnityARHitTestExample : MonoBehaviour
	{
		public Transform m_HitTransform;
		public bool lockModel = false;
		public GameObject particle;
		public Button feedback, feedback2, feedback3;
		public int rayCount = 0;
		public int touchCount = 0;
		public int colliderHitCount = 0;

		public GameObject sceneScripts;
		public GameObject callout1, callout2;
		public GameObject detail1_1, detail1_2, detail2_1, detail2_2;


        bool HitTestWithResultType (ARPoint point, ARHitTestResultType resultTypes)
        {
            List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface ().HitTest (point, resultTypes);
            if (hitResults.Count > 0) {
                foreach (var hitResult in hitResults) {
                    Debug.Log ("Got hit!");
                    m_HitTransform.position = UnityARMatrixOps.GetPosition (hitResult.worldTransform);
                    m_HitTransform.rotation = UnityARMatrixOps.GetRotation (hitResult.worldTransform);
                    Debug.Log (string.Format ("x:{0:0.######} y:{1:0.######} z:{2:0.######}", m_HitTransform.position.x, m_HitTransform.position.y, m_HitTransform.position.z));
                    return true;
                }
            }
            return false;
        }
		
		// Update is called once per frame
		void Update () {
			if (!lockModel) {
				Debug.Log ("Unlocked");
				feedback.GetComponentInChildren<Text> ().text = "Unlocked";
			}
			else
			{
				Debug.Log ("Locked");
				feedback.GetComponentInChildren<Text> ().text = "Locked";
			}

			if (!lockModel && Input.touchCount > 0 && m_HitTransform != null) {
				Debug.Log ("Good screen hit");
				var touch = Input.GetTouch (0);
				if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved) {
					var screenPosition = Camera.main.ScreenToViewportPoint (touch.position);

					if (screenPosition.x > 0.8 || screenPosition.x < 0.2) {
						Debug.Log ("No screen touch");
						return;
					}

					ARPoint point = new ARPoint {
						x = screenPosition.x,
						y = screenPosition.y
					};

					// prioritize reults types
					ARHitTestResultType[] resultTypes = {
						ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent, 
						// if you want to use infinite planes use this:
						//ARHitTestResultType.ARHitTestResultTypeExistingPlane,
						ARHitTestResultType.ARHitTestResultTypeHorizontalPlane, 
						ARHitTestResultType.ARHitTestResultTypeFeaturePoint
					}; 
					
					foreach (ARHitTestResultType resultType in resultTypes) {
						if (HitTestWithResultType (point, resultType)) {
							return;
						}
					}
				}
			} else if (lockModel) {
				//feedback.GetComponentInChildren<Text> ().text = "No touch";

				if (Input.touchCount > 0) {
					touchCount++;
					print ("CALLED");

				}
				GameObject temp;

				for (var i = 0; i < Input.touchCount; ++i) {
					if (Input.GetTouch(i).phase == TouchPhase.Began) {
						Debug.Log ("Cast Ray");
						// Construct a ray from the current touch coordinates
						Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
						// Create a particle if hit
						RaycastHit hit;

						if (Physics.Raycast (ray, out hit)) {
							feedback2.GetComponentInChildren<Text>().text = "Hit: " + hit.collider.gameObject.name;

							// User clicked on Callout 1, zoom in to show further detail
							if (hit.collider.gameObject.tag == "yellow"){
								rayCount++;

								sceneScripts.GetComponent<Visualizer> ().zoomInOnCallout (hit.collider.gameObject);
								//callout1.GetComponent<JumpToEnlarge> ().scaleUp();
								
							} 


						}
					}
				}

			}

		}

		public void lockMovement(){

			Debug.Log ("LockModel : " + lockModel);

			lockModel = !lockModel;
		}

	
	}
}

