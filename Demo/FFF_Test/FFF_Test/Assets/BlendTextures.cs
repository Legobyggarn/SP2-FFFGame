using UnityEngine;
using System.Collections;

public class BlendTextures : MonoBehaviour {
    public Material material1;
    public Material material2;
    public float lerp;
    public Renderer rend;
	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        rend.material = material1;

    }

    // Update is called once per frame
    void Update () {
        rend.material.Lerp(material1, material2, lerp);
    }
}
