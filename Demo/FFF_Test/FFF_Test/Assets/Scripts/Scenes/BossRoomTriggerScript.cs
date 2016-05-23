using UnityEngine;
using System.Collections;

public class BossRoomTriggerScript : MonoBehaviour {

	// Public variables
	public GameObject target;
	public GameObject player;
	public GameObject screenTransition;
	public float moveTime;
	public string BossRoomSceneName;
	 
	// Private variables
	Vector3 lerpStartPos;
	private float timer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Collider enters the trigger
	void OnTriggerEnter(Collider collider) {

		if (collider.gameObject.tag == "Player") {
			// Get player script and disable movement (set speed 0 or lerp the speed to 'moveSpeed'
			player.GetComponent<PlayerMovement>().StaticSpeed = 0f;

			// Start screen transition (fade to white)
			screenTransition.GetComponent<ScenesTransision>().fadeToWhite();

			// Debug
			Debug.Log("[BossRoomTriggerScript] Fading to white...");

			// Begin to move the player towards 'target' with the speed 'moveSpeed'
			lerpStartPos = player.transform.position;
			StartCoroutine("moveToTarget");
		}

	}

	// Coroutine to lerp towards target
	IEnumerator moveToTarget() {
		float perc = 0f;
		// Lerp player towards 'target'
		while (timer < moveTime) {
			timer += Time.deltaTime;
			perc = timer / moveTime;
			player.transform.position = Vector3.Lerp(lerpStartPos, target.transform.position, perc);
			yield return null;
		}
		// [Byt scen till boss rum]
		// Change scene to boss room
		Application.LoadLevel(BossRoomSceneName);

		// Debug
		Debug.Log("[BossRoomTriggerScript] Switching scene to boss room...");
	}

}
