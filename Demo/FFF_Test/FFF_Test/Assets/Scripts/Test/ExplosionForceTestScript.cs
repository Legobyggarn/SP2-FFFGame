using UnityEngine;
using System.Collections;

public class ExplosionForceTestScript : MonoBehaviour {

	// Public
	public GameObject simpleCubeTestPrefab;
	public int numSimpleCubes;
	public float explosionForce;
	public float explosionRadius;

	public bool useOwnExplosion;

	// Use this for initialization
	void Start () {
	
		// Test for randomizing the positions of the children (cubes to be pushed away)
		//...

	}
	
	// Update is called once per frame
	void Update () {
	
		//Debug.Log ("Kill em all");

		// Create and scatter
		if (Input.GetKeyDown(KeyCode.K)) {

			for (int i = 0; i < numSimpleCubes; i++) {
				Vector3 randPos = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
				GameObject go = Instantiate (simpleCubeTestPrefab, transform.position + randPos, Quaternion.identity) as GameObject;
				go.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 0f);

				// Set parent
				go.transform.parent = transform;
			}

			/*
			// Explode...
			// (Own explosion: Get all colliders/rigidbodies inside of explosionTrigger)
			for (int i = 0; i < transform.childCount; i++) {
				// Transform and rigidbody of current child (not apart of the "real" code)
				Transform child = transform.GetChild (i); // Current child object transform
				Rigidbody childRb = child.GetComponent<Rigidbody>(); // Current child rigidbody

				// Find a valid position to put/create the shell chunk in.
				// Early version (Random position)
				//Debug.Log ("Random position");
				Vector3 randPos = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
				child.transform.position = transform.position + randPos;
				Debug.Log ("Random position: " + randPos);

				// Scatter shell chunks by adding explosion force
				// Unity explosion

				if (!useOwnExplosion) {
					//...
					//childRb.angularDrag = 1.0f;
					childRb.AddExplosionForce(explosionForce, transform.position, explosionRadius, 0.0f);
				}

				// Own explosion "algorithm"
				else {
					Vector3 vec = child.transform.position - transform.position;
					vec = vec.normalized;
					childRb.AddForce(vec * explosionForce);
				}
			}
			*/
		}

	}
}
