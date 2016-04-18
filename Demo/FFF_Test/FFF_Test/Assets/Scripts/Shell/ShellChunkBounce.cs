using UnityEngine;
using System.Collections;

public class ShellChunkBounce : MonoBehaviour {

	// Public variables

	// Private variables
	private float lineLength = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {

		/*
		Debug.DrawLine(contact.point, contact.normal * lineLength, Color.white, 3.0f, true);
		Debug.DrawLine(contact.point, reclectionPoint * lineLength, Color.red, 3.0f, false);
		*/

		foreach (ContactPoint contact in collision.contacts) {
			//  print(contact.thisCollider.name + " hit " + contact.otherCollider.name);
			//  drawMyLine(contact.point, contact.normal, Color.white);
			Debug.DrawLine(contact.point, contact.normal * lineLength, Color.white, 3.0f, false);

			/*
			Rigidbody rb = GetComponent<Rigidbody>();

			Vector3 velDir = rb.angularVelocity;
			Vector3 reflectionPoint = Vector3.Reflect(velDir, contact.normal);

			Debug.DrawLine(contact.point, reflectionPoint * lineLength, Color.red, 3.0f, false);
			*/
		}

	}

}
