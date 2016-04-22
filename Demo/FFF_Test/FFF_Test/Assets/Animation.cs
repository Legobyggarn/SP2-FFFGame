using UnityEngine;
using System.Collections;

public class Animation : MonoBehaviour {
	Animator anim;
	int fireHash = Animator.StringToHash("New Trigger");
	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			anim.SetTrigger (fireHash);
		}
	}
}
