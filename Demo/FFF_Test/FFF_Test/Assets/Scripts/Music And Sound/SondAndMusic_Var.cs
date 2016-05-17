using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SondAndMusic_Var : MonoBehaviour {

	// TODO: Have two seperate lists, one for cores and one for shells
	// TODO: Add protection against invalid indexes in methods using an index for looking up a core

	public GameObject player;
	public List <GameObject> coreList;
	public Transform orbitsCenterTransform;
	private Vector3 orbitsCenter;

	// Use this for initialization
	void Start () 
	{
		orbitsCenter = orbitsCenterTransform.position;
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
				return coreList[i].GetComponent<memoryDistance>().getChildCount();
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
			numberOfShellParts += coreList[i].GetComponent<memoryDistance>().getChildCount();
		}

		return numberOfShellParts;
	}

	// Returns the value of the players distance to the orbit point 
	public float getDistanceToCenter() 
	{
		Vector3 distanceVector = orbitsCenter - player.transform.position;
		float distanceToCenter = distanceVector.magnitude;
		//float temp = 0;
		return distanceToCenter;
	}

	// Returns the value of the core with index i distance to the orbit point.
	// return float?
	public float getDistanceToCenterCore(int index)
	{

		Vector3 capsulePos = coreList[index].transform.position;
		Vector3 orbitPos = coreList[index].GetComponent<memoryDistance>().getOrbitPoint();
		Vector3 distVec = orbitPos - capsulePos;
		float distance = distVec.magnitude;
		return distance;

	}

	// Get the amount of capsules which shell has been destroyed. [Should return an int]
	public float getDoneCapsules() 
	{
		int numDoneCapsules = 0;
		foreach (GameObject core in coreList) {
			if (core.GetComponent<memoryDistance>().isDone()) {
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
	public void shellChunkDestroyed() {
		// Play music/sound or call another funtion...

		// Debug
		//Debug.Log("Shell chunk was destroyed");
	}

	// Shell chunk "merges" with core again, called when merging starts
	// [Called from shell handler when a shell part is "re-added"]
	public void shellChunkMergeWithCore() {
		// Play music/sound or call another funtion...

		// Debug
		//Debug.Log("Shell chunk merged with core");
	}

	// Orbit increases
	// [Called from memoryDistance when number of children are incremented]
	public void orbitIncrease() {
		// Play music/sound or call another funtion...

		// Debug
		//Debug.Log("Orbit increased");
	}

	// Orbit decreases
	// [Called from memoryDistance when number of children are decremented]
	public void orbitDecrease() {
		// Play music/sound or call another funtion...

		// Debug
		//Debug.Log("Orbit decreased");
	}

	// Core arrives at center
	// [Called from memoryDistance when core reaches the center]
	public void coreAtCenter() {
		// Play music/sound or call another funtion...

		// Debug
		//Debug.Log("Core is at center");
	}

	// Bullet object gets destroyed (not called anywhere yet)
	// [Called from a bullet script just before the bullet is destroyed]
	public void bulletDestroyed() {
		// Play music/sound or call another funtion...

		// Debug
		//Debug.Log("A bullet was destroyed");
	}

}
