using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class POIClosedRoom : MonoBehaviour {

	// Public variables
	public bool doorsLockedOnStart = false;
	public List<GameObject> doors;
	public List<GameObject> entrances;

	// Private variables
	//...

	void Start () {
		if (!doorsLockedOnStart) {
			unlockDoors();
		}
	}

	// Lock doors
	// UNNECESSARY? IS NEVER USED! 
	// 	(May want it if the doors should only appear when room is first entered, or maybe appear when room is entered and removed when the player leaves the room.)
	// Only called after prop/POI has been destroyed
	public void lockDoors() {
		// Unlock all doors (enables doors)
		foreach (GameObject door in doors) {
			door.SetActive(true);
		}
		// Deactivate all entrances
		foreach (GameObject entrance in entrances) {
			entrance.SetActive(false);
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
