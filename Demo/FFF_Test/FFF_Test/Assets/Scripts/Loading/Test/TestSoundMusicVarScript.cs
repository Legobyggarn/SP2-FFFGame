using UnityEngine;
using System.Collections;

public class TestSoundMusicVarScript : MonoBehaviour {

	public GameObject soundAndMusic;
	private SondAndMusic_Var soundMusicScript;

	// Use this for initialization
	void Start () {

		soundMusicScript = soundAndMusic.gameObject.GetComponent<SondAndMusic_Var>();

	}
	
	// Update is called once per frame
	void Update () {

		// If I pressed run test on 'SoundAndMusic_Var.cs'
		if (Input.GetKeyDown(KeyCode.I)) {
			runTest();
		}

	}

	private void runTest() {
		
		// Test variables
		float playerSpeed = 0f;
		float distPlayerCore = 0f;
		float numShellPartsCapsule = 0f;
		float numShellParts = 0f;
		float distanceToCenter = 0f;
		float distanceToCenterCore = 0f;
		float doneCapsules = 0f;

		// Test player speed
		playerSpeed = soundMusicScript.getPlayerSpeed();

		// Distance between player and a core specified by index (here index = 0)
		distPlayerCore = soundMusicScript.getDistancePlayerCapsule(0);

		// Reload time
		//...

		// Number of shell parts (on capsule)
		numShellPartsCapsule = soundMusicScript.getNumberOfShellParts(0);

		// Total number of shell parts
		numShellParts = soundMusicScript.getTotalNumberOfShellParts();

		// Distance to center
		distanceToCenter = soundMusicScript.getDistanceToCenter();

		// Distance to center core
		distanceToCenterCore = soundMusicScript.getDistanceToCenterCore(0);

		// Done capsules
		doneCapsules = soundMusicScript.getDoneCapsules();

		//...

		// Debug log all variables
		//Debug.Log("PlayerSpeed: " + playerSpeed);

		Debug.Log (
			"Player speed: " + playerSpeed + "\n" + 
			"Number of shell parts (on capsule, 0): " + numShellPartsCapsule + "\n" + 
			"Total number of shell parts: " + numShellParts + "\n" + 
			"Distance to center (player - orbit point): " + distanceToCenter + "\n" + 
			"Distance to center (core, 0 - orbit point): " + distanceToCenterCore + "\n" + 
			"Done capsules: " + doneCapsules + "\n"
		);



		// Test functions

		// Destroyed shell chunk
		soundMusicScript.shellChunkDestroyed();

		// Shell chunk merging
		soundMusicScript.shellChunkDestroyed();

		// Increase orbit
		soundMusicScript.orbitIncrease();

		// Decrease orbit
		soundMusicScript.orbitDecrease();

		// Core is at center
		soundMusicScript.coreAtCenter();

		// Bullet is destroyed
		soundMusicScript.bulletDestroyed();

	}

}
