using UnityEngine;
using System.Collections;

public class Options : MonoBehaviour {
   // Private members (TODO)
    public int MasterSoundVolume;
    public int GameVolume;
    public int MusicVolume;
    public int VideoResolution_X;
    public int VideoResolution_Y;
    public bool FullScreenMode;
    public int QualitySetting;
    string[] QualityStrings = { "Fastest", "Fast", "Simple", "Good", "Beautiful", "Fantastic" };
    public bool apply;
    public bool cancel;
    public bool OptionOn;
    public Material OptionMaterial;
    public TextMesh MasterSoundText;
    public TextMesh Resolution_X_Text;
    public TextMesh Resolution_Y_Text;
    public TextMesh FullScreenText;
    public TextMesh QualityText;
    public TextMesh ApplyText;
    public TextMesh CancelText;
    public TextMesh BackText;
    // Use this for initialization
    void Start () {
        MasterSoundVolume = 30;
        GameVolume = 30;
        MusicVolume = 30;
        VideoResolution_X = 640;
        VideoResolution_Y = 480;
        FullScreenMode = true;
        QualitySetting = 0;
        apply = false;
        cancel = false;
        OptionOn = false;
	}
    private void resetOptions()
    {
        MasterSoundVolume = 30;
        GameVolume = 30;
        MusicVolume = 30;
        VideoResolution_X = 640;
        VideoResolution_Y = 480;
        FullScreenMode = true;
        QualitySetting = 0;
        apply = true;
        applyOptions();
    }
    public void applyOptions()
    {
        if(apply)
        {
            AudioListener.volume = (float)MasterSoundVolume / 100;
            Screen.SetResolution(VideoResolution_X, VideoResolution_Y, FullScreenMode);
            QualitySettings.SetQualityLevel(QualitySetting, true);
            apply = false;
                  
        }
    }
	public void OptionsOn()
    {
        OptionOn = true;
    }
	// Update is called once per frame
	void Update () {
	
	}
}
