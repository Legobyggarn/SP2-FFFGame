using UnityEngine;
using System.Collections;


public class TestParticleStartSize : MonoBehaviour {
    public float size;
    public ParticleSystem ps;
    // Use this for initialization
    void Start () {
        ps = GameObject.Find("cloud").GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        ps = GameObject.Find("cloud").GetComponent<ParticleSystem>();
        ps.startSize = size;
	}
}
