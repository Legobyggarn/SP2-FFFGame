using UnityEngine;
using System.Collections;

public class TestShootShellChunkScript : MonoBehaviour {

	// Public
	public Transform testSpawn;
	public GameObject testShellChunk;

	// Private
	//...

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown(KeyCode.T)) {
			GameObject go = Instantiate(testShellChunk, testSpawn.position, Quaternion.identity) as GameObject;
			go.GetComponent<Rigidbody>().AddForce (transform.forward * 1000f);
		}

	}
}
