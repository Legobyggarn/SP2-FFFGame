using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class POIManagerScript : MonoBehaviour {

	// TODO: Change from "important POI" to "Prop"?

	// Public variables
	public GameObject soundAndMusic;
	// "Regular" points of interest and names
	[Header("Regular POI")]
	public List<GameObject> pointsOfInterest;
	public List<string> pointsOfInterestNames;
	// Important points of interest and names
	[Header("Important POI")]
	public List<GameObject> importantPointsOfInterest;
	public List<string> importantPointsOfInterestNames;
	public List<GameObject> POIClosedRooms;

	// Private variables
	private int numPOIAtStart;
	private int numImportantPOIAtStart;
	// Sound and music variable script?
	private SondAndMusic_Var soundAndMusicScript;


	// Use this for initialization
	void Start () {
		numPOIAtStart = pointsOfInterest.Count;
		numImportantPOIAtStart = importantPointsOfInterest.Count;

		if (soundAndMusic != null) {
			soundAndMusicScript = soundAndMusic.GetComponent<SondAndMusic_Var>();
		}


		// Error if # POIs and # POI names don't match
		// Remove because it is a Debug. ?
		/*
		if (pointsOfInterest.Count != pointsOfInterestNames.Count) {
			Debug.LogError("[POIManager] Error: Number of POIs and names don't match.");
		}

		if (importantPointsOfInterest.Count != importantPointsOfInterestNames.Count) {
			Debug.LogError("[POIManager] Error: Number of important POI and names don't match.");
		}
		*/
		// Debug.LogError


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Notify that POI (name) has been discovered
	// [CHANGE TO GAMEOBJECT PARAMETER?]
	public void POIDiscovered(GameObject go) {
		if (pointsOfInterest.Contains(go)) {
			// Find the correct name
			int nameIndex = pointsOfInterest.IndexOf(go);
			string name = pointsOfInterestNames[nameIndex];

			// Remove POI and name from their respective lists
			pointsOfInterest.Remove(go);
			pointsOfInterestNames.RemoveAt(nameIndex);

			// Notify the sound and music script (if musicAndSound is set)
			if (soundAndMusic != null) {
				soundAndMusicScript.POIDiscovered(name);
			}
		}
	}

	// Notify that important POI (name) has been discovered
	public void importantPOIDiscovered(GameObject go) {
		if (importantPointsOfInterest.Contains(go)) {
			// Find the correct name
			int nameIndex = importantPointsOfInterest.IndexOf(go);
			string name = importantPointsOfInterestNames[nameIndex];
			// Open up (unlock doors in) room for important POI
			if (POIClosedRooms.Count >= nameIndex) {
				POIClosedRooms [nameIndex].GetComponent<POIClosedRoom> ().unlockDoors ();
			}
			// Remove POI, name and room from their respective lists
			importantPointsOfInterest.Remove(go);
			importantPointsOfInterestNames.RemoveAt(nameIndex);
			if (POIClosedRooms.Count >= nameIndex) {
				POIClosedRooms.RemoveAt (nameIndex);
			}
			// Notify the sound and music script (if musicAndSound is set)
			if (soundAndMusic != null) {
				soundAndMusicScript.importantPOIDiscovered(name);
			}
		}
	}


	// Get the closest POI from 'position' (both regular and important POI)
	public float distanceClosestPOI(Vector3 position) {

		float shortDist = Mathf.Infinity; // Shortest distance

		// If no POI exist return -1
		if (pointsOfInterest.Count + importantPointsOfInterest.Count <= 0) {
			shortDist = -1f;
		} 

		else {
			// Loop through the POIs
			foreach (GameObject go in pointsOfInterest) {
				float distance = Vector3.Distance (position, go.transform.position);
				if (distance < shortDist) {
					shortDist = distance;
				}
			}
			// Loop through the important POIs
			foreach (GameObject go in importantPointsOfInterest) {
				float distance = Vector3.Distance (position, go.transform.position);
				if (distance < shortDist) {
					shortDist = distance;
				}
			}
		}

		return shortDist; // Return the shortest distance, or -1 if no POI exists
	}

	// Get amount of discovered points of interest
	public int numOfDiscoveredPOI() {
		return (numPOIAtStart - pointsOfInterest.Count);
	}

	// Get amount of discovered important points of interest
	public int numOfDiscoveredImportantPOI() {
		return numImportantPOIAtStart - importantPointsOfInterest.Count;
	}

}
