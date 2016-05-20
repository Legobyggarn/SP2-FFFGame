using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class POIClosedRoom : MonoBehaviour {

	// Public variables
	public List<GameObject> doors;

	// Private variables
	//...

	// Lock doors
	// UNNECESSARY? IS NEVER USED! 
	// 	(May want it if the doors should only appear when room is first entered, or maybe appear when room is entered and removed when the player leaves the room.)
	public void lockDoors() {
		// Unlock all doors (enables doors)
		foreach (GameObject door in doors) {
			door.SetActive(true);
		}
	}

	// Unlock doors
	public void unlockDoors() {
		// Unlock all doors (disable doors)
		foreach (GameObject door in doors) {
			door.SetActive(false);
		}
	}

}
