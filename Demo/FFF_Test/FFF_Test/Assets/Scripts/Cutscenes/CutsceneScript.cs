using UnityEngine;
using System.Collections;

// Require audio source
[RequireComponent (typeof(AudioSource))]

public class CutsceneScript : MonoBehaviour {

	// Public variables
	public int cutsceneToPlay;
	public MovieTexture[] cutscenes;

	// Private variables
	//...

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
	
		// Check if the cutscene is done. If so start new scene...

	}
}
