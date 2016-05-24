using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


public class Options : MonoBehaviour {
    public struct TextInfo
    {
        public TextMesh textMesh;
        public int Textlength;

    }

    private MenuRayCast otherScript;
    public Color StandardColor;
    public Color TargetColor;
    // Private members (TODO)
    public int SpaceSpaceSpace;
    public int MasterSoundVolume;
    public int SoundVolume;
    public int MusicVolume;
    public int VideoResolution_X;
    public int VideoResolution_Y;
    public bool FullScreenMode;
    public int QualitySetting;
    public float TestAlpha;
    private static float ClickTime;
    public float pressTimer;
    string[] QualityStrings = { "Fastest", "Fast", "Simple", "Good", "Beautiful", "Fantastic" };
  
    public bool cancel;
    public bool OptionOn;
    public Material OptionMaterial;
    public int currentOption;

 
    public TextInfo MasterSoundInfo;
    public TextInfo Musicinfo;
    public TextInfo SoundInfo;
    public TextInfo Resolution_X_Info;
    public TextInfo Resolution_Y_Info;
    public TextInfo FullScreenInfo;
    public TextInfo QualityInfo;
    public TextInfo ApplyInfo;
    public TextInfo ResetInfo;
    public TextInfo BackInfo;
    public List<TextInfo> OptionList;
    public TextMesh[] allTexts;
    public Material standardMat;
    public Material glowMat;
    private int[] X_res = {640, 1024, 1280, 1600, 1920 };
    private int[] Y_res = {480, 576, 720, 900, 1080};
    private struct InputHandler
    {
        private static bool upIsPressed;
        private static bool downIsPressed;
        private static bool leftIsPressed;
        private static bool rightIsPressed;
        private static bool enterIsPressed;
        private static bool backIsPressed;
        private static float coolDown;

        private static bool EnterReseted;
        private static bool BackReseted;

