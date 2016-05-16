using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShellHandlerScript : MonoBehaviour {

	// TODO: Fix minSizeThreshold use

	// Public
	public float minSizeThreshold;
	public float maxSizeThreshold;

	public float maxDistance;

	public bool useDistance;
	public bool distanceOverVolume;

	public GameObject notifyNumShellChunks;
	private memoryDistance memoryDistanceScript;
	//public bool fillUpVolume;

	// Private
	private List<GameObject> availSpots;

	// Use this for initialization
	void Start () {

		availSpots = new List<GameObject>();

		memoryDistanceScript = notifyNumShellChunks.gameObject.GetComponent<memoryDistance>();

	}

	// Find collision with core here. (Temp?)
	void OnCollisionEnter(Collision collision) {
		// Find collision with shell and call 'coreCollision'
		if (collision.gameObject.tag == "ShellChunk") {
			Debug.Log("Core collision");
			if (!collision.gameObject.GetComponent<ShellDamageScript>().GracePeriodActive && collision.gameObject.GetComponent<ObjectVolumeScript>().Active) {
				if (useDistance) {
					coreCollisionDistance(collision.gameObject);
				} 
				else {
					coreCollision(collision.gameObject);
				}

				// Tell the shell chunk to merge into the core and destroy itself (instead of destroying the shell chunk here)
				collision.gameObject.GetComponent<ShellChunkCoreMergeScript>().MergeWithCore(transform.position);

			}

			else {
				// Add force away from core
				Vector3 currVelocity = collision.gameObject.GetComponent<Rigidbody>().velocity;
				collision.gameObject.GetComponent<Rigidbody>().AddForce(-currVelocity * 100f, ForceMode.Impulse);

			}

		}

	}

	// Notify that a collision with the shell has occured (parameter should contain the collided shell piece)
	public void shellCollision(GameObject go) {

		Debug.Log("In ShellCollision");

		go.GetComponent<MeshCollider>().enabled = false;
		go.GetComponent<MeshRenderer>().enabled = false;

		availSpots.Add(go);

		// Tell script controlling orbit to decrease number of shell parts left
		memoryDistanceScript.decrementNumChildren();

	}

	// Notify that the core has been hit (parameter should contain the object that collided with the core)
	public void coreCollision(GameObject go) {
		
		float hitGOVolume = go.GetComponent<ObjectVolumeScript>().Volume;

		if (availSpots.Count > 0) {
			List<GameObject> toDestroy = new List<GameObject>();

			bool found = false;
			int i = 0;
			foreach (GameObject game_object in availSpots) {
				// The volumen of the current game object from 'availSpots'
				float availGOVolume = game_object.GetComponent<ObjectVolumeScript>().Volume;
				// If the size is roughly the same.
				if (hitGOVolume - minSizeThreshold <= availGOVolume && availGOVolume <= hitGOVolume + maxSizeThreshold) {
					//Debug.Log("Reattached");
					game_object.GetComponent<MeshCollider>().enabled = true;
					game_object.GetComponent<MeshRenderer>().enabled = true;

					// Tell script controlling orbit to increase number of shell parts left
					memoryDistanceScript.incrementNumChildren();

					toDestroy.Add(game_object); // Add object to destroy later
					found = true;
					break;
				}
				i++;
			}

			if (!found) {
				float filledVolume = 0f;
				i = 0;
				foreach (GameObject game_object in availSpots) {
					// The volumen of the current game object from 'availSpots'
					float availGOVolume = game_object.GetComponent<ObjectVolumeScript>().Volume;

					// If there is room for the current piece
					if (availGOVolume < (hitGOVolume - filledVolume)) {
						filledVolume += availGOVolume;
						// Reactivate mesh collider and mesh renderer
						game_object.GetComponent<MeshCollider>().enabled = true;
						game_object.GetComponent<MeshRenderer>().enabled = true;
						toDestroy.Add(game_object); // Add object to destroy later
					}

					i++;
				}

			}

			// Remove items
			foreach (GameObject game_object in toDestroy) {
				availSpots.Remove(game_object);
			}

		}
	}

	//...
	public void coreCollisionDistance(GameObject go) {

		float startAvail = availSpots.Count;

		// TODO: Add to a priority queue (min), based on distance from position of object colliding with core
		// For now, create without using a list, and use bubble sort to sort the list

		// Collision (object) position
		Vector3 colPos = go.transform.position;

		// List of objects close enough
		List<ShellPartPriority> closeObjects = new List<ShellPartPriority>();

		// Find shell parts close to the collision position, that are smaller than the colliding shell chunk, add to a list
		for (int i = 0; i < availSpots.Count; i++) {
			MeshCollider meshCollider = availSpots[i].transform.GetComponent<MeshCollider>();
			meshCollider.enabled = true; // Enable mesh collider to look for position
			Vector3 objPos = availSpots[i].transform.GetComponent<MeshCollider>().bounds.center;
			meshCollider.enabled = false; // Disable mesh collider again
			Vector3 diffVec = objPos - colPos;
			if (diffVec.magnitude <= maxDistance) {
				closeObjects.Add(new ShellPartPriority(diffVec.magnitude, availSpots[i]));
			}
		}

		// Sort the list of close shell parts (from closest to furthest away) using bubble sort
		if (distanceOverVolume) {
			closeObjects = distanceSort(closeObjects);
		} 

		else {
			closeObjects = volumeSort (closeObjects);
		}


		// Go through the list filling out the volume as close to the collision position as possible
		float filled = 0f;
		for (int i = 0; i < closeObjects.Count; i++) {
			//Debug.Log ("Current volume: " + closeObjects [i].CloseGameObject.GetComponent<ObjectVolumeScript> ().Volume + " | " + (go.GetComponent<ObjectVolumeScript>().Volume - filled));
			if (closeObjects[i].CloseGameObject.GetComponent<ObjectVolumeScript>().Volume < (go.GetComponent<ObjectVolumeScript>().Volume - filled)) {
				// Add to 'filled'
				filled += closeObjects[i].CloseGameObject.GetComponent<ObjectVolumeScript>().Volume;

				// Reactivate object
				//closeObjects[i].CloseGameObject.GetComponent<MeshCollider>().enabled = true;
				closeObjects[i].CloseGameObject.GetComponent<MeshRenderer>().enabled = true; // Enable the mesh renderer, making it 
				closeObjects[i].CloseGameObject.GetComponent<ShellDamageScript>().ResetHealth(); // Reset the health of object back to max health

				closeObjects[i].CloseGameObject.GetComponent<ShellPartScript>().GrowShell(); // Tell shell part to grow the shell part back

				// Remove the object from 'availSpots'
				availSpots.Remove(closeObjects[i].CloseGameObject);

				// Tell script controlling orbit to increase number of shell parts left
				memoryDistanceScript.incrementNumChildren();

				// Notify sound and music script that a shell chunk will start to merge
				GameObject.Find("Sound_and_Music_Var").GetComponent<SondAndMusic_Var>().shellChunkMergeWithCore();

			}
		}

	}

	private List<ShellPartPriority> distanceSort(List<ShellPartPriority> list) {

		// Bubble sort (based on distance)
		int length = list.Count;
		bool swapped = false;
		do {
			swapped = false;
			for (int i = 0; i < length-1; i++) {
				if (list[i] > list[i+1]) {
					// Swap
					ShellPartPriority temp = list[i];
					list[i] = list[i+1];
					list[i+1] = temp;
					// Set 'swapped' to true
					swapped = true;
				}
			}
		} while (swapped);

		return list;

	}

	private List<ShellPartPriority> volumeSort(List<ShellPartPriority> list) {

		// Bubble sort (based on volume)
		int length = list.Count;
		bool swapped = false;
		do {
			swapped = false;
			for (int i = 0; i < length-1; i++) {
				float vol_one = list[i].CloseGameObject.GetComponent<ObjectVolumeScript>().Volume;
				float vol_two = list[i+1].CloseGameObject.GetComponent<ObjectVolumeScript>().Volume;
				if (vol_one < vol_two) {
					// Swap
					ShellPartPriority temp = list[i];
					list[i] = list[i+1];
					list[i+1] = temp;
					// Set 'swapped' to true
					swapped = true;
				}
			}
		} while (swapped);

		return list;

	}

}
