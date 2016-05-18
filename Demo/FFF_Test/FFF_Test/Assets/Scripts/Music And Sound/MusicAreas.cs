using UnityEngine;
using System.Collections;

public class MusicAreas : MonoBehaviour
{

    public float areaNumber;
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
            musicPlayer.ChangeParameter("Music_Level", "Area", areaNumber);
            //musicPlayer.PrintTest();
            //print("Player triggered");
        }
    }
}