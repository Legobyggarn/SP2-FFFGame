﻿using UnityEngine;
using System.Collections;

public class POIClosedRoomEntrance : MonoBehaviour {

	// Public variables
	//...

	// Private variables
	//...

	// When player enters the room (or leaves)
	void OnTriggerEnter(Collider collider) {

		// If player has entered
		if (collider.gameObject.tag == "Player") {
			transform.root.GetComponent<POIClosedRoom>().lockDoors();
		}

	}

}