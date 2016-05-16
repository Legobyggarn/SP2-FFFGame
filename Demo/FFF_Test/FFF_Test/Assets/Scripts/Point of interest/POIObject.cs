using UnityEngine;
using System.Collections;

public class POIObject : MonoBehaviour {

	// Public variables
	//public string name;
	public bool isImportantPOI = false;

	// Private variables
	//...

	// POI will tell POI manager that it has been discovered
	public void POIDiscovered() {
		if (isImportantPOI) {
			GameObject.Find("POIManager").GetComponent<POIManagerScript>().importantPOIDiscovered(gameObject);
		} 
		else {
			GameObject.Find("POIManager").GetComponent<POIManagerScript>().POIDiscovered(gameObject);
		}
	}

	// When POI is destroyed
	void OnDestroy() {
		POIDiscovered();
	}

}
