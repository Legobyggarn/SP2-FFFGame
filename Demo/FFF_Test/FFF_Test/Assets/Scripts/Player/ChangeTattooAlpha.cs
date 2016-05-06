using UnityEngine;
using System.Collections;

public class ChangeTattooAlpha : MonoBehaviour {
    public float testalpha;
    public Renderer rend;
    public float fadeInSpeed;
    public float fadeOutSpeed;
    public float alphaValue;
    public bool fadeOut;
    public bool fadeIn;
    public bool TestingModeSet;
    Material[] mat;
    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        alphaValue = 1;
        mat = rend.materials;
        
    }
	
	// Update is called once per frame
	void Update () {
        if (TestingModeSet) { 
        setAlpha();
        }
        if (fadeOut)
        {
            fadeOutTattoo();
        }
        else if(fadeIn)
        {
            fadeInTattoo();
        }
    }   
    public void setFadeOutTattoo()
    {
        fadeOut = true;
        fadeIn = false; 
    }
    public void setFadeInTattoo()
    {
        fadeOut = false;
        fadeIn = true;
    }
    private void fadeOutTattoo()
    {
        if (alphaValue > 0)
        {
            alphaValue -= Time.deltaTime * fadeOutSpeed;
        }
        else
        {
            alphaValue = 0;
            fadeOut = false;
        }
        
        mat[1].color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, alphaValue);
       
    }
    private void fadeInTattoo()
    {
        if (alphaValue < 1)
        {
            alphaValue += Time.deltaTime * fadeInSpeed;
        }
        else
        {
            alphaValue = 1;
            fadeIn = false;
        }
      
        mat[1].color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, alphaValue);
        
    }

    public void setAlpha()
    {
        Material[] mat = rend.materials;
        mat[1].color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, testalpha);
      //  rend.materials = mat;
    }
}
