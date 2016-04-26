	using UnityEngine;
using System.Collections;

public class ObjectVolumeScript : MonoBehaviour {

	// TODO: Add functionality to change the mass of the rigidbody depending on the calculated volume of the object(?).

	// Public
	public float minVolume;
	public float scalingAdjustment = 1f;

	// Private
	private float volume;
	private bool active;

	private bool aspiringVolumeSet = false;
	private float aspiringVolume;
	private float mPreviousHeight;

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

		// Get mesh collider(?)
		// Get mesh renderer(?)

		recalculateVolume(mesh_collider);

		if (volume < minVolume) {
			active = false;
		}
		else {
			active = true;
		}

		// Not needed because this is never called by a shell chunk that needs it?
		// Calculate the real scale based on the aspiring volume variable
		if (aspiringVolumeSet) {
			// Calculate the scalar to scale to the aspiring volume, and adding a scale adjustment (because the volume is not the exact volume of the chunk, but the bounding volume)
			float scalar = Mathf.Pow ((aspiringVolume / volume), (1f / 3f));
			scalar *= Mathf.Pow(scalingAdjustment, (1f / 3f));
			transform.localScale *= scalar;
		}

		else {
			// Re-calculate scale
			recalculateScale();
		}

		// Recalculate volume to ensure that 'volume' has the right value
		recalculateVolume(mesh_collider);

	}

	public void calcVolumeDeactivate(MeshCollider mesh_collider) { // Mesh renderer only for debug

		// Get mesh collider(?)
		MeshRenderer mesh_renderer = transform.GetChild(0).transform.GetComponent<MeshRenderer>(); // TODO: Change "GetChild(0)", because this will stop working if another child is added at that position.

		recalculateVolume(mesh_collider);

		if (volume <= minVolume) {
			active = false;
			mesh_renderer.material.color = Color.red;
		} 

		else {
			active = true;
		}

		// Calculate the real scale based on the aspiring volume variable
		if (aspiringVolumeSet) {
			// Calculate the scalar to scale to the aspiring volume, and adding a scale adjustment (because the volume is not the exact volume of the chunk, but the bounding volume)
			float scalar = Mathf.Pow((aspiringVolume / volume), (1f / 3f));
			scalar *= Mathf.Pow(scalingAdjustment, (1f / 3f));
			transform.localScale *= scalar;
		}

		else {
			// Re-calculate scale
			recalculateScale();
		}

		// Recalculate volume to ensure that 'volume' has the right value
		recalculateVolume(mesh_collider);

		//Debug.Log("Volume: " + volume + " | Aspiring volume: " + aspiringVolume);

	}

	private void recalculateVolume(MeshCollider mesh_collider) {

		// Calculate volume
		Vector3 size = mesh_collider.bounds.size;
		volume = size.x * size.y * size.z;

	}

	private void recalculateScale() {

		// Calculate the scale, given the global scale. (Only for shell parts, shell chunks created during play will not call this)
		GameObject globalScaleGO = GameObject.Find("GLOBAL_SCALE");
		transform.localScale *= globalScaleGO.GetComponent<GlobalScaleScript>().shellScale;

	}

	// Set scale
	public void SetAspiringVolume(float aspVolume) {
		// Set the aspiring volume variable
		aspiringVolumeSet = true;
		aspiringVolume = aspVolume;
	}

}
