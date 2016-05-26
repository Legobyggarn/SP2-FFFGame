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
    public bool Bars;
    public static float ClickTime;
    public Light spotLightExit;
    public Light spotLightPlay;
    public Light spotLightOption;
    public float minIntencity;
    public float maxIntencity;
    public float startIntencity;
    public Animator anim;
    private bool loadExit;
    private bool loadPlay;
    private bool loadOption;
    public ChangeTattooAlpha changeAlpha;
    public Transform trans;
    // Use this for initialization
    void Start () 
	{
        if (GameObject.Find("CenterEyeAnchor") != null && GameObject.Find("CenterEyeAnchor").activeSelf)
        {
          
            trans = GameObject.Find("CenterEyeAnchor").transform;
        }
        else trans = transform;
        anim = GameObject.Find("mainchar").GetComponent<Animator>();
        spotLightExit = GameObject.Find("SpotExit").GetComponent<Light>();
        spotLightPlay = GameObject.Find("SpotPlay").GetComponent<Light>();
        spotLightOption = GameObject.Find("SpotOption").GetComponent<Light>();
        InOptionNow = false;
        OptionScript = GameObject.Find("OptionsMenu").GetComponent<Options>();
        
     
        resetAlpha();
        if (Bars) { 
        GreenBarTransformPlay = GameObject.Find("GreenTransPlay").transform;
        GreenBarTransformExit = GameObject.Find("GreenTransExit").transform;
        GreenBarTransformOption = GameObject.Find("GreenTransOption").transform;
        }
        maxSceneChangeTime = 1f;
        maxSceneFadeTime = st.getLerpTime();
	}
    public bool found = false;
    private void getAlphaScript()
    {
        //Debug.Log(changeAlpha == GetComponentInChildren<ChangeTattooAlpha>());
        if(null != GetComponentInChildren<ChangeTattooAlpha>())
        if (found == false &&  changeAlpha != GetComponentInChildren<ChangeTattooAlpha>())
        {
            changeAlpha = GetComponentInChildren<ChangeTattooAlpha>();
            changeAlpha.setAlpha(0);
            found = true;
            //Debug.Log("PRINT");
        }
    }
    void Update()
    {
        getAlphaScript();
        
        //changeAlpha.setAlpha(0);

        if (FadeInText)
        {
            //  Debug.Log("Call Function Now " + FadeInText);
            FadeInAlpha(PlayMat);
            FadeInAlpha(ExitMat);
            FadeInAlpha(OptionMat);
            if (OptionMat.color.a>=1)
            {
                
                FadeInText = false;
                InOptionNow = false;
                loadingbarStop = false;
            }
        }
        else if (!InOptionNow)
        {
           
            lotsOffStuff();
        }
    }
    void resetAlpha()
    {
        PlayMat.SetColor("_Color", new Color(PlayMat.color.r, PlayMat.color.g, PlayMat.color.b, 1));
        ExitMat.SetColor("_Color", new Color(ExitMat.color.r, ExitMat.color.g, ExitMat.color.b, 1));
        OptionMat.SetColor("_Color", new Color(OptionMat.color.r, OptionMat.color.g, OptionMat.color.b, 1));
    }
    private void setBar(float f, Transform t)
    {
        if(Bars)
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
    }
    
    // Update is called once per frame

    public void GoBackFromOptions()
    {
        FadeInText = true;
        //  changeAlpha.setFadeOutTattoo();
        changeAlpha.setTargetAlpha(sceneChangeTime / maxSceneChangeTime);
        //  Debug.Log("FADE IN TEXT NOW " + FadeInText);
    }
    public void playAnimation()
    { 
       if (Input.GetAxis("Fire1") >= 0.2f && !loadingbarStop)
        {
          
            anim.SetBool("FireIsPressed", true);
        }
        else {
                   
            anim.SetBool("FireIsPressed", false);
            
            
        }
    }
  
    private void lotsOffStuff()
    {
        playAnimation();
        // Shoot            
        if (0.2f < Mathf.Abs(Input.GetAxis("Fire1")))
        { // Keyboard input (Use button instead?)
          // Shot
           
            // Raycast
            RaycastHit hit;
            // TODO Change transform to oculus
            if (Physics.Raycast(trans.position, trans.forward, out hit))
            {
                //Debug
                //	Debug.Log ("Debug: Object: [" + hit.transform.name + "] was hit");

                if (hit.transform.tag == "Play")
                {
                    setFadeInTatto();
                    loadPlay = true;
                    if(loadExit || loadOption)
                    {
                        loadExit = false;
                        loadOption = false;
                        resetVisual();
                    }
                    sceneChangeTime += Time.deltaTime;

                    float lerp = Mathf.PingPong(Time.time, duration) / duration;
                    go.GetComponent<Renderer>().material.color = Color.Lerp(colorStart, colorEnd, lerp);

                    setBar(sceneChangeTime, GreenBarTransformPlay);
           
                    spotIntens(sceneChangeTime, spotLightPlay);
                    
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
                    setFadeInTatto();
                    loadOption = true;
                    if (loadExit || loadPlay)
                    {
                        loadExit = false;
                        loadPlay = false;
                        resetVisual();
                    }
                    sceneChangeTime += Time.deltaTime;




                    setBar(sceneChangeTime, GreenBarTransformOption);
                  
                    spotIntens(sceneChangeTime, spotLightOption);
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
                    setFadeInTatto();
                    loadExit = true;
                    if (loadOption || loadPlay)
                    {
                        loadOption = false;
                        loadPlay = false;
                        resetVisual();
                    }
                    sceneChangeTime += Time.deltaTime;
                    setBar(sceneChangeTime, GreenBarTransformExit);
                    spotIntens(sceneChangeTime, spotLightExit);
                    
                    if (maxSceneChangeTime < sceneChangeTime)
                    {
                        loadingbarStop = true;
                       // Debug.Log("Exit now!");
                        Application.Quit();
                    }

                }
                else
                {
                    setFadeOutTatto();
                    resetVisual();

                }
            }
        }
        else
        {

            setFadeOutTatto();
            resetVisual();
        }
        if (onOption && FadeOut)
        {
            FadeAlpha(PlayMat);
            FadeAlpha(ExitMat);

            if (FadeAlpha(OptionMat))
            {
                InOptionNow = true;
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
    private void setFadeOutTatto()
    {
        if (!loadingbarStop)
        {
           // changeAlpha.setFadeOutTattoo();
            changeAlpha.setTargetAlpha(sceneChangeTime / maxSceneChangeTime);
        }
    }
    private void setFadeInTatto()
    {
        if(!loadingbarStop)
        {
            //changeAlpha.setFadeInTattoo();
            changeAlpha.setTargetAlpha(sceneChangeTime / maxSceneChangeTime);
        }
    }
    private void resetVisual()
    {
        sceneChangeTime = 0;
        setBar(sceneChangeTime, GreenBarTransformPlay);
        setBar(sceneChangeTime, GreenBarTransformExit);
        setBar(sceneChangeTime, GreenBarTransformOption);
        if (!loadingbarStop) { 
        spotLightExit.intensity = minIntencity;
        spotLightOption.intensity = minIntencity;
        spotLightPlay.intensity = minIntencity;
        }
    }
    public float alphaMult;
    private bool FadeAlpha(Material mat)

    {
       // Debug.Log("FADING NOW " + mat.name + " || alpha is " + mat.color.a);
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
        //Debug.Log("FADING In NOW " + mat.name + " || alpha is " + mat.color.a);
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
    public float test;
    public void spotIntens(float f, Light l)
    {
      
        float maxI = maxIntencity;
        float minI = minIntencity;
        float dif = maxI - minI;
        float mult = (f / maxSceneChangeTime);
     
        float difadd = mult * dif;
        float add = difadd + minI;
        l.intensity = f + test;
    }
   
  
}


