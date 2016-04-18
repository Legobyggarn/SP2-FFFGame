using UnityEngine;
using System.Collections;

public class memoryDistance : MonoBehaviour {

	//public
	public float coreFix; 
	public float speed = 1.0f;
	public float lerpTime = 5.0f;

	public GameObject core;

	//Private
	private float numberOfChildren;
	private float childCount;
	private float maximumNumberOfChildern;
	private float distanceCore;
	private float distanceShell;
	private float childProcent;


	private GameObject parentGo;
	private GameObject grandParentGo;
	private GameObject rootGo;

	//Lerp variabler



	private float currentLerpTime;
	private float moveDistance;

	private Vector3 startPos;
	private Vector3 endPos;

	// Use this for initialization
	void Start () {
		parentGo = transform.parent.gameObject;
		grandParentGo = transform.parent.gameObject;
		rootGo = transform.root.gameObject;

		maximumNumberOfChildern = transform.childCount;
		numberOfChildren = transform.childCount;
		childCount = transform.childCount;

		distanceCore = Vector3.Distance (rootGo.transform.position, transform.position);
		distanceShell = Vector3.Distance (rootGo.transform.position, core.transform.position);


		//So it dose not move beforethe shell is removed 
		startPos = transform.localPosition;
		endPos = transform.localPosition;

	}
	
	// Update is called once per frame
	void Update () {



		//Debug.Log ("maximumNumberOfChildern    " + numberOfChildren);
		//Debug.Log ("numberOfChildren    " + maximumNumberOfChildern);
		//Debug.Log ("Child Procest    " + childProcent);

		// makes it stop in the orbit point 
		if (parentGo.transform.localPosition.z > -distanceCore)
		{

		currentLerpTime += Time.deltaTime;

		//Position in the lerp
		float prec = currentLerpTime / lerpTime;

			if (numberOfChildren != childCount) 
			{

				currentLerpTime = 0;
				prec = currentLerpTime / lerpTime;

				numberOfChildren = childCount;
				childProcent = (numberOfChildren / maximumNumberOfChildern);

				startPos = parentGo.transform.localPosition;
				endPos = new Vector3 (0, 0, (distanceCore * childProcent) - distanceCore);

				// Change the position of the shell
				parentGo.transform.localPosition = Vector3.Lerp (startPos, endPos, prec);

				// Change the position of the core
				core.transform.localPosition = Vector3.Lerp (startPos, endPos, prec);
				core.transform.localPosition += new Vector3 (0, coreFix, 0);
		

				Debug.Log("Prec:   " +prec);
			}
			else 
			{
				numberOfChildren = childCount;
				childProcent = (numberOfChildren / maximumNumberOfChildern);

				// Change the position of the shell
				parentGo.transform.localPosition = Vector3.Lerp (startPos, endPos, prec);

				// Change the position of the core
				core.transform.localPosition = Vector3.Lerp (startPos, endPos, prec);
				core.transform.localPosition += new Vector3 (0, coreFix, 0);

				//Debug.Log("Prec:   " +prec);
			}

			//Debug.Log ("Position    " + parentGo.transform.localPosition.z);
		}
	}

	public void incrementNumChildren() 
	{

		childCount++;

	}

	public void decrementNumChildren() 
	{

		childCount--;

	}

}
