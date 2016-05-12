using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CapsuleWallScript : MonoBehaviour {
	
	// Public variables
	public float hitPushAmount = 0f;
	public float explosionForce = 0f;
	public float explosionRadius = 0f;

	// Private variables
	private List<GameObject> wallParts = new List<GameObject>();
	private SondAndMusic_Var soundAndMusicScript;
	private bool hitThisFrame = false;

	// Use this for initialization
	void Start () {
	
		// Get all shell parts (use a public GameObject variable to identify the child/model?)
		for (int i = 0; i < transform.GetChild(0).childCount; i++) {
			
			// Settings for the rigidbody of child
			// Get the rigidbody of the child of index i
			Rigidbody rb = transform.GetChild(0).GetChild(i).gameObject.GetComponent<Rigidbody>();
			// "Disable" rigidbody by setting 'isKinematic' to true and 'detectCollision' to false
			rb.isKinematic = true;
			//rb.detectCollisions = false;

			// Add new rigidbody to 'wallParts'
			wallParts.Add(transform.GetChild(0).GetChild(i).gameObject);

			// Ignore collisions with all objects with the "Player" tag (should only be player, unless there is multiplayer)
			/*
			GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
			foreach (GameObject player in players) {
				Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());
			}
			*/

		}

		// Get the sound and music game object
		soundAndMusicScript = GameObject.Find("Sound_And_Music_Var").GetComponent<SondAndMusic_Var>();

	}

	// Update is called once per frame
	void Update () {
		if (hitThisFrame) {
			hitThisFrame = false;
		}
	}

	// Handles collision of children (child send info here)
	public void OnCollisionEnterChild(Collision collision) {

		if (!hitThisFrame) {
			hitThisFrame = true;

			if (collision.gameObject.tag == "Bullet") {
				breakWall (collision);

				// Temporary? Destroy the bullet object
				Destroy (collision.gameObject);
			}
		}

	}

	private void breakWall(Collision collision) {
		
		// Get a list of all wall parts in within 'explosionRadius'
		List<GameObject> wallPartsInRadius = findWallPartsInRadius(collision.transform.position, explosionRadius);

		Vector3 localPoint = transform.InverseTransformPoint(collision.transform.position);
		Vector3 localDir = localPoint.normalized;

		Vector3 point = collision.transform.position;
		float fwdDot = Vector3.Dot(localDir, Vector3.forward);
		Debug.Log ("fwdDot: " + fwdDot);

		foreach (GameObject go in wallPartsInRadius) {

			/*
			float distance = Vector3.Distance(collision.transform.position, go.transform.position); // Distance between collision position and current wall part
			float distPerc = 1f - (distance / explosionRadius); // [Clamp '(distance / explosionRadius)' between 0 and 1 first?]
			distPerc = Mathf.Clamp(distPerc, 0f, 1f); // Clamp between 0 and 1(?)
			//go.transform.localPosition -= new Vector3(0f, 0f, hitPushAmount * distPerc); // Move the game object
			*/

			Vector3 explosionPos = collision.transform.position;

			if (fwdDot >= 0f) { // Infront
				go.transform.position -= transform.forward * (hitPushAmount); // Move the game object
				explosionPos += transform.forward * 1f;
			} 

			else { // Behind
				go.transform.position += transform.forward * (hitPushAmount); // Move the game object
				explosionPos -= transform.forward * 1f;
			}

			// Destroy wall
			Rigidbody rb = go.GetComponent<Rigidbody>();
			if (rb != null) {
				rb.isKinematic = false;
				//rb.detectCollisions = true;
				rb.AddExplosionForce(explosionForce, explosionPos, explosionRadius, 0f, ForceMode.Impulse);
			}

			// "Free" the wall part
			go.transform.parent = null; // Set parent to null
			wallParts.Remove(go); // Remove from 'wallParts'


			// Notify the music and sound script that a part of the wall has been destroyed
			if (soundAndMusicScript != null) {
				soundAndMusicScript.partOfWallDestroyed ();
			}

		}

	}

	// Find all objects in 'wallParts' (all wall parts of this wall) withing a given radius
	private List<GameObject> findWallPartsInRadius(Vector3 center, float radius) {
		List<GameObject> objectsInRadius = new List<GameObject>();
		foreach (GameObject go in wallParts) {
			float distance = Vector3.Distance(center, go.transform.position);
			if (distance <= radius) {
				objectsInRadius.Add(go);
			}
		}
		return objectsInRadius;
	}

}
