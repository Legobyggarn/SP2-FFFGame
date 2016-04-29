using UnityEngine;
using System.Collections;

public class ShellChunkBounce : MonoBehaviour {

	// Public variables
	public float bounceForce;

	// Private variables
	//...

	void OnCollisionExit(Collision collision) {
		
		// Velocity vector when leaving
		Vector3 velocity = GetComponent<Rigidbody>().velocity;

		// Add force
		GetComponent<Rigidbody> ().AddForce (velocity.normalized * bounceForce, ForceMode.Impulse);

	}

}
