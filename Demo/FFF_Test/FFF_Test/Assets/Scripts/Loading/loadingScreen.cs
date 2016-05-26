using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class loadingScreen : MonoBehaviour {

	public ScenesTransision st;

	public float sceneChangeTime;
    public float maxSceneChangeTime = 10f;

	public float sceneFadeTime;
    public float maxSceneFadeTime;

    public bool fading = true;

	// Scene loading
	private bool loaded = false;
	public string levelName;
	private AsyncOperation asyncLoad;

	// Use this for initialization
	void Start () 
	{
		st.fadeFromWhite ();
		maxSceneFadeTime = st.getLerpTime();
        //
        // Start loading
        StartCoroutine("load");
       
    }
	
	// Update is called once per frame
	void Update () 
	{
		sceneChangeTime += Time.deltaTime;

		// If level loaded fade to white and start level
		if (maxSceneChangeTime < sceneChangeTime && loaded) // (maxSceneChangeTime < sceneChangeTime)
		{ 
			if (fading) 
			{
				st.fadeToWhite ();
				fading = false;
			}
			sceneFadeTime += Time.deltaTime;

			if (maxSceneFadeTime < sceneFadeTime) 
			{
				// Start the loaded level...
				asyncLoad.allowSceneActivation = true;

                
                Application.LoadLevel ("new_assets_scene");
			}
		}

		// Print loading process
		//Debug.Log("Loading: " + async.progress + "%");

	}

	// Coroutine for loading
	IEnumerator load() {
		// Debug log warning?
		//Debug.Log("Loading scene " + levelName);
		asyncLoad = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
		asyncLoad.allowSceneActivation = false;
		while (!loaded) {
		//	Debug.Log("Loading: " + asyncLoad.progress + "%");
            
			yield return null;
			// Work around for bug stopping loading at 0.9
			if (asyncLoad.progress >= 0.9f) {
			//	Debug.Log("Loading complete");
				loaded = true;
			}
		}

	}

}
