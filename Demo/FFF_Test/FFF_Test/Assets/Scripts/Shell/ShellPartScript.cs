using UnityEngine;
using System.Collections;

public class ShellPartScript : MonoBehaviour {
	
	// Public variables
	public GameObject core;

	public float growTime;

	// Private variables
	private ShellHandlerScript shellHandlerScript;
	private GameObject globalScaleGO;

	private Vector3 grownScale = Vector3.one;
	private Vector3 shrunkScale = Vector3.zero;

	private bool isGrowing = false;
	private float growthTimer;
	private float percentage;

	private Vector3 startPosition;
	private Vector3 endPosition;
	private bool startEndSet = false;

	// Use this for initialization
	void Start () {
	
		shellHandlerScript = core.GetComponent<ShellHandlerScript>();

		// Calculate volume
		transform.GetComponent<ObjectVolumeScript>().calcVolume(transform.GetComponent<MeshCollider>(), transform.GetComponent<MeshRenderer>());

		// Set timer
		growthTimer = 0f;
		percentage = 0f;

		// Scale up scales
		globalScaleGO = GameObject.Find("GLOBAL_SCALE"); // Find game object for global scale
		grownScale *= globalScaleGO.GetComponent<GlobalScaleScript>().shellScale;
		//shrunkScale *= globalScaleGO.GetComponent<GlobalScaleScript>().shellScale;	

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

				// Update start and end position (needs to updated every frame because of the movement of the core and target)
				startPosition = core.transform.position;
				endPosition = transform.parent.position;

				// Lerp from start position to end position
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

	}

	public void HideShell() {
		shellHandlerScript.shellCollision(gameObject);
		// Reset position
		transform.position = startPosition;
	}

	public void GrowShell() {
		isGrowing = true;
	}

}
