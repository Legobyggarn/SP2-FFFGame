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
	private float maxSceneChangeTime = 3f;
	private float sceneChangeTime;
	private float maxSceneFadeTime;
	private float sceneFadeTime;

	private bool onPlay = false;
	private bool onOption = false;
	private bool onExit = false;
	private bool fading = true; 
	private bool noNeedToPress = false;
    public Material PlayMat;
    public Material ExitMat;
    public Material OptionMat;
	// Use this for initialization
	void Start () 
	{
        maxSceneChangeTime = 3f;
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
                    float greenColorValue = (255f * sceneChangeTime) / (maxSceneChangeTime*255f);
                    if(greenColorValue > 255f) greenColorValue = 255f;
                    Debug.Log("GreenColorvalue " + greenColorValue);
                    Color finalColor = new Color(0f,greenColorValue, 0f);
                 //   finalColor = new Color(0f, 10f, 0f);
                    PlayMat.SetColor("_EmissionColor", finalColor);
                    Debug.Log("Color value  " + PlayMat.GetColor("_EmissionColor"));
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
                    //Color finalColor = new Color(0f, 0f, 0f);
                   // PlayMat.SetColor("_EmissionColor", finalColor);
                    sceneChangeTime = 0;
                    Debug.Log("sceneChangeTime    " + sceneChangeTime);
                }	
			}
		} 
		else 
		{
            Color finalColor = new Color(0f, 0f, 0f);
            PlayMat.SetColor("_EmissionColor", finalColor);
            sceneChangeTime = 0;
            Debug.Log("sceneChangeTime    " + sceneChangeTime);
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


