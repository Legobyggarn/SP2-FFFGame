using UnityEngine;
using System.Collections;

public class POITestScript : MonoBehaviour {

	// Public variables
	public GameObject player;

	// Private variables
	private POIManagerScript POIManager;

	// Use this for initialization
	void Start () {

		POIManager = GameObject.Find("POIManager").GetComponent<POIManagerScript>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown(KeyCode.R)) {
			runTest();
			Debug.Break();
		}

	}

	private void runTest() {

		// TODO: For new test, some of the methods have changed names. Look up the new names.

		/*
		string result = "";

		// Shortest distance
		result += "Distance: " + POIManager.closestPOI(player.transform.position) + "\n";

		// Destroyed POI
		result += "Destroyed POIs: " + POIManager.numOfDestroyedPOI() + "\n";

		// Destroyed POI percentage
		result += "Destroyed POIs (perc): " + (POIManager.numOfDestroyedPOIPerc()*100f) + "%\n";

		// Destroyed important POI
		result += "Destroyed important POIs: " + POIManager.numOfDestroyedImportantPOI() + "\n";

		// Destroyed important POI percentage
		result += "Destroyed important POIs (perc): " + (POIManager.numOfDestroyedImportantPOIPerc()*100f) + "%\n";

		Debug.Log (result);
		*/

	}

}
