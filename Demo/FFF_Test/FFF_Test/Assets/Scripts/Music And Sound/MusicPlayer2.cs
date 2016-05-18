using UnityEngine;
using System.Collections;

public class MusicPlayer2 : MonoBehaviour 
{
    private SondAndMusic_Var soundAndMusic;

    public string eventMusic;
    public string eventVo;
    public string eventStinger1;
    public string eventStinger2;
    public string eventStinger3;
    private float areasCleared;
    private float capsuleDistanceMid;
    //private float areaIn;
    //float voPlay;

    //Bool som avgör om eventet ska startas direkt
    public bool startOnAwake = true;

    private Rigidbody cachedRigidBody;


    // FMOD declerations stuff
    [FMODUnity.EventRef]
	FMOD.Studio.EventInstance levelInstance;
    FMOD.Studio.EventInstance stingerInstance;
    //FMOD.Studio.EventInstance voInstance;
    FMOD.Studio.ParameterInstance paramInstance;



	// Use this for initialization
	void Start () 
	{	
		
		levelInstance = FMODUnity.RuntimeManager.CreateInstance (eventMusic);
        //voInstance = FMODUnity.RuntimeManager.CreateInstance(eventVo);
        //Starts Event if you choose "true"
        if (startOnAwake == true)
        {
            levelInstance.start();
            //voInstance.start();
        }

        //Reference to SoundAndMusic Variables
        GameObject soundAndMusicObject = GameObject.FindWithTag("Sound");
        soundAndMusic = soundAndMusicObject.GetComponent<SondAndMusic_Var>();

    }


    void Update ()
    {
        /* Lägg till dessa tre chunks när POI fungerar som de ska.
        //Byter "AreaClear"-parametern i "Music" eventet när ett viktigt prop förstörs. 
        areasCleared = soundAndMusic.getNumOfDiscoveredImportantPOI();
        ChangeParameter(eventMusic, "AreaClear", areasCleared);
        
        //Byter "CapsuleDistanceMid" parametern i "Music"-eventet när "bossen" kommer närmre mitten.
        capsuleDistanceMid = soundAndMusic.getDistanceToCenterCore(1);
        ChangeParameter(eventMusic, "CapsuleDistanceMid", capsuleDistanceMid);

        //När kapseln når mitten spelas stingern kopplat till det objektet. Blanda både 3D och 2D-event?
        if (capsuleDistanceMid == 0)
        {
            //Byt numret till stingern för capsule-destroyed.
            PlayStingerNumber(3);
            //PlayStingerNumber(13); //Använd denna för om du vill blanda flera olika ljud.
        }

        */

        //Gör ny Vo-lösning när POI fungerar! Eget script? (Se "upplägg av musik"-dokument).
        //Använd däremot enbart "MusicPlayer2-prefab" när rösten ska spelas i cutscenes etc. 
    }


	// play music function
	void PlayMusic()
	{
        levelInstance.start ();
        //sound.setParameterValue (paramNameOne, value);
    }


    //funktion för att ändra parametrar
    public void ChangeParameter(string eventName, string paramName, float value)
    {
        if (eventName == "Music_Level")
        {
            levelInstance.getParameter(paramName, out paramInstance);
            paramInstance.setValue(value);
        }

    }

    //Spelar upp den stingern man anger. Används i andra script (bl.a. triggers)
    public void PlayStingerNumber(int stingNum)
    {
        if (stingNum == 1)
        {
            stingerInstance = FMODUnity.RuntimeManager.CreateInstance(eventStinger1);
            stingerInstance.start();
        }
        if (stingNum == 2)
        {
            stingerInstance = FMODUnity.RuntimeManager.CreateInstance(eventStinger2);
            stingerInstance.start();
        }
        if (stingNum == 3)
        {
            stingerInstance = FMODUnity.RuntimeManager.CreateInstance(eventStinger3);
            stingerInstance.start();
        }
        //utöka när det behövs
    }

    public void PrintTest()
    {
        print("It's working alright!");
    }

}
