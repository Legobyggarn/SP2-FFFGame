using UnityEngine;
using System.Collections;

public class CapsuleWallPartHealthScript : MonoBehaviour {

	// Public variables
	public float maxHealth;

	// Private variables
	public float currHealth;

	// Getters/Setters
	public float Health {
		get {
			return currHealth;
		}
	}


	// Use this for initialization
	void Start () {
		currHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Call this to affect the health
	public void affectHealth(float healthChange) {
		currHealth += healthChange;
	}

}
