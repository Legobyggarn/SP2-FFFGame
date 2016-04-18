using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomizeMeshScript : MonoBehaviour {

	// Public variables
	public List<Mesh> shellChunkMeshes;

	// Private variables

	// Use this for initialization
	void Start () {
	
		if (shellChunkMeshes.Count > 0) {
			int randomMesh = Random.Range (0, shellChunkMeshes.Count - 1);
			transform.GetChild(0).GetComponent<MeshFilter>().mesh = shellChunkMeshes[randomMesh];
			GetComponent<MeshCollider> ().sharedMesh = shellChunkMeshes [randomMesh];

			//transform.GetChild(0).GetComponent<MeshRenderer> ().material.color = new Color (125f/255f, 80f/255f, 0f/255f);

			// Recalculate size
			GetComponent<ObjectVolumeScript>().calcVolumeDeactivate(transform.GetComponent<MeshCollider>(), transform.GetChild(0).transform.GetComponent<MeshRenderer>());

		}

	}

}
