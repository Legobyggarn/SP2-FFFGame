﻿using UnityEngine;
using System.Collections;

public class ObjectVolumeScript : MonoBehaviour {

	// Public
	public float minVolume;

	// Private
	private float volume;
	private bool active;

	// Setters/Getters
	public float Volume {
		get {
			return volume;
		}
	}

	public bool Active {
		get {
			return active;
		}
	}

	void Start() {

	}

	// Calculate the volume of the object (from mesh collider) (parameter game object to calculate the volume of
	public void calcVolume(MeshCollider mesh_collider, MeshRenderer mesh_renderer) { // Mesh renderer only for debug

		// Calculate volume
		Vector3 size = mesh_collider.bounds.size;
		volume = size.x * size.y * size.z;

		if (volume < minVolume) {
			active = false;
		}
		else {
			active = true;
		}

		//Debug.Log("Bounding volume: " + size);

		/*
		if (volume <= minVolume) {
			//Destroy(gameObject);
			mesh_collider.enabled = false;
			// Temp. Turn material red
			mesh_renderer.material.color = Color.red;
		}
		*/

	}

	public void calcVolumeDeactivate(MeshCollider mesh_collider, MeshRenderer mesh_renderer) { // Mesh renderer only for debug

		// Calculate volume
		Vector3 size = mesh_collider.bounds.size;
		volume = size.x * size.y * size.z;

		if (volume < minVolume) {
			active = false;
		} 
		else {
			active = true;
		}

		//Debug.Log("Volume of shell chunk: " + volume);
		//Debug.Log("Bounding volume: " + size);

		if (volume <= minVolume) {
			//Destroy(gameObject);
			//mesh_collider.enabled = false;
			// Temp. Turn material red
			mesh_renderer.material.color = Color.red;
		}

	}

}