using UnityEngine;
using System.Collections;

public class MenuRayCast : MonoBehaviour {

	//Public 
	public float shotDelay; // Delay (in seconds) between shots
	public GameObject laserSpawn;
	public GameObject laserPrefab;

	// Private
	private float timeSinceLastShot;
	private float maxSceneChangeTime = 3;
	private float sceneChangeTime;
	private bool sameSquare;
	private bool lifeTime;
	private GameObject go;

	// Use this for initialization
	void Start () {

		timeSinceLastShot = 0.0f;
		lifeTime = true;

	}

	// Update is called once per frame
	void Update () {

		// Shoot
			if (Input.GetKey(KeyCode.Space)) { // Keyboard input (Use button instead?)
				// Shot

				// Raycast
				RaycastHit hit;
				if (Physics.Raycast(transform.position, transform.forward, out hit)) {
					//Debug
					Debug.Log("Debug: Object: [" + hit.transform.name + "] was hit");

					if (hit.transform.tag == "Play") 
					{
						sceneChangeTime += Time.deltaTime;

						if (maxSceneChangeTime < sceneChangeTime) 
						{
							Application.LoadLevel ("Scene_01");
						}					
					} 
					else if (hit.transform.tag == "Options") 
					{
					
					} 
					else if (hit.transform.tag == "Exit") 
					{
						Application.Quit();
					}

					//Spawn laser
					go = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
					//Vector3 laserSpawnPoint = transform.position + transform.forward + -transform.up * 0.5f;
					go.GetComponent<LineRenderer>().SetPosition(0, laserSpawn.transform.position );
					go.GetComponent<LineRenderer>().SetPosition(1, hit.point);
					//go.transform.SetParent(this);

					// Set has shoot
					timeSinceLastShot = 0.0f;
					lifeTime = false;
				}
			}

		else {
			if (timeSinceLastShot >= shotDelay) { // Shot delay over
				lifeTime = true;
				Destroy (go.transform.gameObject);
			}
			else {
				timeSinceLastShot += Time.deltaTime;
			}
		}

	}
}

