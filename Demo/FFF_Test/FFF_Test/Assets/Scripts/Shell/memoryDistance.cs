using UnityEngine;
using System.Collections;

public class memoryDistance : MonoBehaviour {

	//public
	public float coreFix; 
	public float speed = 5.0f;
	public float lerpTime = 5.0f;

	public bool firstShot = false; 

	public GameObject core;

	//following varr

	public float rotSpeed = 1;
	public float stepSpeed = 1;

	private Vector3 PlayerCordinates;
	private Vector3 lookDirection;

	//Private
	private float numberOfChildren;
	private float childCount;
	private float maximumNumberOfChildern;
	private float distanceCore;
	private float distanceShell;
	private float childProcent;

	private float coreDistanceScalar;

	private float sceneChangeTime = 0f;
	private float maxSceneChangeTime;

	public ScenesTransision st;

	private bool fading = true;

	private Vector3 lookPosition;

	private GameObject parentGo;
	private GameObject grandParentGo;
	private GameObject rootGo;

	//Lerp variabler



	private float currentLerpTime;
	private float moveDistance;

	private Vector3 startPos;
	private Vector3 endPos;

	// Use this for initialization
	void Start () 
	{
		parentGo = transform.parent.gameObject;
		grandParentGo = parentGo.transform.parent.gameObject;
		rootGo = transform.root.gameObject;

		maximumNumberOfChildern = transform.childCount;
		numberOfChildren = transform.childCount;
		childCount = transform.childCount;

		distanceCore = Vector3.Distance (rootGo.transform.position, transform.position);
		distanceShell = Vector3.Distance (rootGo.transform.position, core.transform.position);


		//So it dose not move beforethe shell is removed 
		startPos = transform.localPosition;
		endPos = transform.localPosition;

		// Set the core distance scalar
		coreDistanceScalar = GameObject.Find("GLOBAL_SCALE").GetComponent<GlobalScaleScript>().shellScale;

		// Scale up core
		core.transform.localScale *= coreDistanceScalar;

		maxSceneChangeTime = st.getLerpTime();

		lookDirection = transform.forward;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Debug.Log ("maximumNumberOfChildern    " + numberOfChildren);
		//Debug.Log ("numberOfChildren    " + maximumNumberOfChildern);
		//Debug.Log ("Child Procest    " + childProcent);

		// makes it stop in the orbit point 
		if (parentGo.transform.localPosition.z > -distanceCore) {

			//Debug.Log ("Lotuc position   " + parentGo.transform.localPosition.z);
			//Debug.Log ("Core position    " + distanceCore);

			currentLerpTime += Time.deltaTime;

			//Position in the lerp
			float prec = currentLerpTime / lerpTime;

			if (numberOfChildren != childCount) {

				currentLerpTime = 0;
				prec = currentLerpTime / lerpTime;

				numberOfChildren = childCount;

				//	Debug.Log ("Number Of Children:  " + numberOfChildren);


				childProcent = (numberOfChildren / maximumNumberOfChildern);

				startPos = parentGo.transform.localPosition;
				endPos = new Vector3 (0, 0, (distanceCore * childProcent) - distanceCore);
				//endPos = new Vector3 (0, 0, -(distanceCore * childProcent));

				// Change the position of the shell
				parentGo.transform.localPosition = Vector3.Lerp (startPos, endPos, prec);

				// Change the position of the core
				core.transform.localPosition = Vector3.Lerp (startPos, endPos, prec);
				core.transform.localPosition += new Vector3 (0, coreFix * coreDistanceScalar, 0);
		

				//Debug.Log("Prec:   " +prec);
			} else {
				numberOfChildren = childCount;
				//Debug.Log ("Number Of Children:  " + numberOfChildren);
				childProcent = (numberOfChildren / maximumNumberOfChildern);

				// Change the position of the shell
				parentGo.transform.localPosition = Vector3.Lerp (startPos, endPos, prec);

				// Change the position of the core
				core.transform.localPosition = Vector3.Lerp (startPos, endPos, prec);
				core.transform.localPosition += new Vector3 (0, coreFix * coreDistanceScalar, 0);

				//Debug.Log("Prec:   " +prec);
			}

			//Debug.Log ("Position    " + parentGo.transform.localPosition.z);
		} 

		if (childCount == 0) {	//The core is in the middle of the room 
			if (fading) {
				st.fadeToWhite ();
				fading = false;
			}

			sceneChangeTime += Time.deltaTime;

			if (maxSceneChangeTime < sceneChangeTime) { 
				win ();
			}
		}

		// Looking at the player

		//Only call if the shell has bean shot at 
		if (firstShot == true) 
		{
			followPlayerTest ();
	
		}
	}

	public void incrementNumChildren() 
	{

		childCount++;

		// Notify sound and music script that a shell chunk will start to merge
		GameObject.Find("Sound_and_Music_Var").GetComponent<SondAndMusic_Var>().orbitIncrease();

	}

	public void decrementNumChildren() 
	{

		childCount--;

		// Notify sound and music script that a shell chunk will start to merge
		GameObject.Find("Sound_and_Music_Var").GetComponent<SondAndMusic_Var>().orbitDecrease();

	}

	public void win()
	{
		// Notify sound and music script that a shell chunk will start to merge. [May have to be moved when more than one capsule is present in the level]
		GameObject.Find("Sound_and_Music_Var").GetComponent<SondAndMusic_Var>().coreAtCenter();

		Application.LoadLevel ("Victory");
	}

	// ...
	// Rename getNumShellParts?
	public float getChildCount() 
	{
		return childCount;
	}

	public bool isDone() 
	{
		return childCount <= 0;
	}

	public Vector3 getOrbitPoint() 
	{
		return transform.root.transform.position;
	}

	public void followPlayer()
	{
		lookDirection = transform.forward;
		lookPosition = transform.forward;

		PlayerCordinates = GameObject.Find("PlayerStandard 1").transform.position;
	
		Vector3 targetDir = PlayerCordinates - transform.position;
		float step = speed * Time.deltaTime;
	
		Vector3 newDir = Vector3.RotateTowards(lookPosition, targetDir, step, 0.0f);
	
		grandParentGo.transform.rotation = Quaternion.LookRotation(newDir);
	}

	public void followPlayerTest()
	{
		Quaternion targetRotation = Quaternion.LookRotation(PlayerCordinates = GameObject.Find("PlayerStandard 1").transform.position - transform.position);

		grandParentGo.transform.rotation = Quaternion.Slerp (grandParentGo.transform.rotation, targetRotation, rotSpeed * Time.deltaTime);

	}

	public void setLookPosition(Vector3 poss)
	{
		firstShot = true;
		Debug.Log ("lookPosistion set 2");
		lookPosition = poss;
	}
}
