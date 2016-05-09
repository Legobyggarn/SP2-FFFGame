using UnityEngine;
using System.Collections;

public class LookTowardPlayer : MonoBehaviour {


    // Rotation
    public float rotSpeed = 1;
    public float MagnitudeDelta = 1;
    public float speed = 1;
    // Movement

    private Vector3 PlayerCordinates;
    private Vector3 lookDirection;

    // Use this for initialization
    public bool debugMode;      // Set true if you want to print debugging information
    void Start () {
        // Rotation

        lookDirection = transform.forward;
    }
	
	// Update is called once per frame
	void Update () {

     
        lookDirection = transform.forward;
        PlayerCordinates = GameObject.Find("PlayerStandard 1").transform.position;
        Vector3 targetDir = PlayerCordinates - transform.position;
        float step = speed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(lookDirection, targetDir, step, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

 
}
