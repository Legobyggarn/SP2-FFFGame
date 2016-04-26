using UnityEngine;
using System.Collections;

public class ShellPartScript : MonoBehaviour {

	// TODO: Add so that the scales are modified by the global scale variable

	// TODO: Make a better solution for getting the start and end position before lerping when growing the shell parts

	// Public
	public GameObject shellHandler;
	public GameObject coreGO;

	public float growTime;
	public Vector3 grownScale;
	public Vector3 shrunkScale;

	// Private
	private ShellHandlerScript shellHandlerScript;

	private GameObject globalScaleGO;

	private bool isGrowing = false;
	private float growthTimer;
	private float percentage;

	public Vector3 startPosition;
	public Vector3 endPosition;
	private bool startEndSet = false;

	// Use this for initialization
	void Start () {
	
		shellHandlerScript = shellHandler.GetComponent<ShellHandlerScript>();

		// Calculate volume
		transform.GetComponent<ObjectVolumeScript>().calcVolume(transform.GetComponent<MeshCollider>(), transform.GetComponent<MeshRenderer>());

		// Set timer
		growthTimer = 0f;
		percentage = 0f;

		// Scale up scales
		globalScaleGO = GameObject.Find("GLOBAL_SCALE"); // Find game object for global scale
		grownScale *= globalScaleGO.GetComponent<GlobalScaleScript>().shellScale;
		shrunkScale *= globalScaleGO.GetComponent<GlobalScaleScript>().shellScale;

		// Set positions
		/*
		startPosition;
		endPosition;
		*/	

	}
	
	// Update is called once per frame
	void Update () {
		
		// Growing
		if (isGrowing) {
			growthTimer += Time.deltaTime;
			if (growthTimer < growTime) {
				percentage = growthTimer / growTime;
				// Grow the shell part by lerping the scale and position (y-axis)
				transform.localScale = Vector3.Lerp(shrunkScale, grownScale, percentage);

				/*
				if (!startEndSet) {
					startPosition = coreGO.transform.localPosition;
					endPosition = transform.parent.position;
					startEndSet = true;

					Debug.Log("Start position: " + startPosition + " | End position: " + endPosition);
				}
				*/

				startPosition = coreGO.transform.position;
				endPosition = transform.parent.position;

				transform.position = Vector3.Lerp(startPosition, endPosition, percentage);

			} 

			else {
				isGrowing = false;
				transform.position = transform.parent.transform.position;
				transform.localScale = grownScale;
				growthTimer = 0f;
				// Activate mesh renderer and mesh collider
				GetComponent<MeshCollider>().enabled = true;
				// Reset start- och endPosition
				startEndSet = false;
			}
		}

		/*
		// Shrinking
		else if (isShrinking) {
			growthTimer += Time.deltaTime;
			if (growthTimer < shrinkTime) {
				// Shrink the shell part
				//Debug.Log ("Shrinking...");
				percentage = growthTimer / shrinkTime;
				// Grow the shell part
				// Lerp the scale
				transform.localScale = Vector3.Lerp(grownScale, shrunkScale, percentage);
				// Lerp the position (y-axis)
				//...
				// Deactivate mesh collider
				//GetComponent<MeshCollider>().enabled = false;
			} 

			else {
				//Debug.Log("Done shrinking");
				isShrinking = false;
				transform.localScale = shrunkScale;
				growthTimer = 0f;
				// Deactivate mesh renderer and mesh collider
				//GetComponent<MeshRenderer>().enabled = false;
			}
		}
		*/

	}

	void OnCollisionEnter(Collision collision) {

		/*
		if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "ShellChunk") {
			shellHandlerScript.shellCollision(gameObject);
			// Destroy bullet
			//Debug.Log("Destroy bullet!");
			//Destroy (collision.gameObject);
		}
		*/

	}


	public void HideShell() {
		shellHandlerScript.shellCollision(gameObject);
		// Reset position
		transform.position = startPosition;
	}

	public void GrowShell() {
		isGrowing = true;
	}

	//...

}
