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
			if (!collision.gameObject.GetComponent<ShellDamageScript>().GracePeriodActive) {
				if (useDistance) {
					coreCollisionDistance(collision.gameObject);

					// Mark collision object
					/*
					collision.gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
					collision.gameObject.transform.GetChild(0).gameObject.GetComponent<MeshCollider>().enabled = false;
					*/
				} 
				else {
					coreCollision(collision.gameObject);
				}
			}

			else {
				// Add force away from core
				//collision.gameObject.transform.position;
				Debug.Log("Bouncing away");
				collision.gameObject.transform.name = "Bounced"; // Marking object
				//Debug.Break(); // Pause the game
				//collision.gameObject.transform.position = Vector3.zero;

				/*
				Vector3 currVelocity = collision.gameObject.GetComponent<Rigidbody>().velocity;
				collision.gameObject.GetComponent<Rigidbody>().AddForce(-currVelocity * 100f, ForceMode.Impulse);
				*/
			}

			Destroy (collision.gameObject);

			/*
			// TEMP. Only remove bullets
			if (collision.gameObject.tag == "Bullet") {
				Destroy (collision.gameObject);
			}
			*/

		}

	}

	// Notify that a collision with the shell has occured (parameter should contain the collided shell piece)
	public void shellCollision(GameObject go) {
		//...

		go.GetComponent<MeshCollider>().enabled = false;
		go.GetComponent<MeshRenderer>().enabled = false;

		availSpots.Add(go);

		// Tell shell part to shrink the shell part
		//go.GetComponent<ShellPartScript>().ShrinkShell();

		// Tell script controlling orbit to decrease number of shell parts left
		memoryDistanceScript.decrementNumChildren();

		// "Grow" back the memory bit.

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
					game_object.GetComponent<MeshCollider> ().enabled = true;
					game_object.GetComponent<MeshRenderer> ().enabled = true;

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
						game_object.GetComponent<MeshCollider> ().enabled = true;
						game_object.GetComponent<MeshRenderer> ().enabled = true;
						toDestroy.Add(game_object); // Add object to destroy later
					}

					i++;
				}
				//Debug.Log("Filled: " + filledVolume + " | Volume to fill: " + hitGOVolume);
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
		List<int> toRemove = new List<int>();
		float filled = 0f;
		for (int i = 0; i < closeObjects.Count; i++) {
			//Debug.Log ("Current volume: " + closeObjects [i].CloseGameObject.GetComponent<ObjectVolumeScript> ().Volume + " | " + (go.GetComponent<ObjectVolumeScript>().Volume - filled));
			if (closeObjects[i].CloseGameObject.GetComponent<ObjectVolumeScript>().Volume < (go.GetComponent<ObjectVolumeScript>().Volume - filled)) {
				// Add to 'filled'
				filled += closeObjects[i].CloseGameObject.GetComponent<ObjectVolumeScript>().Volume;
				// Reactivate object

				//closeObjects[i].CloseGameObject.GetComponent<MeshCollider>().enabled = true;
				closeObjects[i].CloseGameObject.GetComponent<MeshRenderer>().enabled = true;

				closeObjects[i].CloseGameObject.GetComponent<ShellDamageScript>().ResetHealth();

				// Tell shell part to grow the shell part back
				closeObjects[i].CloseGameObject.GetComponent<ShellPartScript>().GrowShell();

				// Remove the object from 'availSpots'
				availSpots.Remove(closeObjects[i].CloseGameObject);

				// Tell script controlling orbit to increase number of shell parts left
				memoryDistanceScript.incrementNumChildren();

				// Add to 'toDestroy'
				toRemove.Add(i);


				Debug.Log("Grow back");
			}
		}

		// DOESN'T DO ANY DIFFERENCE, SO THEREFORE IT IS COMMENTED OUT!
		// DO WE WANT TO FILL UP THE VOLUME?
		// Try to fill up remaining spaces (perhaps something that kicks in if the filled out percentage is less than a set variable (for example 50%))
		// Remove objects to be removed
		/*
		float remainFilled = filled; // Outside if because it's used in debug logs further down (outside the if-statement)

		if (fillUpVolume) {
			for (int i = 0; i < toRemove.Count; i++) {
				closeObjects.RemoveAt (toRemove [i]);
			}

			// Sort (by volume)
			closeObjects = volumeSort (closeObjects);

			// Fill in remaining spaces
			for (int i = 0; i < closeObjects.Count; i++) {
				if (closeObjects [i].CloseGameObject.GetComponent<ObjectVolumeScript> ().Volume < (go.GetComponent<ObjectVolumeScript> ().Volume - remainFilled)) {
					// Add to 'remainFilled'
					remainFilled += closeObjects [i].CloseGameObject.GetComponent<ObjectVolumeScript> ().Volume;
					// Reactivate object
					closeObjects [i].CloseGameObject.GetComponent<MeshCollider> ().enabled = true;
					closeObjects [i].CloseGameObject.GetComponent<MeshRenderer> ().enabled = true;
					// Remove the object from 'availSpots'
					availSpots.Remove (closeObjects [i].CloseGameObject);
				}
			}
		}
		*/

		// DEBUG OUTPUT
		//float filledPerc = (filled/go.GetComponent<ObjectVolumeScript>().Volume)*100; // Percentage of collided chunk recreated
		//Debug.Log ("Filled: " + filledPerc + "% with " + startAvail + " available");	

	}

	private List<ShellPartPriority> distanceSort(List<ShellPartPriority> list) {

		// Bubble sort (for now atleast)
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

		// Bubble sort (for now atleast)
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
