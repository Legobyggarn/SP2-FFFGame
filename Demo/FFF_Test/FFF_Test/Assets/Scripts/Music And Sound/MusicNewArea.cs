using UnityEngine;
using System.Collections;

public class MusicNewArea : MonoBehaviour {

    //public float areaNumber;
    public MusicPlayer2 musicPlayer;
    public int stingerNumber;


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
            //musicPlayer.ChangeParameter("Music_Level", "AreaNew", areaNumber);
            //musicPlayer.PrintTest();
            musicPlayer.PlayStingerNumber(stingerNumber);
            //print("Player triggered Stinger");
            Destroy(gameObject);
        }
    }
}
