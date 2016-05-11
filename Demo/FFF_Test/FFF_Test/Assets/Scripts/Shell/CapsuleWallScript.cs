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

		}

		// Get the sound and music game object
		soundAndMusicScript = GameObject.Find("Sound_And_Music_Var").GetComponent<SondAndMusic_Var>();

	}

	// Handles collision when they first happen
	void OnCollisionEnter(Collision collision) {

		if (collision.gameObject.tag == "Bullet") {
			breakWall(collision.gameObject.transform.position);

			// Temporary? Destroy the bullet object
			Destroy (collision.gameObject);
		} 

	}

	/*
	// Handles objects entering the trigger
	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "Bullet") {
			breakWall(collider.gameObject.transform.position);

			// Temporary? Destroy the bullet object
			Destroy(collider.gameObject);
		}
	}
	*/

	private void breakWall(Vector3 collisionPos) {
		
		// Get a list of all wall parts in within 'explosionRadius'
		List<GameObject> wallPartsInRadius = findWallPartsInRadius(collisionPos, explosionRadius);

		foreach (GameObject go in wallPartsInRadius) {
			float distance = Vector3.Distance(collisionPos, go.transform.position); // Distance between collision position and current wall part
			float distPerc = 1f - (distance / explosionRadius); // [Clamp '(distance / explosionRadius)' between 0 and 1 first?]
			distPerc = Mathf.Clamp(distPerc, 0f, 1f); // Clamp between 0 and 1(?)
			// BUG: Root node move aswell? (doesn't occur anymore?)
			go.transform.localPosition += new Vector3(0f, 0f, hitPushAmount * distPerc); // Move the game object 

			// Destroy wall
			Rigidbody rb = go.GetComponent<Rigidbody>();
			if (rb != null) {
				rb.isKinematic = false;
				//rb.detectCollisions = true;
				rb.AddExplosionForce(explosionForce, collisionPos, explosionRadius, 0f, ForceMode.Impulse); // Add force mode?
				//rb.AddForce(dir * 100f, ForceMode.Impulse);
			}

			go.transform.parent = null; // Set parent to null
			wallParts.Remove(go); // Remove from 'wallParts'

			// Message music and sound script that a part of the wall has been destroyed
			//...

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
