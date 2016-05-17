using UnityEngine;
using System.Collections;

public class ShellDamageScript : MonoBehaviour {
	
	// Public variables
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

	// Private variables
	private bool isHit;
	private bool firstHit = false; 
	private float currHealth;
	private float gracePeriodTimer = 0f;
	private bool activeGracePeriod = true;
	private GameObject parentGo;

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

		parentGo = transform.parent.gameObject;

	}
	
	// Update is called once per frame
	void Update () {

		// Update grace period
		if (activeGracePeriod && gracePeriodTimer <= gracePeriod) {
			gracePeriodTimer += Time.deltaTime;
		} 

		else {
			gracePeriodTimer = 0f;
			activeGracePeriod = false;
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
			if (!firstHit) {
				firstHit = true; 

				//Debug.Log ("lookPosistion set 1");
				parentGo.GetComponent<memoryDistance> ().setLookPosition (collision.transform.position);
			}

			if (!isHit) {
				isHit = true;
				if (GetComponent<ObjectVolumeScript>().Active && !activeGracePeriod) {
					// Take damage
					currHealth -= bulletDamage;
					//Debug.Log ("Health: " + currHealth);
					checkDead(collision.transform.position);
					Destroy(collision.gameObject);
				}
			}			
		} 

		else if (collision.gameObject.tag == "ShellChunk" || collision.gameObject.tag == "Shell") {
			//...
			if (!isHit) {
				isHit = true;
				if (!activeGracePeriod && GetComponent<ObjectVolumeScript>().Active && !activeGracePeriod) {
					// Use collision.relativeVelocity instead of calculating it yourself...
					Vector3 myVector = transform.GetComponent<Rigidbody>().velocity;
					Vector3 itsVector = collision.transform.GetComponent<Rigidbody>().velocity;
					Vector3 diffVector = myVector - itsVector;
					float diffLength = diffVector.magnitude;
					diffLength *= transform.localScale.x; // Change, should use the volume instead of scale
					float damage = baseColDamage * diffLength;
					currHealth -= damage;
					checkDead(collision.transform.position);
					//Debug.Log("Damage: " + damage + " | " + currHealth);
					//Debug.Log ("diffVector: " + diffVector + ", Magnitude: " + diffLength + " | Damage: " + damage + " | Health: " + currHealth);
				}
			}
		} 

		else if (collision.gameObject.tag == "Player") {
			//Debug.Log ("Player hit by shell chunk");
		}	

	}

	void checkDead(Vector3 collisionPoint) {
		if (currHealth <= 0f) {
			// Split shell chunk 
			GetComponent<BoxSplitBehaviourScript>().Split(collisionPoint);
			// Destroy object, if tag not "Shell"
			if (gameObject.tag == "Shell") {
				GetComponent<ShellPartScript>().HideShell();
			}
			else { // If not shell destroy self
				Destroy(gameObject);
			}
			// Notify sound and music script that this shell chunk is destroyed
			GameObject.Find("Sound_and_Music_Var").GetComponent<SondAndMusic_Var>().shellChunkDestroyed();
		}
	}

	// Add as 'GiveHealth' instead?
	public void ResetHealth() {
		currHealth = maxHealth;
	}

}
