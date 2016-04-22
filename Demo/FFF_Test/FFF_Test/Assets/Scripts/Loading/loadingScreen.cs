using UnityEngine;
using System.Collections;

public class loadingScreen : MonoBehaviour {

	public ScenesTransision st;

	private float sceneChangeTime;
	private float maxSceneChangeTime = 6f;

	private float sceneFadeTime;
	private float maxSceneFadeTime;

	private bool fading = true;

	// Use this for initialization
	void Start () 
	{
		st.fadeFromWhite ();
		maxSceneFadeTime = st.getLerpTime();
	}
	
	// Update is called once per frame
	void Update () 
	{
		sceneChangeTime += Time.deltaTime;

		if (maxSceneChangeTime < sceneChangeTime) 
		{ 
			if (fading) 
			{
				st.fadeToWhite ();
				fading = false;
			}
			sceneFadeTime += Time.deltaTime;
			if (maxSceneFadeTime < sceneFadeTime) 
			{
				Application.LoadLevel ("Scene_01");
			}
		}
	}
}
