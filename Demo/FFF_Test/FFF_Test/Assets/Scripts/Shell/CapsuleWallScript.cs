using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CapsuleWallScript : MonoBehaviour {

	// TODO: Instead of number of hits, have a health/hit variable for each wall part. Then if a object is part of a collision and has no health/hit left then it will be destroyed?
	//       Or just have it to need one hit to kill the wall.

	// Public variables
	public int hitsToDestroy = 1;
	public float hitPushAmount = 0f;
	public int explosionForce = 0;
	public int explosionRadius = 0;

	// Private variables
	private int hits = 0;
	private List<GameObject> wallParts = new List<GameObject>();

	// TODO:
	// Idea for damaging of wall. Move hit parts a small amount in the direction of the bullets direction.

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

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Handles collision when they first happen
	void OnCollisionEnter(Collision collision) {

		if (collision.gameObject.tag == "Bullet") {
			hits++;
			checkWallBreak(collision.gameObject.transform.position);
			// Temporary? Destroy the bullet object
			Destroy(collision.gameObject);
		}

	}

	private void checkWallBreak(Vector3 collisionPos) {

		// Destroy wall
		if (hits >= hitsToDestroy) {
			// Find all colliders in a sphere radius around collision position
			Collider[] colliders = Physics.OverlapSphere (collisionPos, explosionRadius);
			foreach (Collider col in colliders) {
				Rigidbody rb = col.gameObject.GetComponent<Rigidbody> ();
				if (rb != null) {
					rb.isKinematic = false;
					rb.detectCollisions = true;
					rb.AddExplosionForce (explosionForce, collisionPos, explosionRadius, 0f); // Add force mode?
				}
			}
		}

		// Push back wall parts
		else {
			Debug.Log("Push back");
			Collider[] colliders = Physics.OverlapSphere (collisionPos, explosionRadius);

			// Loop through all child wall parts and pick out the once with a distance shorter than 'explosionRadius'
			List<GameObject> objectsInRadius = new List<GameObject>();
			foreach (GameObject go in wallParts) {
				float distance = Vector3.Distance(collisionPos, go.transform.position);
				if (distance <= explosionRadius) {
					objectsInRadius.Add(go);
				}
			}

			foreach (GameObject go in objectsInRadius) {
				// Measure distance between collision point and collider...
				// Calculate (1f - (measuredDist / explosionRadius)) and get the percentage of the way from the collision point...
				// Move the wall part (moveAmount * previousResult)...
				float distance = Vector3.Distance(collisionPos, go.transform.position);
				float distPerc = 1f - (distance / explosionRadius); // Clamp '(distance / explosionRadius)' between 0 and 1 first?
				distPerc = Mathf.Clamp(distPerc, 0f, 1f); // Clamp between 0 and 1(?)
				// BUG: Root node move aswell
				go.transform.localPosition += new Vector3(0f, 0f, hitPushAmount * distPerc); // Move the game object 
			}
		}

	}

}
