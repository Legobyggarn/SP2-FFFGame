using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour 
{
	//public

	// button to start music
	public KeyCode play;
	// button to change music track
	public KeyCode changeTrack;
	// value for a parameter in FMOD
	public float paramValue;


	//Private

	// number of shells close to player
	private float shellNum = 0f;




	// FMOD declerations stuff
	[FMODUnity.EventRef]
	public string soundEvent;
	FMOD.Studio.EventInstance sound;



	// Use this for initialization
	void Start () 
	{	
		
		sound = FMODUnity.RuntimeManager.CreateInstance (soundEvent);

	}

	// add +1 to shellNum if a shell is close to player
	void ShellNumPlus()
	{
		shellNum++;
		//Debug.Log (shellNum);
	}

	// subtract -1 from shellNum when a shell is no longer considered close to player
	void ShellNumNeg()
	{
		shellNum--;
		//Debug.Log (shellNum);
	}

	// play sound function
	void PlaySound(float value)
	{
		sound.setParameterValue ("Music", value);
		sound.start ();
	}

	void ChangeParam()
	{
		paramValue = paramValue + 1f;
		if (paramValue >= 8f) 
		{
			paramValue = 0f;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		// starts the ChangeParam function
		if (Input.GetKeyDown (changeTrack) && changeTrack != null) 
		{ ChangeParam ();
			
		}

		// start the sound function and send along paramValue 
		if (Input.GetKeyDown (play) && play != null) 
		{
			PlaySound (paramValue);
		}
			

	
	}
		
}
