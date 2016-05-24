using UnityEngine;
using System.Collections;

public class Tesr : MonoBehaviour {
    public Material m;
    public float f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        m.renderQueue = (int)f;
        Debug.Log("Hello " + m.renderQueue);
	}
}
