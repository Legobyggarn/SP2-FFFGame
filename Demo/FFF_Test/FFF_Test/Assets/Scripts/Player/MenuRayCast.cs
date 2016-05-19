using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MenuRayCast : MonoBehaviour {

	//Public 

	//public GameObject laserSpawn;
	//public GameObject laserPrefab;
	public Color colorStart = Color.red;
	public Color colorEnd = Color.green;
	public float duration = 1.0f; 

	public ScenesTransision st;

	public GameObject go;

	// Private
	private float timeSinceLastShot;
	public float maxSceneChangeTime;
	private float sceneChangeTime;
	private float maxSceneFadeTime;
	private float sceneFadeTime;

	private bool onPlay = false;
	private bool onOption = false;
	private bool onExit = false;
	private bool fading = true; 
	private bool noNeedToPress = false;
    public Material PlayMat;
    public Material ExitMat;
    public Material OptionMat;
    private Transform GreenBarTransformPlay;
    private Transform GreenBarTransformExit;
    private Transform GreenBarTransformOption;
    private bool loadingbarStop;
    private Options OptionScript;
    private bool InOptionNow;
    private bool FadeInText;
    private bool FadeOut;
    // Use this for initialization
    void Start () 
	{
        InOptionNow = false;
        OptionScript = GameObject.Find("OptionsMenu").GetComponent<Options>();
        resetAlpha();
        GreenBarTransformPlay = GameObject.Find("GreenTransPlay").transform;
        GreenBarTransformExit = GameObject.Find("GreenTransExit").transform;
        GreenBarTransformOption = GameObject.Find("GreenTransOption").transform;
        maxSceneChangeTime = 0.5f;
        maxSceneFadeTime = st.getLerpTime();
	}
    void resetAlpha()
    {
        PlayMat.SetColor("_Color", new Color(PlayMat.color.r, PlayMat.color.g, PlayMat.color.b, 1));
        ExitMat.SetColor("_Color", new Color(ExitMat.color.r, ExitMat.color.g, ExitMat.color.b, 1));
        OptionMat.SetColor("_Color", new Color(OptionMat.color.r, OptionMat.color.g, OptionMat.color.b, 1));
    }
    private void setBar(float f, Transform t)
    {
        if (!loadingbarStop) { 
        float maxhigt = 1.66f;
        float minhight = -0.11f;
        float dif = maxhigt - minhight;
        float mult = (f / maxSceneChangeTime);
        float difadd = mult * dif;
        float y = difadd + minhight;
     //   Debug.Log("f is " +f  +" || mult " + mult + " || dif " + dif + " || difadd " + difadd + " || y is " + y);
        t.position = new Vector3(t.position.x, y, t.position.z);
      //  Debug.Log("greenbar vector is " + GreenBarTransformPlay.position);
        }
    }
	// Update is called once per frame
	void Update () 
	{
        if(!InOptionNow)
        {
            lotsOffStuff();
        }
		if(FadeInText)
        {
            Debug.Log("Call Function Now " + FadeInText);
            FadeInAlpha(PlayMat);
            FadeInAlpha(ExitMat);
            if (FadeInAlpha(OptionMat))
            {
                FadeInText = false;
                InOptionNow = false;
            }
        }
	}
    public void GoBackFromOptions()
    {
        FadeInText = true;
        loadingbarStop = false;
        Debug.Log("FADE IN TEXT NOW " + FadeInText);
    }
    private void lotsOffStuff()
    {
        // Shoot            
        if (0.2f < Mathf.Abs(Input.GetAxis("Fire1")))
        { // Keyboard input (Use button instead?)
          // Shot

            // Raycast
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                //Debug
                //	Debug.Log ("Debug: Object: [" + hit.transform.name + "] was hit");

                if (hit.transform.tag == "Play")
                {
                    sceneChangeTime += Time.deltaTime;

                    float lerp = Mathf.PingPong(Time.time, duration) / duration;
                    go.GetComponent<Renderer>().material.color = Color.Lerp(colorStart, colorEnd, lerp);

                    setBar(sceneChangeTime, GreenBarTransformPlay);
                    if (maxSceneChangeTime < sceneChangeTime)
                    {
                        //Debug.Log ("Fading");
                        if (fading)
                        {
                            st.fadeToWhite();
                            fading = false;
                            noNeedToPress = true;
                            loadingbarStop = true;

                        }
                    }
                }

                else if (hit.transform.tag == "Options")
                {
                    sceneChangeTime += Time.deltaTime;




                    setBar(sceneChangeTime, GreenBarTransformOption);
                    if (maxSceneChangeTime < sceneChangeTime)
                    {
                        onOption = true;

                        loadingbarStop = true;
                        // Fade AwayText
                        FadeOut = true;

                    }
                }

                else if (hit.transform.tag == "Exit")
                {
                    sceneChangeTime += Time.deltaTime;
                    setBar(sceneChangeTime, GreenBarTransformExit);
                    if (maxSceneChangeTime < sceneChangeTime)
                    {
                        loadingbarStop = true;
                        Debug.Log("Exit now!");
                        Application.Quit();
                    }

                }
                else
                {

                    sceneChangeTime = 0;
                    setBar(sceneChangeTime, GreenBarTransformPlay);
                    setBar(sceneChangeTime, GreenBarTransformExit);
                    setBar(sceneChangeTime, GreenBarTransformOption);

                }
            }
        }
        else
        {

            sceneChangeTime = 0;
            setBar(sceneChangeTime, GreenBarTransformPlay);
            setBar(sceneChangeTime, GreenBarTransformExit);
            setBar(sceneChangeTime, GreenBarTransformOption);
        }
        if (onOption && FadeOut)
        {
            FadeAlpha(PlayMat);
            FadeAlpha(ExitMat);

            if (FadeAlpha(OptionMat))
            {
                OptionScript.OptionsOn();
                FadeOut = false;
            }
        }
        if (noNeedToPress)
        {
            sceneFadeTime += Time.deltaTime;
            if (maxSceneFadeTime < sceneFadeTime)
            {
                //   Debug.Log("GO TO LOADING SCREEN!");
                Application.LoadLevel("Loading_screen");
            }
        }
    }
    public float alphaMult;
    private bool FadeAlpha(Material mat)

    {
        Debug.Log("FADING NOW " + mat.name + " || alpha is " + mat.color.a);
        float alpha = mat.color.a;
        if (alpha <= 0) return true;
        else
        {
            alpha -= alphaMult * Time.deltaTime;
            if (alpha <= 0) alpha = 0;
            mat.SetColor("_Color",new Color(mat.color.r, mat.color.g,mat.color.b,alpha));
            
        }
        return false;
    }
    private bool FadeInAlpha(Material mat)

    {
        Debug.Log("FADING In NOW " + mat.name + " || alpha is " + mat.color.a);
        float alpha = mat.color.a;
        if (alpha >= 1) return true;
        else
        {
            alpha += alphaMult * Time.deltaTime;
            if (alpha >= 1) alpha = 1;
            mat.SetColor("_Color", new Color(mat.color.r, mat.color.g, mat.color.b, alpha));
            
        }
        return false;
    }
}


