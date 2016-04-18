using UnityEngine;
using System.Collections;
using FMODUnity;

public class PlaySoundAtObject : MonoBehaviour 
{
	
	//så man kan använda sig av fmodsfunktionalitet
	[FMODUnity.EventRef]
	public string SoundEvent;

	FMOD.Studio.EventInstance Sound;

	// Use this for initialization
	void Start () 
	{
		//gör så eventet faktiskt funkar
		Sound = FMODUnity.RuntimeManager.CreateInstance (SoundEvent);
		//startar eventet
		Sound.start();
	}
		
	
	// Update is called once per frame
	void Update () 
	{
		//sätter så eventet spelas ifrån objectet som detta script ligger på
		if (SoundEvent != null) 
		{
			Sound.set3DAttributes (RuntimeUtils.To3DAttributes (gameObject));
		}
	}
}
