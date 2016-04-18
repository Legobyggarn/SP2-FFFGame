using UnityEngine;
using System.Collections;


public class BoxSplitBehaviourScript : MonoBehaviour {

    // Public
    public GameObject shellChunkPrefab;
    public ParticleSystem explo;
    public int minShellChunk;
    public int maxShellChunk;
    public float explosionForce;
    public float minShellChunkScale;
    public float maxShellChunkScale;
	/*
	public float baseVolumeWidth;
	public float baseVolumeHeight;
	public float baseVolumeDepth;
	*/

	public Transform playerTransform;
	public Transform coreTransform;

	public int maxNumOfSplits;

    // Private
    private bool isHit;
    private int numShellChunks;
    private float explosionRadius;

	private float colTimeout;

	private int numOfSplits;

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
			numShellChunks = Random.Range (minShellChunk, maxShellChunk);

			for (int i = 0; i < numShellChunks; i++) {

				// Spread out from center (to control "spread speed")
				Vector3 randPos = new Vector3 (Random.Range (-1f, 1f), Random.Range (-1f, 1f), Random.Range (-1f, 1f));

				// Instantiate new game object
				GameObject go = Instantiate (shellChunkPrefab, transform.position + randPos, transform.rotation) as GameObject; // Create new shell chunk

				// Determine scale
				float chunkScale = Random.Range (minShellChunkScale, maxShellChunkScale);

				// Not the same scale for every created object?
				//go.transform.localScale *= (1.0f / Mathf.Pow (numShellChunks, 1.0f / 3.0f)); // Scale shell chunk
				go.transform.localScale *= chunkScale; // Scale shell chunk

				go.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 0f);

				// Tracking number of splits
				int newValue = numOfSplits + 1;
				go.GetComponent<BoxSplitBehaviourScript>().NumberOfSplits(newValue);

				//go.GetComponent<BoxSplitBehaviourScript>().calcVolume();

			}

			// Instantiates explosion particle effect
			Instantiate (explo, transform.position, Quaternion.identity);

		}

	}

	// Split for shell(?)
	public void Split(Vector3 collisionPoint) {
		
		if (!isHit && numOfSplits < maxNumOfSplits) {

			isHit = true;
			// Randomize number of new chunks to be created
			numShellChunks = Random.Range (minShellChunk, maxShellChunk);

			// "Available spread"
			//Vector3 outVec = coreTransform.position - collisionPoint;
			//Vector3 maxVec = Vector3.Cross(outVec, transform.up);
			//Debug.Log("'outVec'" + outVec + " | up " + transform.up + " | 'maxVec' " + maxVec);

			for (int i = 0; i < numShellChunks; i++) {

				Vector3 randPos = randPos = new Vector3 (Random.Range (-1, 1), Random.Range (-1, 1), Random.Range (-1, 1));;

				/*
				// Spread out from center (to control "spread speed")
				bool okPos = false;
				while (!okPos) {
					randPos = new Vector3 (Random.Range (-1, 1), Random.Range (-1, 1), Random.Range (-1, 1));
					Vector3 compVec = randPos - collisionPoint;
					float angle = Mathf.Acos (Vector3.Dot (outVec, compVec) / (outVec.magnitude * compVec.magnitude));
					angle *= Mathf.Rad2Deg;
					Debug.Log ("Angle: " + angle);
					if (angle >= 90) { // 90 should instead be a variable for designers
						okPos = true;
					} 

					else {
						Debug.Log("Not an ok position");
					}
				}
				*/


				// Instantiate game object
				GameObject go = Instantiate (shellChunkPrefab, collisionPoint + randPos, transform.rotation) as GameObject; // Create new shell chunk

				// Set scale
				float chunkScale = Random.Range (minShellChunkScale, maxShellChunkScale);

				// Not the same scale for every created object?
				//go.transform.localScale *= (1.0f / Mathf.Pow (numShellChunks, 1.0f / 3.0f)); // Scale shell chunk
				go.transform.localScale *= chunkScale; // Scale shell chunk

				go.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, collisionPoint, explosionRadius, 0f);

				// Tracking number of splits
				int newValue = numOfSplits + 1;
				go.GetComponent<BoxSplitBehaviourScript>().NumberOfSplits(newValue);

				//go.GetComponent<BoxSplitBehaviourScript>().calcVolume();

			}

			// Instantiates explosion particle effect
			Instantiate (explo, collisionPoint, Quaternion.identity);

		}

	}


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
