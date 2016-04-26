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
				/*
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
				*/


				//Debug.Log("Volume: " + thisVolume);
				aspiringVolume = thisVolume / (float)numShellChunks;
				//thisHeight /= (float)numShellChunks;
				//Debug.Log("Aspiring volume: " + aspiringVolume);
				go.GetComponent<ObjectVolumeScript>().SetPreviousHeight(aspiringVolume);



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

	}

	// Split for shell(?)
	public void Split(Vector3 collisionPoint) {
		
		if (!isHit && numOfSplits < maxNumOfSplits) {

			isHit = true;
			// Randomize number of new chunks to be created
			numShellChunks = Random.Range (minShellChunk, maxShellChunk);

			float thisVolume = GetComponent<ObjectVolumeScript>().Volume;
			float randVolumeDiff = 0f;
			float aspiringVolume = 0f;

			// "Available spread"
			//Vector3 outVec = coreTransform.position - collisionPoint;
			//Vector3 maxVec = Vector3.Cross(outVec, transform.up);
			//Debug.Log("'outVec'" + outVec + " | up " + transform.up + " | 'maxVec' " + maxVec);

			for (int i = 0; i < numShellChunks; i++) {


				Vector3 randPos = randPos = new Vector3 (Random.Range (-1, 1), Random.Range (-1, 1), Random.Range (-1, 1));;

				/*
				int numOfIterations = 0;
				// Spread out from center (to control "spread speed")
				bool okPos = false;
				while (!okPos) {
					randPos = new Vector3 (Random.Range (-1, 1), Random.Range (-1, 1), Random.Range (-1, 1));
					Vector3 compVec = randPos - collisionPoint;

					// Draw debug line for vector for where the shell chunk will perhaps launch
					Vector3 startPos = coreTransform.position;
					Vector3 endPos = (coreTransform.position + randPos) * 25f;
					Debug.DrawLine(startPos, endPos, Color.white, Time.deltaTime, false);

					float angle = Mathf.Acos (Vector3.Dot (outVec, compVec) / (outVec.magnitude * compVec.magnitude));
					angle *= Mathf.Rad2Deg;
					Debug.Log ("Angle: " + angle);
					if (angle >= 90) { // 90 should instead be a variable for designers
						okPos = true;

						// Draw a lasting debug line (showing that it worked)
						Debug.DrawLine(startPos, endPos, Color.green, 10f, false);
					} 

					else {
						Debug.Log("Not an ok position");
					}
					numOfIterations++;
				}

				Debug.Log("Ok position found after " + numOfIterations + " iterations.");
				*/


				// Instantiate game object
				GameObject go = Instantiate (shellChunkPrefab, collisionPoint + randPos, transform.rotation) as GameObject; // Create new shell chunk


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

				aspiringVolume = thisVolume / (float)numShellChunks;
				go.GetComponent<ObjectVolumeScript>().SetPreviousHeight(aspiringVolume);



				// Not the same scale for every created object?
				//go.transform.localScale *= (1.0f / Mathf.Pow (numShellChunks, 1.0f / 3.0f)); // Scale shell chunk
				//go.transform.localScale *= chunkScale; // Scale shell chunk

				go.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, collisionPoint, explosionRadius, 0f, ForceMode.Impulse);

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
