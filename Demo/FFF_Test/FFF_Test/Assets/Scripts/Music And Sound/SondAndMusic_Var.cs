using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
public class SondAndMusic_Var : MonoBehaviour {

	public GameObject player;
	//public GameObject capsule;
	public List <GameObject> coreList;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}


	//Get Functions for sound and music  

	public float getPlayerSpeed()
	{
		float temp = 0;
		return temp;
	}

	public Vector3 getDistanceToCapsule(int index)
	{
		Vector3 temp = new Vector3 (0f,0f,0f);
		return temp;
	}

	public float getReloadTime()
	{
		float temp = 0;
		return temp;
	}

	public float getNumberOfShellChunks(int index) // returnes the number off chunks still on the memory with index i
	{
		for(int i = 0; i < coreList.Count; i++)
		{
			if (i == index) 
			{
				return coreList[i].GetComponent<memoryDistance> ().getChildCount ();
			}
		}
		return 0;
	}

	public float getTotalNumberOfShellChunks() // returnes the number off chunks still on all memorys 
	{
		float numberOfShellChunks = 0f;

		for(int i = 0; i < coreList.Count; i++)
		{
			numberOfShellChunks += coreList[i].GetComponent<memoryDistance> ().getChildCount ();
		}

		return numberOfShellChunks;
	}

	public float getDistanceToCenter() // returnes the value of the palyers distance to the orbit point 
	{
		

		float temp = 0;
		return temp;
	}

	public Vector3 getDistanceToCenter_core() // returnes the value of the core with index i distance to the orbit point 
	{
		Vector3 playerPoss = player.transform.localPosition;
		Vector3 orbitPoss = coreList[0].GetComponent<memoryDistance> ().getOrbitPoint ();

		Vector3 temp = new Vector3 (0f,0f,0f);
		return temp;
	}

	public float getDoneCapsules()
	{


		float temp = 0;
		return temp;
	}

	public Vector3 getCapsulOrbitDistance(int index)

	{
		Vector3 temp = new Vector3 (0f,0f,0f);
		return temp;
	}

}
*/
