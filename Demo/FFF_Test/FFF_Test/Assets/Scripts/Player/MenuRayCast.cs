using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MenuRayCast : MonoBehaviour {

	//Public 

	//public GameObject laserSpawn;
	//public GameObject laserPrefab;
	public Color colorStart = Color.red;
	public Color colorEnd = Color.green;
	public float duration = 1.0f; 

	public ScenesTransision st;

	public GameObject go;

	// Private
	private float timeSinceLastShot;
	private float maxSceneChangeTime = 3;
	private float sceneChangeTime;
	private float maxSceneFadeTime;
	private float sceneFadeTime;

	private bool onPlay = false;
	private bool onOption = false;
	private bool onExit = false;
	private bool fading = true; 
	private bool noNeedToPress = false;

	// Use this for initialization
	void Start () 
	{

		maxSceneFadeTime = st.getLerpTime();
	}

	// Update is called once per frame
	void Update () 
	{

		// Shoot            
		if (0.2f < Mathf.Abs(Input.GetAxis("Fire1"))) 
		{ // Keyboard input (Use button instead?)
			// Shot

			// Raycast
			RaycastHit hit;
			if (Physics.Raycast (transform.position, transform.forward, out hit)) 
			{
				//Debug
				Debug.Log ("Debug: Object: [" + hit.transform.name + "] was hit");

				if (hit.transform.tag == "Play") 
				{
					sceneChangeTime += Time.deltaTime;

					float lerp = Mathf.PingPong (Time.time, duration) / duration;
					go.GetComponent<Renderer>().material.color = Color.Lerp (colorStart, colorEnd, lerp);

					Debug.Log ("sceneChangeTime    " + sceneChangeTime);

					if (maxSceneChangeTime < sceneChangeTime) 
					{
						//Debug.Log ("Fading");
						if (fading) 
						{
							st.fadeToWhite ();
							fading = false;
							noNeedToPress = true;
						}
					}					
				}

				else if (hit.transform.tag == "Options") 
				{

				}

				else if (hit.transform.tag == "Exit") 
				{
					sceneChangeTime += Time.deltaTime;

					if (maxSceneChangeTime < sceneChangeTime) 
					{
						Application.Quit ();
					}		

				} 
				else 
				{
					sceneChangeTime = 0;
				}	
			}
		} 
		else 
		{
			sceneChangeTime = 0;
		}

		if(noNeedToPress)
		{
			sceneFadeTime += Time.deltaTime;
			if (maxSceneFadeTime < sceneFadeTime) 
			{
                Debug.Log("GO TO LOADING SCREEN!");
				Application.LoadLevel ("Loading_screen");
			}
		}
	}
}


