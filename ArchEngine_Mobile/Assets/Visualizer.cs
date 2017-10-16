using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Visualizer : MonoBehaviour {
    float duration = 1;
    float alpha = 0;
    Color ColorTrans;

    bool animationFinished = false;
    public Scene nextScene;

    public GameObject building;
	public GameObject stairModel, explodeStair;
	public Material setall;
    private GameObject[] comps;
    private Color[] origColors;
    public GameObject zoomie;

    private List<GameObject> positions = new List<GameObject>();
    private List<GameObject> boxes = new List<GameObject>();
    private List<float> scales = new List<float>();
    // Use this for initialization
    void Start () {
        
		/*foreach (GameObject dink in comps)
		{
			if(dink.tag == "red")
			{
				dink.GetComponent<Renderer>().material = setall;
			}
		}*/
        int children = building.transform.childCount;
        comps = new GameObject[children];
        origColors = new Color[children];
        for (int i = 0; i < children; ++i)
        {
            try
            {
                //print("For loop: " + transform.GetChild(i));
                comps[i] = building.transform.GetChild(i).gameObject;
                origColors[i] = building.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color;
            }
            catch { };
        }
        
    }
    
    // Update is called once per frame
    // when  you press teh bue buttons, it hides everything that isnt blue, when you press the red it hides everything that isnt red, when you press green it hides evertyhing that wasnt green
    void Update () {

		// Layer function - Structural
        if (Input.GetKeyDown(KeyCode.B))
        {
            RestoreColors();
            FadeoutEverythingOtherThan("blue");
        }

		// Layer function - Furniture
        if (Input.GetKeyDown(KeyCode.G))
        {
            RestoreColors();
            FadeoutEverythingOtherThan("green");
        }

		// Layer function - Mechanical
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestoreColors();
            FadeoutEverythingOtherThan("red");
        }

		// Back to base model
        if (Input.GetKeyDown(KeyCode.O))
        {
            RestoreColors();
        }

		//
        if (Input.GetKeyDown(KeyCode.I))
        {
            print("called i");
            StartCoroutine(ScaleUp(building, zoomie));
           // StartCoroutine(waitforNewScne());
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            StartCoroutine(ScaleDown(building, zoomie));
        }
        Vector3 speed = new Vector3(2, 2, 2);


    }
    private void RestoreColors()
    {
        for (int i = 0; i < comps.Length; ++i)
        {
			if (comps[i].gameObject.tag != "yellow")
            	StartCoroutine(FadeIn(comps[i].gameObject));

        }
    }
    IEnumerator waitforNewScne()
    {
        for (float f = 1f; f <= 5f; f += 0.1f)
        {
            yield return new WaitForSeconds(.000000005f);
        }
        ChangeScene();
        
    }

    IEnumerator ScaleUp(GameObject MITmodel, GameObject calloutPosition)
    {
		calloutPosition.gameObject.transform.parent = null;
		MITmodel.transform.parent = calloutPosition.transform;
		print(MITmodel.transform.parent);

        for (float f = .15f; f <= 0.75f; f += 0.015f)
        {
			calloutPosition.transform.localScale = new Vector3(f,f,f);
            yield return new WaitForSeconds(.000000005f);
            
        }
		MITmodel.transform.parent = null;
		calloutPosition.gameObject.transform.parent = building.transform;
		calloutPosition.SetActive (false);

    }


    IEnumerator ScaleDown(GameObject g, GameObject position)
    {
		position.gameObject.transform.parent = null;
        g.transform.parent = position.transform;
        print(g.transform.parent);

        for (float f = .15f; f >= 0.75f; f -= 0.015f)
        {
            position.transform.localScale = new Vector3(f, f, f);
            yield return new WaitForSeconds(.000000005f);
        }
        g.transform.parent = null;
		position.gameObject.transform.parent = building.transform;
		position.SetActive (true);
    }


    private void FadeoutEverythingOtherThan(string colorTag)
    {
        foreach (GameObject g in comps)
        {
            if (g.tag != colorTag)
            {
                StartCoroutine(FadeOut(g));
            }
        }
    }


    IEnumerator FadeOut(GameObject g)
    {

        if(g.GetComponent<Renderer>().material.color != null)
        {
            for (float f = 1f; f >= .33; f -= 0.01f)
            {

                //print("called " + f);
                Color c = g.GetComponent<Renderer>().material.color;
                c.a = f;
                g.GetComponent<Renderer>().material.color = c;
                yield return new WaitForSeconds(.0005f);
            }
        }
        
    }
    IEnumerator FadeIn(GameObject g)
    {

        if (g.GetComponent<Renderer>().material.color != null)
        {

            for (float f = .33f; f <= 1f; f += 0.01f)
            {

                //print("called " + f);
                Color c = g.GetComponent<Renderer>().material.color;
                c.a = f;
                g.GetComponent<Renderer>().material.color = c;
                yield return new WaitForSeconds(.0005f);
            }
        }
    }

    void ChangeScene()
    {
        Application.LoadLevel("staircase");
    }


	public bool mechanicalLayerEnabled = false;
	public bool structLayerEnabled = false;
	public bool furnitureLayerEnabled = false;

	// Layer function - Structural
	public void viewLayer(string colorOfLayer){

		if (colorOfLayer == "green") {
			
			if (!mechanicalLayerEnabled) {
				RestoreColors ();
				FadeoutEverythingOtherThan (colorOfLayer);
				mechanicalLayerEnabled = true;
				structLayerEnabled = false;
				furnitureLayerEnabled = false;
			} else {
				RestoreColors ();
				mechanicalLayerEnabled = false;
			}

		} else if (colorOfLayer == "red") {

			if (!structLayerEnabled) {
				RestoreColors ();
				FadeoutEverythingOtherThan (colorOfLayer);
				structLayerEnabled = true;
				mechanicalLayerEnabled = false;
				furnitureLayerEnabled = false;
			} else {
				RestoreColors ();
				structLayerEnabled = false;
			}

		} else if (colorOfLayer == "blue") {

			if (!furnitureLayerEnabled) {
				RestoreColors ();
				FadeoutEverythingOtherThan (colorOfLayer);
				furnitureLayerEnabled = true;
				structLayerEnabled = false;
				mechanicalLayerEnabled = false;
			} else {
				RestoreColors ();
				furnitureLayerEnabled = false;
			}

		}
	}



	public void zoomInOnCallout(GameObject position){

		StartCoroutine(ScaleUp (building, position));

	}

	public void colorRestoration(){
		RestoreColors ();
	}

	public void homePressed(GameObject position){
		StartCoroutine(ScaleDown (building, position));

	}
	public bool stairsExploded = false;

	public void explodeStairs(){

		if (!stairsExploded) {
			explodeStair.SetActive (true);
			stairModel.SetActive (false);
			explodeStair.GetComponent<Animator> ().SetTrigger ("explod");
			stairsExploded = true;
		} else {
			explodeStair.GetComponent<Animator> ().SetTrigger ("unexplode");
			explodeStair.SetActive (false);
			stairModel.SetActive (true);
			stairsExploded = false;
		}



	}


}



