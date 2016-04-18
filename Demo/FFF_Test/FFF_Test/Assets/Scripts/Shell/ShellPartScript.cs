using UnityEngine;
using System.Collections;

public class ShellPartScript : MonoBehaviour {

	// Public
	public GameObject shellHandler;

	// Private
	private ShellHandlerScript shellHandlerScript;

	// Use this for initialization
	void Start () {
	
		shellHandlerScript = shellHandler.GetComponent<ShellHandlerScript>();

		// Calculate volume
		transform.GetComponent<ObjectVolumeScript>().calcVolume(transform.GetComponent<MeshCollider>(), transform.GetComponent<MeshRenderer>());

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/*
	void OnCollisionEnter(Collision collision) {

		if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "ShellChunk") {
			shellHandlerScript.shellCollision(gameObject);
			// Destroy bullet
			//Debug.Log("Destroy bullet!");
			//Destroy (collision.gameObject);
		}

	}
	*/

	public void HideShell() {
		shellHandlerScript.shellCollision(gameObject);
	}

	//...

}
