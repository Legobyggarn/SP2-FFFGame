using UnityEngine;
using System.Collections;
using FMODUnity;

public class PlaySoundOnButton : MonoBehaviour {
	//om man vill stoppa ljudet eller inte
	public bool stop;
	//avgör väntetiden innan eventet avbryts
	public float waitTime;
	//avgör om eventet får ha en fadeout eller inte
	public bool allowFadeOut = true;
	//knappen för att spela upp eventet
	public KeyCode button;

	public float paramValue;


	[FMODUnity.EventRef]
	public string SoundEvent;
	FMOD.Studio.ParameterInstance paramInstance;
	FMOD.Studio.EventInstance Sound;


	// Use this for initialization
	void Start () 
	{
		Sound = FMODUnity.RuntimeManager.CreateInstance (SoundEvent);


	}
	
	// Update is called once per frame
	void Update () 
	{
		//spelar upp eventet om rätt knappt är nedtryckt
		if (Input.GetKeyDown (button)) 
		{

			Sound.setParameterValue ("Concept_X",paramValue);;
			Sound.start ();
			if (stop == true) 
			{
				StartCoroutine (Wait (waitTime));
			}

		}
	
	}

	//gör så det väntar waitTime sekunder innan eventet stopas om det får stoppas
	IEnumerator Wait(float x)
	{
		if (allowFadeOut == false) {
			yield return new WaitForSeconds (x);
			Sound.stop (FMOD.Studio.STOP_MODE.IMMEDIATE);
		} 
		else
		{
			yield return new WaitForSeconds (x);
			Sound.stop (FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		}
	}
}
