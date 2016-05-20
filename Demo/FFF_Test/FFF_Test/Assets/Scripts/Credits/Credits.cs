using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Credits : MonoBehaviour {

	// TODO: Add support for groupings of credits, with spacing and header.

	// Public variables
	public string[] credits;
	public float scrollDuration;
	public float creditSpawnInterval = 1f;

	[Header("Color")]
	public float fadeInDuration;
	public float fadeOutDuration;
	public Color fadedInColor;
	public Color fadedOutColor;

	[Header("Credit prefab")]
	public GameObject creditPrefab;

	[Header("Scroll start/end")]
	public Transform scrollStart;
	public Transform scrollEnd;

	[Header("Screen transition")]
	public GameObject sceenTransition;

	// Private variables
	//private List<GameObject> creditTexts;
	private float totalScrollTime;

	// Use this for initialization
	void Start () {
		StartCoroutine("spawnCredits");

		// Calculate total time
		totalScrollTime = scrollDuration + creditSpawnInterval * credits.Length;

		// End level when credits are done
		StartCoroutine("endLevel");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator spawnCredits() {
		foreach (string credit in credits) {	
			// Spawn credit
			GameObject go = Instantiate(creditPrefab, scrollStart.position, transform.rotation) as GameObject;
			go.transform.parent = transform;
			go.GetComponent<CreditTextScript>().setupCreditText(credit, scrollStart.position, scrollEnd.position, scrollDuration, fadeInDuration, fadeOutDuration, fadedInColor, fadedOutColor);
			// Wait to spawn next credit (if there is one)
			yield return new WaitForSeconds(creditSpawnInterval);
		}
	}

	IEnumerator endLevel() {

		yield return new WaitForSeconds (totalScrollTime);
		// Fade to white and go to main menu...
		sceenTransition.GetComponent<ScenesTransision>().fadeToWhite();

		// Temp. Switch to main menu directly
		//Application.LoadLevel("MainMenu");

	}

}
