using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MiniCapsuleScript : MonoBehaviour {

	// Public variables
	public int hitsToDestroy = 1;
	public int explosionForce = 0;
	public int explosionRadius = 0;
	public GameObject coreGameObjectPrefab; // The object that will be inside the capsule. Is spawned when the capsule is destroyed.
	// Sound and music game object
	public GameObject soundAndMusic;
	// Shake [combined attribute]
	[Header("Shake")]
	public float baseShakeTime = 1f;
	public float shakeTimeMultiplier = 1f; // How much the shake time should increase each time the capsule is hit but not destroyed
	public float baseShakeAmount = 1f; // XYZ (add a seperate for x,y,z)? [combined attribute]
	public float shakeAmountMultiplier = 1f; // How much the shake amount should increase each time the capsule is hit but not destroyed
	public float shellShakeChange = 0f;


	// Private variables
	private int hits = 0;
	private List<GameObject> shellParts = new List<GameObject>();
	// Shake
	private bool isShaking = false;
	private float currShakeTime;
	private float currShakeAmount;
	private Vector3 startShakePos;
	private float currShellShakeChange;
	// Sound and music variable script?
	private SondAndMusic_Var soundAndMusicScript;


	// Use this for initialization
	void Start () {
	
		// Get all shell parts (use a public GameObject variable to identify the child/model?)
		for (int i = 0; i < transform.GetChild(0).childCount; i++) {

			// Add new rigidbody to 'shellParts'
			shellParts.Add(transform.GetChild(0).GetChild(i).gameObject);

			// Settings for the rigidbody of child
			// Get the rigidbody of the child of index i
			Rigidbody rb = transform.GetChild(0).GetChild(i).gameObject.GetComponent<Rigidbody>();
			// "Disable" rigidbody by setting 'isKinematic' to true and 'detectCollision' to false
			rb.isKinematic = true;
			rb.detectCollisions = false;

			if (soundAndMusic != null) {
				soundAndMusicScript = soundAndMusic.GetComponent<SondAndMusic_Var>();
			}

		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Handles collision when they first happen
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Bullet") {
			hits++;

			checkDestroySelf();
			// Temporary? (Destroys the bullet)
			Destroy(collision.gameObject);
		}
	}

	// Check if shell should be destroyed
	private void checkDestroySelf() {
		if (hits >= hitsToDestroy) { // POI was destroyed
			// /Disable/destroy self...
			// (Add explosion force to the shell parts, disable/destroy self)

			GetComponent<Collider> ().enabled = false;

			// Explode into multiple shell parts
			foreach (GameObject go in shellParts) {

				// Set back scale to 1. (Maybe not?)
				go.transform.localScale = Vector3.one;

				// Setting for the rigidbody
				Rigidbody rb = go.GetComponent<Rigidbody> ();
				// "Enable/Re-enable" rigidbody by setting 'isKinematic' to false and 'detectCollision' to true
				rb.isKinematic = false;
				rb.detectCollisions = true;

				// Set the parent of all the object to null
				go.transform.parent = null;

				// Add an explosion force to all rigidbody from center of shell
				rb.AddExplosionForce (explosionForce, transform.position, explosionRadius, 0f); // Add force mode?

			}

			// TODO:
			// Create the object that is suppose to be in the center (if one is assigned)
			if (coreGameObjectPrefab != null) {
				Instantiate (coreGameObjectPrefab, transform.position, Quaternion.identity);
			}
			// Destroy this game object
			Destroy(gameObject);

		} 

		else { // POI was hit
			// Play sound when hit (if music & sound not null)
			if (soundAndMusic != null) {
				soundAndMusicScript.POIHit(transform.position);	
			}

			// Shake shell
			if (!isShaking) {
				isShaking = true;
				// Shake time
				currShakeTime = baseShakeTime * shakeTimeMultiplier * (hits - 1);
				if (currShakeTime < baseShakeTime) { // Do as a clamp between 'baseShakeTime' and INFINITY (or maxShakeTime)
					currShakeTime = baseShakeTime;
				}
				// Shake amount
				currShakeAmount = baseShakeAmount * shakeAmountMultiplier * (hits - 1);
				if (currShakeAmount < baseShakeAmount) { // Do as a clamp between 'baseShakeAmount' and INFINITY (or maxShakeAmount)
					currShakeAmount = baseShakeAmount;
				}
				startShakePos = transform.position;
				StartCoroutine ("ShakeCapsule");
			}
		}

	}

	// Coroutine for shaking the capsule when hit but not destroyed
	IEnumerator ShakeCapsule() {

		// Before...
		// Change the scale of the shell parts (to make it appear as cracks in the shell)
		foreach (GameObject go in shellParts) {
			float randScale = Random.Range(-shellShakeChange, shellShakeChange);
			go.transform.localScale += new Vector3(randScale, randScale, randScale);
		}


		float timer = 0f;

		while (timer < currShakeTime) {
			// Update shake
			// A Vector3 of the random offset that should be added to 'shakeStartPos' to find the new position
			Vector3 randOffset = new Vector3(Random.Range(-currShakeAmount, currShakeAmount), Random.Range(-currShakeAmount, currShakeAmount), Random.Range(-currShakeAmount, currShakeAmount));
			transform.position = startShakePos + randOffset;
			// Update timer
			timer += Time.deltaTime;
			yield return Time.deltaTime;
		}

		// Reset the capsules position
		transform.position = startShakePos;

		// Reset 'isShaking'
		isShaking = false;

	}

}
