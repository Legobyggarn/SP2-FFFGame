using UnityEngine;
using System.Collections;
using FMODUnity;

public class ShellDestroySound : MonoBehaviour 
{

	//så man kan använda sig av fmodsfunktionalitet
	[FMODUnity.EventRef]
	public string SoundEvent;

	FMOD.Studio.EventInstance Sound;

	// Use this for initialization
	void Awake () 
	{


	}


	// Update is called once per frame
	void Update () 
	{

	}

	public void Play(Vector3 pos)
	{
		//gör så eventet faktiskt funkar
		Sound = FMODUnity.RuntimeManager.CreateInstance (SoundEvent);

		//sätter så eventet spelas ifrån objectet som detta script ligger på
		if (SoundEvent != null) 
		{
			Sound.set3DAttributes (RuntimeUtils.To3DAttributes (pos));
		}

		//startar eventet
		Sound.start();
	}
}
