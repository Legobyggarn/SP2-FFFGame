using UnityEngine;
using System.Collections;

public class BulletColTest : MonoBehaviour {

    bool isHit;

	// Use this for initialization
	void Start () {

        isHit = false;

	}
	
	// Update is called once per frame
	void Update () {
	
        if (isHit) {
            isHit = false;
        }

	}

    //...
    void OnCollisionEnter(Collision collision) {

        if (!isHit) {
            isHit = true;

            //Destroy(collision.gameObject);

            //Debug.Log("Hit by :" + collision.gameObject.name);
        }
        

    }

}
