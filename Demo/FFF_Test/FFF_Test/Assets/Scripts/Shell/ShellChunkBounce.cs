using UnityEngine;
using System.Collections;

public class ShellChunkBounce : MonoBehaviour {

	// Public variables
	public float bounceForce;
	public float debugLineLength;
	public Vector3 centerPoint;

	public bool towardsCenter;
	public bool towardsCenterAsNormal;
	public bool reflectNormal;
	public bool invertDirection;

	// Private variables
	private Vector3 lastForward;

	// Use this for initialization
	void Start () {
	
		lastForward = new Vector3();

	}
	
	// Update is called once per frame
	void Update () {
	
		// Debug (Draw a line for direction every frame)
		Vector3 velocity = GetComponent<Rigidbody>().velocity.normalized;
		Vector3 startPos = transform.position;
		Vector3 endPos = transform.position + velocity * debugLineLength;
		Debug.DrawLine(startPos, endPos, Color.white, Time.deltaTime, false);

		// Update latest movement direction
		lastForward = velocity;

	}

	void OnCollisionEnter(Collision collision) {
		
		// Bounce (if not in grace period, or collided with a projectile)
		if (collision.gameObject.tag != "Bullet") {
			if (!GetComponent<ShellDamageScript>().GracePeriodActive) {

				// Apply a force equal to the inverted force of the object (to make it stay in place, so later forces acctually take effect)
				Vector3 velocity = GetComponent<Rigidbody> ().velocity;
				GetComponent<Rigidbody> ().AddForce (-velocity, ForceMode.Impulse);

				Vector3 startPoint = new Vector3();
				Vector3 endPoint = new Vector3();

				if (towardsCenter) { // Shell chunks bounce so that they will start moving towards the position defined in the center variable
					/*
					// Apply a force equal to the inverted force of the object (to make it stay in place, so later forces acctually take effect)
					Vector3 velocity = GetComponent<Rigidbody> ().velocity;
					GetComponent<Rigidbody> ().AddForce (-velocity, ForceMode.Impulse);
					*/

					// Apply force...
					Vector3 dirVector = centerPoint - transform.position; // Get the direction vector
					dirVector = dirVector.normalized; // Normalize the direction vector
					GetComponent<Rigidbody> ().AddForce (dirVector * bounceForce, ForceMode.Impulse);

					// Displaying debug line
					startPoint = transform.position;
					endPoint = (transform.position + dirVector * debugLineLength);
				} 

				else if (towardsCenterAsNormal) { // Shell chunks bounce of walls using the vector against the center variable as a normal to reflect about
					/*
					// Apply a force equal to the inverted force of the object (to make it stay in place, so later forces acctually take effect)
					Vector3 velocity = GetComponent<Rigidbody>().velocity;
					GetComponent<Rigidbody>().AddForce (-velocity, ForceMode.Impulse);
					*/

					// Apply force...
					Vector3 inVector = lastForward;
					//inVector = inVector.normalized;
					Vector3 toCenter = centerPoint - transform.position; // Get the direction vector towards the center position
					toCenter = toCenter.normalized; // Normalize the center direction vector
					Vector3 outVector = Vector3.Reflect (inVector, toCenter);
					GetComponent<Rigidbody> ().AddForce (outVector * bounceForce, ForceMode.Impulse);

					// Displaying debug line 'outVector'
					startPoint = transform.position;
					endPoint = (transform.position + outVector * debugLineLength);

					// Debug 'inVector'
					Vector3 startIn = transform.position;
					Vector3 endIn = transform.position - inVector * debugLineLength;
					Debug.DrawLine (startIn, endIn, Color.green, 2f, false);

					// Debug "normal"
					Vector3 startNormal = transform.position;
					Vector3 endNormal = transform.position + toCenter * debugLineLength;
					Debug.DrawLine (startNormal, endNormal, Color.blue, 2f, false);

				} 

				else if (reflectNormal) { // Shell chunks reflect about the actual normal of the surface/plane that it collides with
					/*
					// Apply a force equal to the inverted force of the object (to make it stay in place, so later forces acctually take effect)
					Vector3 velocity = GetComponent<Rigidbody>().velocity;
					GetComponent<Rigidbody>().AddForce (-velocity, ForceMode.Impulse);
					*/

					// Apply force (and find the "real" normal)...
					// InVector
					Vector3 inVector = lastForward;
					//inVector = inVector.normalized;

					// Normal

					// Algorithm for finding the normal
					// Algorithm idea found here: (http://answers.unity3d.com/questions/279634/collisions-getting-the-normal-of-the-collision-sur.html | Answered by user: Nevermind | Unity forum/answers)
					// Take a hit point (or maybe an average of all of them)
					Vector3 point = collision.contacts[0].point; // Point of collision (atleast number 1)
					Vector3 dir = -collision.contacts[0].normal; // The normal of the collision point, but poiting at the collision surface
					// Step away from the hit point
					point -= dir;
					// Shot a raycast against the "model" that was hit


					// TODO: Raycast could (and does maybe sometimes) hit the shell chunk, which leads to the normal being wrong.

					Vector3 normal = new Vector3(); // Get the normal of the collision surface (or collision)
					RaycastHit hit; // Info about the raycast hit (if there is one)
					if (Physics.Raycast(point, dir, out hit)) {
						// Get the normal of the triangle hit
						normal = hit.normal;
					}

					// Debug line for raycast
					Vector3 startRay = point;
					Vector3 endRay = point + dir;
					Debug.DrawLine (startRay, endRay, Color.yellow, 2f, false);


					// OutVector (can cause problems if the normal is non-existant, need to fix a "good-enough" normal if the raycast doesn't hit anything)
					Vector3 outVector = Vector3.Reflect(inVector, normal);

					// Apply the force to the OutVector
					GetComponent<Rigidbody>().AddForce (outVector * bounceForce, ForceMode.Impulse);

					// Displaying debug line 'outVector'
					startPoint = transform.position;
					endPoint = (transform.position + outVector * debugLineLength);

					// Debug 'inVector'
					Vector3 startIn = transform.position;
					Vector3 endIn = transform.position - inVector * debugLineLength;
					Debug.DrawLine (startIn, endIn, Color.green, 2f, false);

					// Debug "normal"
					Vector3 startNormal = transform.position;
					Vector3 endNormal = transform.position + normal * debugLineLength;
					Debug.DrawLine (startNormal, endNormal, Color.blue, 2f, false);
				} 

				else if (invertDirection) { // Shell chunks will start moving in the opposite direction after collision
					//...
					//Vector3 velocity = GetComponent<Rigidbody>().velocity;
					GetComponent<Rigidbody> ().AddForce (-velocity.normalized * bounceForce, ForceMode.Impulse);

					// Displaying debug line
					startPoint = transform.position;
					endPoint = (transform.position + -velocity.normalized * debugLineLength);

				} 

				else { // None marked
					// Do nothing...
				}

				// Debug
				Debug.Log ("Inverted version: Name: "+ collision.gameObject.name + " | Tag: " + collision.gameObject.tag + " | Start point: " + startPoint + " | End point: " + endPoint);
				Debug.DrawLine (startPoint, endPoint, Color.red, 2f, false);

			}
		}


		/*
		Debug.DrawLine(contact.point, contact.normal * lineLength, Color.white, 3.0f, true);
		Debug.DrawLine(contact.point, reclectionPoint * lineLength, Color.red, 3.0f, false);
		*/


		/*
		foreach (ContactPoint contact in collision.contacts) {
			//  print(contact.thisCollider.name + " hit " + contact.otherCollider.name);
			//  drawMyLine(contact.point, contact.normal, Color.white);
			Debug.DrawLine(contact.point, contact.normal * lineLength, Color.white, 3.0f, false);

			///
			Rigidbody rb = GetComponent<Rigidbody>();

			Vector3 velDir = rb.angularVelocity;
			Vector3 reflectionPoint = Vector3.Reflect(velDir, contact.normal);

			Debug.DrawLine(contact.point, reflectionPoint * lineLength, Color.red, 3.0f, false);
			///
		}
		*/

	}

}
