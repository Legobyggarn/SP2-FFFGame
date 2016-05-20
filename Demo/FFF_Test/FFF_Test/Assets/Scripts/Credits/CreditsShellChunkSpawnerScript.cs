using UnityEngine;
using System.Collections;

public class CreditsShellChunkSpawnerScript : MonoBehaviour {

	// Public variables
	public GameObject shellChunkPrefab;
	public float spawnInterval;
	public float minRadius;
	public float maxRadius;
	public float pushForce;

	// Private variables
	//...

	// Use this for initialization
	void Start () {
		StartCoroutine("spawnShellChunk");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator spawnShellChunk() {

		while (true) {
			// Position (Psuedo code)
			// 1. Randomize position.
			// 2. Measure distance.
			// 3. If distance is shorter than shortest go to step 4. If further than furthest go to step 5.
			// 4. Make distance 1 and then multiply by shortest.
			// 5. Make distance 1 and then multiply by furthest.

			// Position
			Vector3 randPos = new Vector3 (Random.Range (-maxRadius, maxRadius), Random.Range (-maxRadius, maxRadius), Random.Range (-maxRadius, maxRadius));
			float distance = Vector3.Distance (transform.position, randPos);
			if (distance < minRadius) {
				randPos = randPos.normalized;
				randPos *= minRadius;
			} else if (distance > maxRadius) {
				randPos = randPos.normalized;
				randPos *= maxRadius;
			}

			GameObject go = Instantiate(shellChunkPrefab, randPos, Quaternion.identity) as GameObject;
			go.transform.parent = transform;
			// Apply force to new shell chunk
			Vector3 randDir = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
			randDir = randDir.normalized;
			go.GetComponent<Rigidbody>().AddForce(randDir * pushForce, ForceMode.Impulse);

			yield return new WaitForSeconds(spawnInterval);

		}

	}

}
