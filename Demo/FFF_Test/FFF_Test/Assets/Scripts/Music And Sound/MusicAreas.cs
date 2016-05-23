using UnityEngine;
using System.Collections;

public class MusicAreas : MonoBehaviour
{

    public float changeTo1;
    public float changeTo2;
    public float changeTo3;
    public string parameterToChange1;
    public string parameterToChange2;
    public string parameterToChange3;
    public MusicPlayer2 musicPlayer;


    // Use this for initialization
    void Start()
    {
        //Reference to the music Player
        GameObject musicPlayerObject = GameObject.Find("MusicPlayer");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //musicPlayer.PrintTest();
            //print("Player triggered");
            if (!string.IsNullOrEmpty(parameterToChange1))
            {
                musicPlayer.ChangeParameter("Music_Level", parameterToChange1, changeTo1);
            }

            if (!string.IsNullOrEmpty(parameterToChange2))
            {
                musicPlayer.ChangeParameter("Music_Level", parameterToChange2, changeTo2);
            }
            if (!string.IsNullOrEmpty(parameterToChange3))
            {
                musicPlayer.ChangeParameter("Music_Level", parameterToChange3, changeTo3);
            }
        }
    }
}