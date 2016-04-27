using UnityEngine;
using System.Collections;


public class BoxSplitBehaviourScript : MonoBehaviour {

    // Public
    public GameObject shellChunkPrefab;
    public ParticleSystem explo;
    public int minShellChunk;
    public int maxShellChunk;
    public float explosionForce;

	// Remove? (not used)
    public float minShellChunkScale;
    public float maxShellChunkScale;

	public Transform playerTransform; // Remove?
	public Transform coreTransform = null;

	public int maxNumOfSplits; // Remove?

    // Private
    private bool isHit;
    private int numShellChunks;
    private float explosionRadius;

	private float colTimeout; // Remove?

	private int numOfSplits; // Remove?

    void Start () {

        isHit = false;

    }

    void Update()
    {
		// Test of Split function
		/*
        if (Input.GetKeyDown(KeyCode.E))
        {
			Split();
        }
        */

        
		// Fix to make sure OnCollisionEnter only is executed once
        if (isHit)
        {
            isHit = false;
        }
        

    }

	public void Split() {

		if (!isHit && numOfSplits < maxNumOfSplits) {

			isHit = true;
			// Randomize number of new chunks to be created
			numShellChunks = Random.Range(minShellChunk, maxShellChunk);

			// Get the volume of this shell chunk
			float thisVolume = GetComponent<ObjectVolumeScript>().Volume;
			float aspiringVolume = 0f; // Will hold the volume that newly created shell chunks will aspire to

			// Calculate a vector from core to position, if a core transform is set
			Vector3 outVec = new Vector3();
			if (coreTransform != null) {
				outVec = transform.position - coreTransform.position;
			}

			for (int i = 0; i < numShellChunks; i++) {

				if (coreTransform != null) {
					// Instantiate game object
					GameObject go = Instantiate(shellChunkPrefab, transform.position + outVec.normalized, transform.rotation) as GameObject; // Create new shell chunk

					// Set aspiring volume
					aspiringVolume = thisVolume / (float)numShellChunks;
					go.GetComponent<ObjectVolumeScript>().SetAspiringVolume(aspiringVolume);

					// Add explosion force to the newly created shell chunk
					//go.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 0f, ForceMode.Impulse);

					// Add force to the newly created shell chunk
					go.GetComponent<Rigidbody>().AddForce(outVec * explosionForce, ForceMode.Impulse);
				} 

				else {
					Vector3 randPos = new Vector3 (Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
					// Instantiate game object
					GameObject go = Instantiate(shellChunkPrefab, transform.position + randPos, transform.rotation) as GameObject; // Create new shell chunk

					// Set aspiring volume
					aspiringVolume = thisVolume / (float)numShellChunks;
					go.GetComponent<ObjectVolumeScript>().SetAspiringVolume(aspiringVolume);

					// Add explosion force to the newly created shell chunk
					go.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 0f, ForceMode.Impulse);
				}

			}

			// Instantiates explosion particle effect (that will be destroyed after x seconds)
			ParticleSystem explosion = Instantiate (explo, transform.position, Quaternion.identity) as ParticleSystem;
			Destroy (explosion.gameObject, 10f); // Destroy the explosion after 10 seconds.

		}




		/*
		if (!isHit && numOfSplits < maxNumOfSplits) {
		

			isHit = true;
			// Randomize number of new chunks to be created
			numShellChunks = Random.Range (minShellChunk, maxShellChunk);

			float thisVolume = GetComponent<ObjectVolumeScript>().Volume;
			float randVolumeDiff = 0f;
			float aspiringVolume = 0f;

			for (int i = 0; i < numShellChunks; i++) {

				// Spread out from center (to control "spread speed")
				Vector3 randPos = new Vector3 (Random.Range (-1f, 1f), Random.Range (-1f, 1f), Random.Range (-1f, 1f));
				// TODO: Add something to control the spread better (other shapes than a cube, more restrictive).

				// Instantiate new game object
				GameObject go = Instantiate (shellChunkPrefab, transform.position + randPos, transform.rotation) as GameObject; // Create new shell chunk

				// Determine scale
				//float chunkScale = Random.Range (minShellChunkScale, maxShellChunkScale);

				// New algorithm for determinig scale (based on size of previous chunk(this chunk))
				///
				if (i % 2 == 0 || i + 1 <= numShellChunks) {
					// Slumpa ett tal mellan min och max
					//randVolumeDiff = Random.Range (0f, 1f);
					// Öka volymen av det första objektet med talet
					//aspiringVolume = thisVolume + randVolumeDiff;
					// Sätt volymen av det nya objektet i dess 'ObjectVolumeScript'
					go.GetComponent<ObjectVolumeScript>().SetAspiringVolume(aspiringVolume);
				} 

				else if (i % 2 != 0) {
					// Minska det andra objektets volym med talet
					//aspiringVolume = thisVolume - randVolumeDiff;
					// Sätt volymen av det nya objektet i dess 'ObjectVolumeScript'
					go.GetComponent<ObjectVolumeScript>().SetAspiringVolume(aspiringVolume);
				}
				///

				aspiringVolume = thisVolume / (float)numShellChunks;
				go.GetComponent<ObjectVolumeScript>().SetAspiringVolume(aspiringVolume);



				// Not the same scale for every created object?
				//go.transform.localScale *= (1.0f / Mathf.Pow (numShellChunks, 1.0f / 3.0f)); // Scale shell chunk
				//go.transform.localScale *= chunkScale; // Scale shell chunk

				go.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 0f);

				// Tracking number of splits
				int newValue = numOfSplits + 1;
				go.GetComponent<BoxSplitBehaviourScript>().NumberOfSplits(newValue);

				//go.GetComponent<BoxSplitBehaviourScript>().calcVolume();

			}

			// Instantiates explosion particle effect
			Instantiate (explo, transform.position, Quaternion.identity);

		}
		*/

	}

