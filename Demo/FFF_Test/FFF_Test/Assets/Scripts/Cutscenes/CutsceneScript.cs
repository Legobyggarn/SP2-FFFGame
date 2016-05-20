using UnityEngine;
using System.Collections;

// Require audio source
[RequireComponent (typeof(AudioSource))]

public class CutsceneScript : MonoBehaviour {

	// Public variables
	public int cutsceneToPlay;
	public MovieTexture[] cutscenes;
	public string sceneToLoad; // Will be another variable taken from the level, in the full game.

	// Private variables
	private float elapsedTime = 0f;

	// Use this for initialization
	void Start () {
	
		// TODO: Add error handling for if the 'cutsceneToPlay' is outside of the range of 'cutscenes'.

		// Audiosource
		AudioSource audioSource = GetComponent<AudioSource>();
		// Assign movie texture
		GetComponent<Renderer>().material.mainTexture = cutscenes[cutsceneToPlay] as MovieTexture;
		// Assing audio
		audioSource.clip = cutscenes[cutsceneToPlay].audioClip;
		// Play cutscene (video and sound)
		cutscenes[cutsceneToPlay].Play();
		audioSource.Play();

	}


	// Update is called once per frame
	void Update () {

		elapsedTime += Time.deltaTime;

		// If the cutscene is done start a new scene
		if (elapsedTime >= cutscenes [cutsceneToPlay].duration) {
			Application.LoadLevel(sceneToLoad);
		}

	}
}
