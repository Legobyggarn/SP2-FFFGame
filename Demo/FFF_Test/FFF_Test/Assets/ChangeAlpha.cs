using UnityEngine;
using System.Collections;

public class ChangeAlpha : MonoBehaviour {
    public Renderer rend;
    public float alpha;
    public Texture TattooTex;
    public Texture NoTattooTex;
    public bool SetTattooTex;
    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        //changeTexture();
        setAlpha();

    }
    public void setAlpha()
    {
        Material[] mat = rend.materials;
        mat[1].color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, alpha);
    }
    public void changeTexture()
    {
        if(SetTattooTex)
        { 
        rend.material.SetTexture("_MainTex", TattooTex);

        }
        else
        {
            rend.material.SetTexture("_MainTex", NoTattooTex);
        }

    }
}
