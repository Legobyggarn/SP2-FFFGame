using UnityEngine;
using System.Collections;

public class LoadingScreenDotsScript : MonoBehaviour {

	// TODO: Add a list to be able to distinguish size of dots. Make "bigger" sounds create a bigger dot and vice versa. (?)

	// Public variables
	public GameObject dotPrefab; // Make a list to be able to choose from a couple of different "dots" (ranging in size and color).
	public int[] dotTimeStamps; // Index 0 should be 0 as to indicate it being the start. Higher index should mean higher number, more progress in song, otherwise weird behaviour can occur.

	// Private variables
	//private float timer = 0f;
	//private int dotCounter = 0;

	// Use this for initialization
	void Start () {
		StartCoroutine("spawnDots");
	}
	
	// Update is called once per frame
	void Update () {
	
		// Update 'timer'
		//timer += Time.deltaTime;

		//...

	}

	IEnumerator spawnDots() {

		// PROBLEM: Solution might get off tempo because of calculations times.

		for (int i = 1; i < dotTimeStamps.Length; i++) {

			// Debug
			Debug.Log("[LoadingScreenDots]: Next dot spawn in " + (dotTimeStamps [i] - dotTimeStamps [i - 1]) + " seconds.");

			float x = Random.Range (-10f, 10f);
			float y = Random.Range (-10f, 10f);

			yield return new WaitForSeconds(dotTimeStamps[i] - dotTimeStamps[i-1]);
			// Spawn dot (at random position) (six for in front, behind, sides, over, under)
			Instantiate (dotPrefab, new Vector3(x, y, 25f), Quaternion.identity); // In front
			Instantiate (dotPrefab, new Vector3(-x, y, -25f), Quaternion.identity); // Behind

			Instantiate (dotPrefab, new Vector3(25f, y, x), Quaternion.identity); // Right
			Instantiate (dotPrefab, new Vector3(-25f, y, -x), Quaternion.identity); // Left

			Instantiate (dotPrefab, new Vector3(-x, 25f, -y), Quaternion.identity); // Over
			Instantiate (dotPrefab, new Vector3(x, -25f, y), Quaternion.identity); // Under

			// Debug
			Debug.Log("[LoadingScreenDots]: Dot spawned");
		}

	}

}