        public static void CheckInput()
        {
            if(Input.GetAxis("EnterAndBack") == 1)
            {
                resetInput();
                enterIsPressed = true;
                BackReseted = true;
            }
            else if(Input.GetAxis("EnterAndBack") == -1)
            {
                resetInput();
                backIsPressed = true;
                EnterReseted = true;
            }
            else if (Input.GetAxis("DpadUpDown") == 1)
            {
                resetInput();
                upIsPressed = true;
                
            }
            else if (Input.GetAxis("DpadUpDown") == -1)
            {
                resetInput();
                downIsPressed = true;
                
            }
            else if (Input.GetAxis("DpadLeftRight") == 1)
            {
                resetInput();
                rightIsPressed = true;
            }
            else if (Input.GetAxis("DpadLeftRight") == -1)
            {
                resetInput();
                leftIsPressed = true;
                
            }
            else
            {
                resetInput();
                EnterReseted = true;
                BackReseted = true;
                coolDown = 0;
            }

       //     Debug.Log("Up is " + upIsPressed + " || down is " + downIsPressed + " || right is " + rightIsPressed + " || left is " + leftIsPressed);
        }
        private static void resetInput()
        {
            upIsPressed = false;
            downIsPressed = false;
            leftIsPressed = false;
            rightIsPressed = false;
            enterIsPressed = false;
            backIsPressed = false;

        }
        public static bool getEnter()
        {
            if(enterIsPressed && EnterReseted)         
            {
                EnterReseted = false;
                return true;
            }

            return false;
        }
        public static bool getBack()
        {
            if (backIsPressed && BackReseted)
            {
                BackReseted = false;
                return true;
            }

            return false;
        }
        public static bool getLeft()
        {
            return leftIsPressed;
        }
        public static bool getRight()
        {
            return rightIsPressed;
        }
        public static bool getDown()
        {
            return downIsPressed;
        }
        public static bool getUp()
        {
            return upIsPressed;
        }
        public static bool getClickLeft()
        {
            if (leftIsPressed && ClickCoolDown())
            {
                return true;
            }
            return false;
        }
        public static bool getClickRight()
        {
            if (rightIsPressed && ClickCoolDown())
            {
                return true;
            }
            return false;
        }
        public static bool getClickUp()
        {
            if (upIsPressed && ClickCoolDown())
            {
                return true;
            }
            return false;
        }
        public static bool getClickDown()
        {
            if (downIsPressed && ClickCoolDown())
            {
                return true;
            }
            return false;
        }
        private static bool ClickCoolDown()
        {
            coolDown -= Time.deltaTime;
            if (coolDown <= 0)
            {
                coolDown = ClickTime;

                return true;
            }
            return false;
        }

    }
    struct StartOptions
    {
        public int x_res;
        public int y_res;
        public bool FullScreenBool;
        public int QualityAnt;
    }
    StartOptions StartOPedop;
    // Use this for initialization
    void Start () {
       // up = KeyCode.joy
        // Fetch all texts allTexts
      //  allTexts = gameObject.GetComponentsInChildren<TextMesh>();
        otherScript = GameObject.Find("MenuPlayer").GetComponent<MenuRayCast>();

        StartOPedop.x_res = Screen.currentResolution.width;
        StartOPedop.y_res = Screen.currentResolution.height;
        StartOPedop.FullScreenBool = Screen.fullScreen;
        StartOPedop.QualityAnt = QualitySettings.GetQualityLevel();
     //   Debug.Log("Quality is now " + StartOPedop.QualityAnt);
        OptionOn = false;
        InitializeTexts();
        OptionList = new List<TextInfo>();
        OptionList.Add(MasterSoundInfo);
        OptionList.Add(Musicinfo);
        OptionList.Add(SoundInfo);
        OptionList.Add(Resolution_X_Info);
        OptionList.Add(Resolution_Y_Info);
        OptionList.Add(FullScreenInfo);
        OptionList.Add(QualityInfo);
        OptionList.Add(BackInfo);
        OptionList.Add(ApplyInfo);
        OptionList.Add(ResetInfo);

        MasterSoundVolume = 30;
        SoundVolume = 30;
        MusicVolume = 30;
        VideoResolution_X = StartOPedop.x_res;
        VideoResolution_Y = StartOPedop.y_res;
        xRes = FindNumbers(VideoResolution_X, X_res);
        YRes = FindNumbers(VideoResolution_Y, Y_res);
        FullScreenMode = StartOPedop.FullScreenBool;
        QualitySetting = StartOPedop.QualityAnt;

        applyOptions();
        lollipop3();
    }
    public float testalpha;
    void Update()
    {
        //SetMaterialAlpha(testalpha, standardMat);
        ClickTime = pressTimer;
        // Running Stage
        if (OptionOn)
        {
            InputHandler.CheckInput();            
            ChangeSettings();
            UpdateDisplay();
        }
        //Fade in Stage

        // Fade out Stage
        if (fadeOut) lowerAlpha();
        else if (fadeIn) IncreaseAlpha();
        //Testing
     //   lollipop();
    }
    /*
    private void lollipop()
    {
       // Debug.Log("lollipop! ");
        for (int i = 0; i < allTexts.Length; i++)
        {
            SetAlpha(TestAlpha, allTexts[i]);
           // Debug.Log("NEW ALPHA ON " +allTexts[i].name);
        }
    }
    private void lollipop2()
    {
       // Debug.Log("lollipop! ");
        for (int i = 0; i < allTexts.Length; i++)
        {
            SetAlpha(cAlpha, allTexts[i]);
          //  Debug.Log("NEW ALPHA ON " + allTexts[i].name);
        }
    }
  */
  private void lollipop3()
    {
        SetMaterialAlpha(0, standardMat);
        SetMaterialAlpha(0, glowMat);
    }
    private float cAlpha;
    public float AlphaMult;
    private bool fadeIn;
    private bool fadeOut;
    void lowerAlpha()
    {

        
            cAlpha -= AlphaMult * Time.deltaTime;
        if (cAlpha <= 0)
        {
            cAlpha = 0;
            fadeOut = false;
            otherScript.GoBackFromOptions();
        }
        SetMaterialAlpha(cAlpha, glowMat);
        SetMaterialAlpha(cAlpha, standardMat);
        //  lollipop2();

    }
    void IncreaseAlpha()
    {

        cAlpha += AlphaMult * Time.deltaTime;
        if (cAlpha >= 1)
        {
            cAlpha = 1;
            fadeIn = false;
        }
        SetMaterialAlpha(cAlpha, glowMat);
        SetMaterialAlpha(cAlpha, standardMat);
        //  lollipop2();
    }
    void SetAlpha(float f, TextMesh t)
    {
        TestAlpha = f;
       
     //   t.color = new Color(t.color.r, t.color.g, t.color.b, f);
        




    }
    void SetMaterialAlpha(float f, Material m)
    {
        m.color = new Color(m.color.r, m.color.g, m.color.b, f);
    }
    void InitializeTexts()
    {
        MasterSoundInfo.textMesh = GameObject.Find("MasterSoundText").GetComponent<TextMesh>();
        Resolution_X_Info.textMesh = GameObject.Find("Video_Resolution_X").GetComponent<TextMesh>();
        Resolution_Y_Info.textMesh = GameObject.Find("Video_Resolution_Y").GetComponent<TextMesh>();
        FullScreenInfo.textMesh = GameObject.Find("FullScreenMode").GetComponent<TextMesh>();
        QualityInfo.textMesh = GameObject.Find("GraphicQuality").GetComponent<TextMesh>();
        ApplyInfo.textMesh = GameObject.Find("ApplySettings").GetComponent<TextMesh>();
        ResetInfo.textMesh = GameObject.Find("Reset").GetComponent<TextMesh>();
        BackInfo.textMesh = GameObject.Find("Back To Main Menu").GetComponent<TextMesh>();
        SoundInfo.textMesh = GameObject.Find("SoundVolume").GetComponent<TextMesh>();
        Musicinfo.textMesh = GameObject.Find("MusicVolume").GetComponent<TextMesh>();

        MasterSoundInfo = setStandardTextLength(MasterSoundInfo);
        Musicinfo = setStandardTextLength(Musicinfo);
        SoundInfo = setStandardTextLength(SoundInfo);
        //Debug.Log("sound length " + SoundInfo.Textlength + " || music length " + Musicinfo.Textlength);
        Resolution_X_Info = setStandardTextLength(Resolution_X_Info);
        Resolution_Y_Info = setStandardTextLength(Resolution_Y_Info);
        FullScreenInfo = setStandardTextLength(FullScreenInfo);
        QualityInfo = setStandardTextLength(QualityInfo);
        ApplyInfo = setStandardTextLength(ApplyInfo);
        ResetInfo = setStandardTextLength(ResetInfo);
        BackInfo = setStandardTextLength(BackInfo);
}
    TextInfo setStandardTextLength(TextInfo t)
    {
        t.Textlength = t.textMesh.text.Length;
        return t;
    }
    private void resetOptions()
    {
       // Debug.Log("ResetOption!");
        MasterSoundVolume = 30;
        SoundVolume = 30;
        MusicVolume = 30;
        VideoResolution_X = StartOPedop.x_res;
        VideoResolution_Y = StartOPedop.y_res;
        xRes = FindNumbers(VideoResolution_X, X_res);
        YRes = FindNumbers(VideoResolution_Y, Y_res);
        FullScreenMode = StartOPedop.FullScreenBool;
        QualitySetting = StartOPedop.QualityAnt;
        
        applyOptions();
    }
    private void UpdateDisplay()
    {
        MasterSoundInfo.textMesh.text = makeString(MasterSoundInfo, MasterSoundVolume);
        Musicinfo.textMesh.text = makeString(Musicinfo, MusicVolume);
        SoundInfo.textMesh.text = makeString(SoundInfo, SoundVolume);
        Resolution_X_Info.textMesh.text = VideoResolution_X.ToString();
        Resolution_Y_Info.textMesh.text = VideoResolution_Y.ToString();
        if (FullScreenMode) FullScreenInfo.textMesh.text = "On";
        else FullScreenInfo.textMesh.text = "Off";
        QualityInfo.textMesh.text = makeVideoQualityString(QualityInfo, 0);
    }
    private String makeVideoQualityString(TextInfo t, int inte)
    {

        String s;
        s = t.textMesh.text.Substring(0, t.Textlength);

        for (int i = t.Textlength; i < SpaceSpaceSpace; i++)
        {
            s += " ";
        }
        s += QualityStrings[QualitySetting];
      
        return s;
    }
    private String makeString(TextInfo t, int inte)
    {

        String s;
        s = t.textMesh.text.Substring(0, t.Textlength);
       
        for(int i = t.Textlength; i<SpaceSpaceSpace; i++)
        {
            s += " ";
        }
        s += inte.ToString();
        
        return s;
    }
    private void applyOptions()
    {   //Sound updates all the time!
        // Fix correct variables.
        //AudioListener.volume = (float)MasterSoundVolume / 100;
      
         //   Debug.Log("Apply Options!");
            Screen.SetResolution(VideoResolution_X, VideoResolution_Y, FullScreenMode);
            QualitySettings.SetQualityLevel(QualitySetting, true);
          //   Debug.Log("Quality is now " + StartOPedop.QualityAnt);
        // TODO
        //Update Save file!!

    }
	public void OptionsOn()
    {
        OptionOn = true;
        fadeIn = true;
        fadeOut = false;
    }
	// Update is called once per frame

