using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FindCenterTestScript : MonoBehaviour {

	public List<GameObject> list;

	// Use this for initialization
	void Start () {

		for (int i = 0; i < list.Count; i++) {
			Vector3 center = list[i].GetComponent<MeshCollider>().bounds.center;
			//Debug.Log("Center: " + center);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown(KeyCode.J)) {
			for (int i = 0; i < list.Count; i++) {
				list [i].GetComponent<MeshCollider> ().enabled = true; // Activate
				Vector3 center = list[i].GetComponent<MeshCollider>().bounds.center;
				Debug.Log("Center: " + center);
				list [i].GetComponent<MeshCollider> ().enabled = false; // Deactivate
			}
		}

		if (Input.GetKeyDown(KeyCode.K)) {
			for (int i = 0; i < list.Count; i++) {

				//list [i].GetComponent<MeshCollider> ().enabled = true; // Activate
				Vector3 center = list[i].GetComponent<MeshCollider>().bounds.center;
				Debug.Log("Center: " + center);
				//list [i].GetComponent<MeshCollider> ().enabled = false; // Deactivate

				// Create new game object with the same mesh as the current game object
				Mesh mesh = list[i].GetComponent<MeshFilter>().mesh;
				//Material material = list[i].GetComponent<MeshFilter>().material;
				list[i].GetComponent<MeshRenderer>().enabled = false; // Disable the mesh renderer

				// Parent
				GameObject parent = new GameObject();
				parent.transform.name = "Parent";
				// Transform
				parent.transform.position = center;
				parent.transform.rotation = list[i].transform.rotation;

				// Child (model)
				GameObject go = new GameObject();
				go.transform.name = "Child";
				// Parenting
				go.transform.parent = parent.transform;
				// Mesh filter, (mesh, material)
				MeshFilter mesh_filter = go.AddComponent<MeshFilter>();
				go.gameObject.GetComponent<MeshFilter>().mesh = mesh;
				//go.gameObject.GetComponent<MeshFilter>().material = material;
				// Mesh renderer
				MeshRenderer mesh_renderer = go.AddComponent<MeshRenderer>();
				// Transform
				go.transform.position = new Vector3(0f, 0f, 0f);
				go.transform.rotation = parent.transform.rotation;
				//go.transform.localScale = list[i].transform.localScale;



				/*
				//list [i].GetComponent<MeshCollider> ().enabled = true; // Activate
				Vector3 center = list[i].GetComponent<MeshCollider>().bounds.center;
				Debug.Log("Center: " + center);
				//list [i].GetComponent<MeshCollider> ().enabled = false; // Deactivate
				*/
			}
		}

	}
}
