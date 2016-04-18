using UnityEngine;
using System.Collections;

public class orbitScript : MonoBehaviour {

	public float rotationSpeedX;
	public float rotationSpeedY;
	public float rotationSpeedZ;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (rotationSpeedX  * Time.deltaTime, rotationSpeedY * Time.deltaTime, rotationSpeedZ  * Time.deltaTime));
	}
}
