using UnityEngine;
using System.Collections;

public class ShellDamageScript : MonoBehaviour {

	//[SerializeField] private float bulletDamage;

	// Public-ish
	// Damage
	// Base
	public float baseColDamage;
	// Bullets
	public float bulletDamage;
	// Health
	public float maxHealth;
	// Grace
	public float gracePeriod;

	public ParticleSystem hitPartSys;

	// Private
	private bool isHit;
	private float currHealth;
	private float gracePeriodTimer;
	private bool activeGracePeriod = true;

	// The rigidbody, for a test...
	private Rigidbody rigidBody;

	// Getters/Setters
	public float Health {
		get {
			return currHealth;
		}
	}

	public bool GracePeriodActive {
		get {
			return activeGracePeriod;
		}
	}

	// Use this for initialization
	void Start () {
	
		isHit = false;

		currHealth = maxHealth;

		activeGracePeriod = true;
		//Debug.Log ("Grace period has begun");

		// Test, set rigidbody to kinematic while under grace period (no collisions?)
		// Instead disable collision detection
		rigidBody = GetComponent<Rigidbody>();
		//rigidBody.isKinematic = false;
		//rigidBody.detectCollisions = false;

	}
	
	// Update is called once per frame
	void Update () {

		// Update grace period
		if (activeGracePeriod && gracePeriodTimer >= gracePeriod) {
			activeGracePeriod = false;
			//Debug.Log ("Grace period has ended");

			// Set rigidbody to not be kinematic (i.e react to collision)
			// Instead enable collision detection
			//rigidBody.detectCollisions = true;
		} 

		else {
			gracePeriodTimer += Time.deltaTime;
		}


		if (isHit)
		{
			isHit = false;
		}

	}

	// Collision
	void OnCollisionEnter(Collision collision) {
		
		if (collision.gameObject.tag == "Bullet") {
			//...
			if (!isHit) {
				isHit = true;
				if (GetComponent<ObjectVolumeScript> ().Active) {
					// Take damage
					currHealth -= bulletDamage;
					//Debug.Log ("Health: " + currHealth);
					checkDead (collision.transform.position);
					Destroy (collision.gameObject);
				}
			}			
		} 

		else if (collision.gameObject.tag == "ShellChunk" || collision.gameObject.tag == "Shell") {
			//...
			if (!isHit) {
				isHit = true;
				if (!activeGracePeriod && GetComponent<ObjectVolumeScript> ().Active) {
					// Use collision.relativeVelocity instead of calculating it yourself...
					Vector3 myVector = transform.GetComponent<Rigidbody> ().velocity;
					Vector3 itsVector = collision.transform.GetComponent<Rigidbody> ().velocity;
					Vector3 diffVector = myVector - itsVector;
					float diffLength = diffVector.magnitude;
					diffLength *= transform.localScale.x; // Change, should use the volume instead of scale
					float damage = baseColDamage * diffLength;
					currHealth -= damage;
					checkDead (collision.transform.position);
					Debug.Log("Damage: " + damage);
					//Debug.Log ("diffVector: " + diffVector + ", Magnitude: " + diffLength + " | Damage: " + damage + " | Health: " + currHealth);
				}
			}
		} 

		else if (collision.gameObject.tag == "Player") {
			Debug.Log ("Player hit by shell chunk");
		}	

	}

	void checkDead() {
		if (currHealth <= 0f) {
			// Split cube 
			GetComponent<BoxSplitBehaviourScript>().Split();
			// Destroy objects
			if (gameObject.tag == "Shell") {
				GetComponent<ShellPartScript>().HideShell();
			}
			else {
				Destroy(gameObject);
			}
		}
	}

	void checkDead(Vector3 collisionPoint) {
		if (currHealth <= 0f) {
			// Split cube 
			GetComponent<BoxSplitBehaviourScript>().Split(collisionPoint);
			// Destroy objects
			if (gameObject.tag == "Shell") {
				GetComponent<ShellPartScript>().HideShell();
			}
			else {
				Destroy(gameObject);
			}
		}
	}

	// Add as 'GiveHealth' instead?
	public void ResetHealth() {
		currHealth = maxHealth;
	}

}
