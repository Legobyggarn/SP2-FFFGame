using UnityEngine;
using System.Collections;

public class pivoPointRotation : MonoBehaviour {

	//public 
	public Material lotucMat;
	private Color coloTransparent = new Color (0, 0, 0, 0);
	private Color lotucColor;

	public float coreFix; 
	public float speed = 5.0f;
	public float lerpTime = 5.0f;
	public float lerpTimeBall = 4.0f;
	public float shakeFix = 50.0f;

	public GameObject lotucGo;

	public Vector3 maximumScaleBall;

	public ScenesTransision st;

	public bool firstShot = false; 
	public bool followPlayerB = true; 

	public ParticleSystem particelEffect;

	public float timeToFade = 15.0f;
	public float currTimeToFade = 0.0f;

	//Lerp variabler
	private float currentLerpTime;
	private float currentLerpTimeBall;
	private float moveDistance;
	private Vector3 ballStartScale;

	private Vector3 startPos;
	private Vector3 endPos;
	private Vector3 LocusEndSize; 

	public float rotSpeed = 1;

	//Private
	private GameObject rootGo;


	private float precBall;

	private Vector3 PlayerCordinates;

	private bool fading = true;

	//Screenchange var
	private float sceneChangeTime = 0.0f;
	public float maxSceneChangeTime = 5.0f;

	//child var
	private float maximumNumberOfChildern;
	private float childCount;
	private float numberOfChildren;
	private float childProcent;

	private float coreDistanceScalar;
	private float distanceToMid;

	// Use this for initialization
	void Start () 
	{
		maximumNumberOfChildern = transform.GetChild(1).GetChild(0).transform.childCount;
		numberOfChildren = transform.GetChild(1).GetChild(0).transform.childCount;
		childCount = transform.GetChild(1).GetChild(0).transform.childCount;	

		startPos = transform.localPosition;
		endPos = transform.localPosition;

		rootGo = transform.root.gameObject;
		lotucGo.SetActive (false);

		distanceToMid = Vector3.Distance (Vector3.zero, transform.localPosition);

		coreDistanceScalar = GameObject.Find("GLOBAL_SCALE").GetComponent<GlobalScaleScript>().shellScale;
		transform.localScale *= coreDistanceScalar;

		ballStartScale = particelEffect.transform.localScale;
		LocusEndSize = lotucGo.transform.localScale;
		Debug.Log ("Lotus size: " + LocusEndSize);

		lotucColor = lotucMat.color;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (transform.localPosition.z > 0) {

			//Debug.Log ("In Rotation 1");

			currentLerpTime += Time.deltaTime;

			//Position in the lerp
			float prec = currentLerpTime / lerpTime;

			if (numberOfChildren != childCount) {

				//Debug.Log ("In new lerp");

				currentLerpTime = 0;
				prec = currentLerpTime / lerpTime;


				numberOfChildren = childCount;

				childProcent = (numberOfChildren / maximumNumberOfChildern);


				//Debug.Log (childProcent + "       childProcent");


				startPos = transform.localPosition;
				endPos = new Vector3 (0, 0, (distanceToMid * childProcent));

				//Debug.Log ("distanceToMid   "  + distanceToMid);

				transform.localPosition = Vector3.Lerp (startPos, endPos, prec);
				//transform.localPosition += new Vector3 (0, coreFix * coreDistanceScalar, 0);

			} else {		
				//Debug.Log ("In old lerp");
				transform.localPosition = Vector3.Lerp (startPos, endPos, prec);
				//transform.localPosition += new Vector3 (0, coreFix * coreDistanceScalar, 0);

			}
		}

		else {

			Debug.Log ("Expanding");

			//Expanding the partical system and spawning in the lotus s
			expandingBall ();

			currTimeToFade += Time.deltaTime;

			if(timeToFade < currTimeToFade ){
				
				if (fading) {
					st.fadeToWhite ();
					fading = false;
				}

				sceneChangeTime += Time.deltaTime;

				if (st.getLerpTime() < sceneChangeTime) { 
					win ();
				}
			}
		}

		if (followPlayerB) {
			followPlayer ();
		}

		if (Input.GetKey (KeyCode.P)) {

			expandingBall ();

			currTimeToFade += Time.deltaTime;

			if(timeToFade < currTimeToFade ){

				if (fading) {
					st.fadeToWhite ();
					fading = false;
				}

				sceneChangeTime += Time.deltaTime;

				if (st.getLerpTime() < sceneChangeTime) { 
					win ();
				}
			}
		}
	}

	public void incrementNumChildren() 
	{
		//Debug.Log ("Increment");
		childCount++;

		// Notify sound and music script that a shell chunk will start to merge
		GameObject.Find("Sound_and_Music_Var").GetComponent<SondAndMusic_Var>().orbitIncrease();

	}

	public void decrementNumChildren() 
	{
		//Debug.Log ("decrementNumChildren");
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

		float distanceToPlayer = Vector3.Distance (transform.position, GameObject.FindWithTag("Player").transform.position);

		if(distanceToPlayer > shakeFix){
			Quaternion targetRotation = Quaternion.LookRotation(GameObject.FindWithTag("Player").transform.position - transform.position);

			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
		}
	}

	public void expandingBall()
	{
		currentLerpTimeBall += Time.deltaTime;
		precBall = currentLerpTimeBall / lerpTimeBall;

		if (currentLerpTimeBall > lerpTimeBall) {

			followPlayerB = false;


		} else {

			lotucGo.SetActive (true);

			transform.rotation = Quaternion.identity; // Set rotation to 0,0,0


			particelEffect.GetComponent<SphereCollider>().enabled = false;  
			particelEffect.emissionRate -= 1.0f;

			particelEffect.transform.localScale = Vector3.Lerp (ballStartScale, maximumScaleBall, precBall);
			lotucGo.transform.localScale = Vector3.Lerp (Vector3.zero, LocusEndSize, precBall);
			lotucMat.color = Color.Lerp (coloTransparent, lotucColor, precBall);
		}
	}
}
