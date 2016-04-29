using UnityEngine;
using System.Collections;


public class BoxSplitBehaviourScript : MonoBehaviour {

    // Public
    public GameObject shellChunkPrefab;
    public ParticleSystem explo;
    public int minShellChunk;
    public int maxShellChunk;
    public float explosionForce;
	public float explosionRadius;

	// Remove? (not used)
    //public float minShellChunkScale;
    //public float maxShellChunkScale;

	//public Transform playerTransform; // Remove?
	public Transform coreTransform = null;

	//public int maxNumOfSplits; // Remove?

    // Private
    private bool isHit;
    private int numShellChunks;

	//private float colTimeout; // Remove?

	//private int numOfSplits; // Remove?

    void Start () {

        isHit = false;

    }

    void Update()
    {
	   
		// Fix to make sure OnCollisionEnter only is executed once
        if (isHit)
        {
            isHit = false;
        }
        

    }

	// Split for shell(?)
	public void Split(Vector3 collisionPoint) {

		if (!isHit) { // Removed: (&& numOfSplits < maxNumOfSplits)

			isHit = true;
			// Randomize number of new chunks to be created
			numShellChunks = Random.Range(minShellChunk, maxShellChunk);

			// Get the volume of this shell chunk
			float thisVolume = GetComponent<ObjectVolumeScript>().Volume;
			float aspiringVolume = thisVolume / (float)numShellChunks; // Will hold the volume that newly created shell chunks will aspire to

			// Calculate a vector from core to collision point, if a core transform is set
			Vector3 outVec = new Vector3();
			if (coreTransform != null) {
				outVec = collisionPoint - coreTransform.position;
			}

			for (int i = 0; i < numShellChunks; i++) {

				if (coreTransform != null) { // Collision with shell
					// Instantiate game object
					GameObject go = Instantiate(shellChunkPrefab, collisionPoint + outVec.normalized, transform.rotation) as GameObject; // Create new shell chunk

					// Set aspiring volume
					go.GetComponent<ObjectVolumeScript>().SetAspiringVolume(aspiringVolume);

					// Add explosion force to the newly created shell chunk
					go.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, collisionPoint, explosionRadius, 0f, ForceMode.Impulse);
				} 

				else {
					Vector3 randPos = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
					// Instantiate game object
					GameObject go = Instantiate(shellChunkPrefab, collisionPoint + randPos, transform.rotation) as GameObject; // Create new shell chunk

					// Set aspiring volume
					go.GetComponent<ObjectVolumeScript>().SetAspiringVolume(aspiringVolume);

					// Add explosion force to the newly created shell chunk
					go.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, collisionPoint, explosionRadius, 0f, ForceMode.Impulse);
				}

			}

			// Instantiates explosion particle effect (that will be destroyed after x seconds)
			ParticleSystem explosion = Instantiate (explo, transform.position, Quaternion.identity) as ParticleSystem;
			Destroy (explosion.gameObject, 10f); // Destroy the explosion after 10 seconds.

		}

	}

	/*
	// Not used anymore. (Remove or start using again?)
	public void NumberOfSplits(int numSplits) {
		
		numOfSplits = numSplits;

		//Debug.Log ("Splits of [" + gameObject.name + "]: " + numOfSplits);

		///
		if (numOfSplits >= maxNumOfSplits) {
			GameObject go = Instantiate (explo, transform.position, Quaternion.identity) as GameObject;
			go.transform.localScale *= transform.localScale.x;
			Destroy(gameObject);
		}
		///

	}
	*/

}