    void ChangeSettings()
    {
        addGlow(OptionList[currentOption]);
        if (Input.GetKeyDown(KeyCode.DownArrow) || InputHandler.getClickDown())
        {

            RemoveGlow(OptionList[currentOption]);
            currentOption++;
            if (currentOption == OptionList.Count) currentOption = 0;
            
           
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || InputHandler.getClickUp())
        {
            RemoveGlow(OptionList[currentOption]);
            currentOption--;
            if (currentOption == -1) currentOption = OptionList.Count-1;
           
        }
        bool GoBack = false;
        if (InputHandler.getBack())
        {
            RemoveGlow(OptionList[currentOption]);
            GoBack = true;
            currentOption = 7;
        }
        switch (currentOption)
        {
            
            //Change MasterSound
            case 0:
                MasterSoundVolume = changeNumberValue(MasterSoundVolume);
                //TODO CALL A FUNCTIONs to update sound music and master and update save file
                break;
                //Change Music Volume
            case 1:
                MusicVolume = changeNumberValue(MusicVolume);
                break;
                // Change SoundVolume (gameSound)
            case 2:
                SoundVolume = changeNumberValue(SoundVolume);
                break;
                // Change resolution X
            case 3:
                ChangeXres();
                
                break;
            case 4:
                // Change Resolution Y
                ChangeYres();
                
                break;
                // FullScreen On/Off
            case 5:
                if (Input.GetKeyDown(KeyCode.LeftArrow) || InputHandler.getClickLeft())
                {
                    FullScreenMode = !FullScreenMode;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow) || InputHandler.getClickRight())
                {
                    FullScreenMode = !FullScreenMode;
                }
                break;
            // Change Graphic Quality
            case 6:
                GraphicSettings();
                    break;
            // Back To main Menu
            case 7:
                if (Input.GetKeyDown(KeyCode.KeypadEnter) || InputHandler.getEnter() || GoBack)
                    backToMainMenu();
               
                break;
                // ApplyGraphicSettings
            case 8:
                if (Input.GetKeyDown(KeyCode.KeypadEnter) || InputHandler.getEnter())
                    applyOptions();
                break;
                // ResetOption
            case 9:
                if(Input.GetKeyDown(KeyCode.KeypadEnter) || InputHandler.getEnter())
                resetOptions();
                break;

        }
    }
    private void backToMainMenu()
    {
      //  Debug.Log("Go Back to main menu!");
        VideoResolution_X = Screen.currentResolution.width;
        VideoResolution_Y = Screen.currentResolution.height;
        xRes = FindNumbers(VideoResolution_X, X_res);
        YRes = FindNumbers(VideoResolution_Y, Y_res);
        FullScreenMode = Screen.fullScreen;
        QualitySetting = QualitySettings.GetQualityLevel();
        RemoveGlow(OptionList[currentOption]);
        currentOption = 0;
        OptionOn = false;
        fadeIn = false;
       
        fadeOut = true;
        
        // Fade out text
        // Disable function with optionon
        // Fade in original Text.
    }

    private int FindNumbers(int res, int[] a)
    {
        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] == res)
                return i;
        }
        return 0;
    }
    
    private void GraphicSettings()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || InputHandler.getClickLeft())
        {
            QualitySetting--;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || InputHandler.getClickRight())
        {
            QualitySetting++;
        }
        if (QualitySetting < 0) QualitySetting = 0;
        if (QualitySetting >= QualityStrings.Length) QualitySetting = QualityStrings.Length - 1;
    }
    private int xRes = 0;
    private void ChangeXres()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || InputHandler.getClickLeft())
        {
            xRes--;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || InputHandler.getClickRight())
        {
            xRes++;
        }
        if (xRes < 0) xRes = 0;
        if (xRes >= X_res.Length) xRes = X_res.Length-1;

        VideoResolution_X = X_res[xRes];
    }
    private int YRes = 0;
    private void ChangeYres()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || InputHandler.getClickLeft())
        {
            YRes--;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || InputHandler.getClickRight())
        {
            YRes++;
        }
        if (YRes < 0) YRes = 0;
        if (YRes >= Y_res.Length) YRes = Y_res.Length - 1;

        VideoResolution_Y = Y_res[YRes];
    }
   
    private int changeNumberValue(int number)
    {

        if(Input.GetKey(KeyCode.LeftArrow) || InputHandler.getLeft())
        {
            number--;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || InputHandler.getRight())
        {
            number++;
        }
        if (number < 0) number = 0;
        if (number > 100) number = 100;
        return number;
    }
    private TextInfo addGlow(TextInfo t)
    {
        // t.textMesh.color = TargetColor;
        MeshRenderer r = t.textMesh.gameObject.GetComponent<MeshRenderer>();
        r.material = glowMat;
        return t;
    }
    private TextInfo RemoveGlow(TextInfo t)
    {
        t.textMesh.color = StandardColor;
        MeshRenderer r = t.textMesh.gameObject.GetComponent<MeshRenderer>();
        r.material = standardMat;
        return t;
    }
}
