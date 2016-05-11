using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class POIManagerScript : MonoBehaviour {

	// TODO: Change from "important POI" to "Prop"?

	// Public variables
	public GameObject soundAndMusic;
	public List<GameObject> pointsOfInterest;
	public List<GameObject> importantPointsOfInterest;

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
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Notify that POI (name) has been discovered
	// [CHANGE TO GAMEOBJECT PARAMETER?]
	public void POIDiscovered(GameObject go) {
		if (pointsOfInterest.Contains(go)) {
			// Remove POI from the list
			pointsOfInterest.Remove(go);
			// Notify the sound and music script (if musicAndSound is set)
			if (soundAndMusic != null) {
				soundAndMusicScript.POIDiscovered();
			}
		}
	}

	// Notify that important POI (name) has been discovered
	public void importantPOIDiscovered(GameObject go) {
		if (importantPointsOfInterest.Contains(go)) {
			// Remove POI from the list...
			importantPointsOfInterest.Remove(go);
			// Notify the sound and music script (if musicAndSound is set)
			if (soundAndMusic != null) {
				soundAndMusicScript.importantPOIDiscovered();
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
