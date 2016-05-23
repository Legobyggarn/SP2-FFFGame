using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;

public class SondAndMusic_Var : MonoBehaviour {

	// TODO: Have two seperate lists, one for cores and one for shells
	// TODO: Add protection against invalid indexes in methods using an index for looking up a core

	// Added by SoundDesigner
	public GameObject shellChunk;
    
	// Sound
	//så man kan använda sig av fmodsfunktionalitet
	[FMODUnity.EventRef]
	public string SoundEvent;

	FMOD.Studio.EventInstance Sound;


	public GameObject player;
	public List <GameObject> coreList;
	public Transform orbitsCenterTransform;
	private Vector3 orbitsCenter;

	public GameObject POIManager;

	// Use this for initialization
	void Start () 
	{
		if (orbitsCenterTransform != null) {
			orbitsCenter = orbitsCenterTransform.position;
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}


	//Get Functions for sound and music  

	// Get the speed of the player
	public float getPlayerSpeed()
	{
		float playerSpeed = player.GetComponent<CharacterController>().velocity.magnitude;
		//float temp = 0;
		return playerSpeed;
	}

	// Get the distance from ...(somewhere)... to the core, specified by index. [Used to return a Vector3]
	public float getDistancePlayerCapsule(int index) // From where? Going with from player
	{
		// Getting the distance between the player and a core specified by index
		Vector3 playerPosition = player.transform.position;
		Vector3 corePosition = coreList [index].transform.position;
		Vector3 distVec = corePosition - playerPosition;
		float distance = distVec.magnitude;
		return distance;
	}

	// Returns the reload time (may not be available)
	public float getReloadTime() // [May not be possible if reload time is not set but determined by animations]
	{
		float temp = 0;
		return temp;
	}

	// Returnes the number off shell parts still on the memory with index i. (Changed "ShellChunks" into "ShellParts")
	public float getNumberOfShellParts(int index) 
	{
		for(int i = 0; i < coreList.Count; i++)
		{
			if (i == index) 
			{
				return coreList[i].GetComponent<pivoPointRotation>().getChildCount();
			}
		}
		return 0;
	}

	// Returns the number off chunks still on all memories. (Changed "ShellChunks" into "ShellParts")
	public float getTotalNumberOfShellParts() 
	{
		float numberOfShellParts = 0f;

		for(int i = 0; i < coreList.Count; i++)
		{
			numberOfShellParts += coreList[i].GetComponent<pivoPointRotation>().getChildCount();
		}

		return numberOfShellParts;
	}

	// Returns the value of the players distance to the orbit point 
	public float getDistanceToCenter() 
	{
		if (orbitsCenterTransform != null) {
			Vector3 distanceVector = orbitsCenter - player.transform.position;
			float distanceToCenter = distanceVector.magnitude;
			//float temp = 0;
			return distanceToCenter;
		} 

		else { // No center...
			return -1;
		}
	}

	// Returns the value of the core with index i distance to the orbit point.
	// return float?
	public float getDistanceToCenterCore(int index)
	{

		Vector3 capsulePos = coreList[index].transform.position;
		Vector3 orbitPos = coreList[index].GetComponent<pivoPointRotation>().getOrbitPoint();
		Vector3 distVec = orbitPos - capsulePos;
		float distance = distVec.magnitude;
		return distance;

	}

	// Get the amount of capsules which shell has been destroyed. [Should return an int]
	public float getDoneCapsules() 
	{
		int numDoneCapsules = 0;
		foreach (GameObject core in coreList) {
			if (core.GetComponent<pivoPointRotation>().isDone()) {
				numDoneCapsules++;
			}
		}

		return numDoneCapsules;

	}

	// (Get the size of the orbit of the capsule(core) of index i?)
	// return float? Same thing as 'getDistanceToCenterCore'?
	public Vector3 getCapsulOrbitDistance(int index) 

	{
		Vector3 temp = new Vector3 (0f,0f,0f);
		return temp;
	}



	// Functions called when an event occured

	// Shell chunk gets destroyed
	// [Called from a shellChunk script before destroying self]
	public void shellChunkDestroyed(Vector3 pos) {
		// Play music/sound or call another funtion...

		//gör så eventet faktiskt funkar
		Sound = FMODUnity.RuntimeManager.CreateInstance (SoundEvent);

		//sätter så eventet spelas ifrån objectet som detta script ligger på
		if (SoundEvent != null) 
		{
			Sound.set3DAttributes(RuntimeUtils.To3DAttributes(pos));
			//startar eventet
			Sound.start();
		}

		//shellChunk.GetComponent<ShellDestroySound>().Play(pos);
	}

	// Shell chunk "merges" with core again, called when merging starts
	// [Called from shell handler when a shell part is "re-added"]
	public void shellChunkMergeWithCore() {
		// Play music/sound or call another funtion...
	}

	// Orbit increases
	// [Called from pivoPointRotation when number of children are incremented]
	public void orbitIncrease() {
		// Play music/sound or call another funtion...
	}

	// Orbit decreases
	// [Called from pivoPointRotation when number of children are decremented]
	public void orbitDecrease() {
		// Play music/sound or call another funtion...
	}

	// Core arrives at center
	// [Called from pivoPointRotation when core reaches the center]
	public void coreAtCenter() {
		// Play music/sound or call another funtion...
	}

	// Bullet object gets destroyed (not called anywhere yet)
	// [Called from a bullet script just before the bullet is destroyed]
	public void bulletDestroyed() {
		// Play music/sound or call another funtion...
	}

	// Part of wall destroyed
	// [Called from a wall object when part are destroyed]
	public void partOfWallDestroyed() {
		// Play music/sound or call another function...
	}


	// TODO: Change name to POIDestroyed?
	// TODO: Add function that is called when a POI is hit.
	// Points of interest
	// A new point of interest was discovered
	public void POIDiscovered(string name) {
		// Play music/sound or call another function...
	}

	// A new important point of interest was discovered
	public void importantPOIDiscovered(string name) {
		// Play music/sound or call another function...
		/*
		if (name == "Rankor")
		{
			musicPlayer.PlayVO(1);
		}
		if (name == "Bär")
		{
			musicPlayer.PlayVO(2);
		}
		if (name == "Fjäril")
		{
			musicPlayer.PlayVO(3);
		}
		if (name == "Sköldpadda")
		{
			musicPlayer.PlayVO(4);
		}
		if (name == "Blomknopp")
		{
			musicPlayer.PlayVO(5);
		}
		*/
	}

	// When a POI is hit. 
	public void POIHit(Vector3 hitPosition) {
		// Play music/sound or call another function...
	}

	// Get amount of destroyed points of interest
	public int getNumOfDiscoveredPOI() {
		return POIManager.GetComponent<POIManagerScript>().numOfDiscoveredPOI();
	}

	// Get amount of discovered important points of interest
	public float getNumOfDiscoveredImportantPOI() {
		return POIManager.GetComponent<POIManagerScript>().numOfDiscoveredImportantPOI();
	}

	// Get the distance to the closest POI form 'position'
	public float getDistanceToClosestPOI(Vector3 position) {
		return POIManager.GetComponent<POIManagerScript>().distanceClosestPOI(position);
	}

}
