using UnityEngine;
using System.Collections;

public class CapsuleWallPartScript : MonoBehaviour {

	// Public variables
	public int hitsToDestroy = 1;
	public float minHitPushAmount = 0f;
	public float maxHitPushAmount = 0f;
	public float hitDetachForce = 0f; // ???
	public float detachScalar = 0f;

	// Private variables
	private int hits = 0;

	// Use this for initialization
	void Start () {
	
		// Set to kinematic
		//GetComponent<Rigidbody>().isKinematic = true;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Handles collision when they first happen
	void OnCollisionEnter(Collision collision) {
		
		if (collision.gameObject.tag == "Bullet") {
			// Do something...
			hits++;
			checkDetachFromWall(collision.gameObject.GetComponent<Rigidbody>().velocity);
			// Temporary? (Destroys the bullet)
			Destroy(collision.gameObject);
		}

	}

	// Check if shell part should fly away from wall (with this or next hit?)
	private void checkDetachFromWall(Vector3 bulletVelocity) {
		
		// Wall part is detached from the wall
		if (hits >= hitsToDestroy) {

			// Scale down the chunk a bit (could be explained as the chunk losing some things when hit)
			transform.localScale *= detachScalar;
			Rigidbody rb = GetComponent<Rigidbody>();
			rb.isKinematic = false;
			rb.AddForce(bulletVelocity * hitDetachForce);

			/*
			Rigidbody rb = GetComponent<Rigidbody>();
			rb.isKinematic = false;
			rb.AddForce(bulletVelocity * hitDetachForce);
			*/
		}

		else { // Wall part is pushed back a small bit
			float randPushBack = Random.Range(minHitPushAmount, maxHitPushAmount);
			transform.localPosition += new Vector3(randPushBack, 0f, 0f);

			/*
			// Try to set to not kinematic, if next hit means that it will detach
			if (hits + 1 >= hitsToDestroy) {
				GetComponent<Rigidbody>().isKinematic = false;
				transform.localScale *= detachScalar;
			}
			*/

		}

	}

	/*
	 *  After "dead"
	 *  - Set 'isKinematic' to false
	*/
}
