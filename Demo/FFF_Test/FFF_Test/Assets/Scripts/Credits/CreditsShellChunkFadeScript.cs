using UnityEngine;
using System.Collections;

public class CreditsShellChunkFadeScript : MonoBehaviour {

	// Public variables
	public float fadeTime;


	// Private variables
	private float timer = 0f;
	private bool isGrowing = true;
	private Vector3 maxScale;

	private Color startColor;
	private Color endColor;

	private MeshRenderer mMeshRenderer;

	// Use this for initialization
	void Start () {

		// Set target/max scale (to grow to)
		maxScale = transform.localScale * GameObject.Find("GLOBAL_SCALE").GetComponent<GlobalScaleScript>().shellScale;

		// Set current scale to zero
		//transform.localScale = Vector3.zero;


		mMeshRenderer = transform.GetChild (0).gameObject.GetComponent<MeshRenderer> ();
		// Set colors
		endColor = mMeshRenderer.material.color;
		startColor = new Color(endColor.r, endColor.b, endColor.g, 0.5f);

		mMeshRenderer.material.color = startColor;


	}
	
	// Update is called once per frame
	void Update () {
	
		// Grow from 0,0,0 to 'maxScale' during 'fadeTime' seconds.
		if (timer <= fadeTime && isGrowing) {
			timer += Time.deltaTime;
			float perc = timer / fadeTime;

			// Debug
			//Debug.Log("[CreditsShellChunkFadeScript]: Percentage: " + perc);

			//transform.localScale = Vector3.Lerp (Vector3.zero, maxScale, perc);

			//mMeshRenderer.material.color = Color.Lerp(startColor, endColor, perc);

			// Debug
			Debug.Log("[CreditsShellChunkFadeScript]: Color: " + mMeshRenderer.material.color);

		} 

	}

}