	// Split for shell(?)
	public void Split(Vector3 collisionPoint) {

		Debug.Log(coreTransform);

		if (!isHit && numOfSplits < maxNumOfSplits) {

			isHit = true;
			// Randomize number of new chunks to be created
			numShellChunks = Random.Range (minShellChunk, maxShellChunk);

			// Get the volume of this shell chunk
			float thisVolume = GetComponent<ObjectVolumeScript>().Volume;
			//float randVolumeDiff = 0f;
			float aspiringVolume = 0f; // Will hold the volume that newly created shell chunks will aspire to

			// Calculate a vector from core to collision point, if a core transform is set
			Vector3 outVec = new Vector3();
			if (coreTransform != null) {
				outVec = collisionPoint - coreTransform.position;
			}

			/*
			if (coreTransform != null) {
				//outVec = coreTransform.position - collisionPoint;
				outVec = collisionPoint - coreTransform.position;
				Vector3 startPos = coreTransform.position;
				Vector3 endPos = (outVec);
				//Debug.DrawLine(startPos, endPos, Color.yellow, 10f, false);
				//maxVec = Vector3.Cross (outVec, transform.up);
				//Debug.Log("'outVec'" + outVec + " | up " + transform.up + " | 'maxVec' " + maxVec);
			}
			*/

			for (int i = 0; i < numShellChunks; i++) {

				if (coreTransform != null) {

					// Instantiate game object
					GameObject go = Instantiate(shellChunkPrefab, collisionPoint + outVec.normalized, transform.rotation) as GameObject; // Create new shell chunk

					// Set aspiring volume
					aspiringVolume = thisVolume / (float)numShellChunks;
					go.GetComponent<ObjectVolumeScript>().SetAspiringVolume(aspiringVolume);

					// Add explosion force to the newly created shell chunk
					go.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, collisionPoint, explosionRadius, 0f, ForceMode.Impulse);
				} 

				else {

					Vector3 randPos = new Vector3(Random.Range (-1, 1), Random.Range (-1, 1), Random.Range (-1, 1));
					// Instantiate game object
					GameObject go = Instantiate(shellChunkPrefab, collisionPoint + randPos, transform.rotation) as GameObject; // Create new shell chunk

					// Set aspiring volume
					aspiringVolume = thisVolume / (float)numShellChunks;
					go.GetComponent<ObjectVolumeScript>().SetAspiringVolume(aspiringVolume);

					// Add explosion force to the newly created shell chunk
					go.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, collisionPoint, explosionRadius, 0f, ForceMode.Impulse);
				}





					/*
				if (coreTransform != null) {
					int numOfIterations = 0;
					// Spread out from center (to control "spread speed")
					bool okPos = false;

					///
					while (!okPos) {
						Debug.Log("Starting...");
						//randPos = new Vector3 (Random.Range (-1, 1), Random.Range (-1, 1), Random.Range (-1, 1));
						Vector3 compVec = randPos - collisionPoint;

						Debug.Log("Drawing line...");
						// Draw debug line for vector for where the shell chunk will perhaps launch
						Vector3 startPos = coreTransform.position;
						Vector3 endPos = (compVec) * 25f;
						Debug.DrawLine(startPos, endPos, Color.magenta, 10f, false);

						startPos = coreTransform.position;
						endPos = (collisionPoint - coreTransform.position);
						Debug.DrawLine(startPos, endPos, Color.blue, 10f, false);


						Debug.Log("Calculating angle...");
						float angle = Mathf.Acos (Vector3.Dot (outVec, compVec) / (outVec.magnitude * compVec.magnitude));
						angle *= Mathf.Rad2Deg;
						Debug.Log ("Angle: " + angle);

						// TEMPORARY!
						//okPos = true;


						Debug.Log("If-statement...");
						if (angle <= 90) { // 90 should instead be a variable for designers
							okPos = true;

							// Draw a lasting debug line (showing that it worked)
							Debug.DrawLine(startPos, endPos, Color.green, 10f, false);
						} 

						else {
							Debug.Log("Not an ok position");
							// Draw a lasting debug line (showing that it didn't work)
							Debug.DrawLine(startPos, endPos, Color.red, 10f, false);
						}
						numOfIterations++;

					}
					///


					//Debug.Log("Ok position found after " + numOfIterations + " iterations.");

				}
				*/



				/*
				// Instantiate game object
				GameObject go = Instantiate (shellChunkPrefab, collisionPoint + randPos, transform.rotation) as GameObject; // Create new shell chunk
				*/



				// Set scale
				//float chunkScale = Random.Range (minShellChunkScale, maxShellChunkScale);

				// New algorithm for determinig scale (based on size of previous chunk(this chunk))
				/*
				if (i % 2 == 0 || i + 1 <= numShellChunks) {
					// Slumpa ett tal mellan min och max
					randVolumeDiff = Random.Range (0f, 1f);
					// Öka volymen av det första objektet med talet
					aspiringVolume = thisVolume + randVolumeDiff;
					// Sätt volymen av det nya objektet i dess 'ObjectVolumeScript'
					go.GetComponent<ObjectVolumeScript>().SetAspiringVolume(aspiringVolume);
				} 

				else if (i % 2 != 0) {
					// Minska det andra objektets volym med talet
					aspiringVolume = thisVolume - randVolumeDiff;
					// Sätt volymen av det nya objektet i dess 'ObjectVolumeScript'
					go.GetComponent<ObjectVolumeScript>().SetAspiringVolume(aspiringVolume);
				}
				*/


				/*
				aspiringVolume = thisVolume / (float)numShellChunks;
				go.GetComponent<ObjectVolumeScript>().SetAspiringVolume(aspiringVolume);
				*/



				// Not the same scale for every created object?
				//go.transform.localScale *= (1.0f / Mathf.Pow (numShellChunks, 1.0f / 3.0f)); // Scale shell chunk
				//go.transform.localScale *= chunkScale; // Scale shell chunk

				/*
				go.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, collisionPoint, explosionRadius, 0f, ForceMode.Impulse);
				*/


				/*
				// Tracking number of splits
				// TODO: Not worry about number of splits, instead just go after volume(?)
				int newValue = numOfSplits + 1;
				go.GetComponent<BoxSplitBehaviourScript>().NumberOfSplits(newValue);
				*/


				//go.GetComponent<BoxSplitBehaviourScript>().calcVolume();

			}

			// Instantiates explosion particle effect (that will be destroyed after x seconds)
			ParticleSystem explosion = Instantiate (explo, transform.position, Quaternion.identity) as ParticleSystem;
			Destroy (explosion.gameObject, 10f); // Destroy the explosion after 10 seconds.

		}

	}

	// Not used anymore. (Remove or start using again?)
	public void NumberOfSplits(int numSplits) {
		
		numOfSplits = numSplits;

		//Debug.Log ("Splits of [" + gameObject.name + "]: " + numOfSplits);

		/*
		if (numOfSplits >= maxNumOfSplits) {
			GameObject go = Instantiate (explo, transform.position, Quaternion.identity) as GameObject;
			go.transform.localScale *= transform.localScale.x;
			Destroy(gameObject);
		}
		*/

	}

}
