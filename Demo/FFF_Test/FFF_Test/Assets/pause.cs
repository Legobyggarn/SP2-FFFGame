using UnityEngine;
using System.Collections;

public class pause : MonoBehaviour {

	public bool isPaused;
	public bool showGUI;

	public GameObject childGO;
	// Use this for initialization
	void Start () 
	{
		isPaused = false;
		showGUI = false;
		childGO = transform.GetChild (0).gameObject;
		childGO.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown ("space")) 
		{
			Debug.Log ("Pess");

			if (isPaused) 
			{
				isPaused = false;
			} 
			else 
			{
				isPaused = true;
			}
		}

		if (isPaused) 
		{
			Time.timeScale = 0;
			Screen.lockCursor = true;
			showGUI = true;
		}

		if (!isPaused) 
		{
			Time.timeScale = 1;
			Screen.lockCursor = false;
			showGUI = false;
		}

		if (showGUI) 
		{
			childGO.SetActive (true);
			Debug.Log ("Set Active");
		}
		else
		{

			childGO.SetActive(false);
			Debug.Log ("Set inActive");
		}
	}
}
