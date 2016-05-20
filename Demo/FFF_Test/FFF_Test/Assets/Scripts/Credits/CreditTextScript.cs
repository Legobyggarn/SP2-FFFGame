using UnityEngine;
using System.Collections;

//[RequireComponent(typeof TextMesh)]
public class CreditTextScript : MonoBehaviour {

	// Public variables
	//...

	// Private variables
	private string mText;
	private Vector3 mStart;
	private Vector3 mEnd;
	private float mTime;
	private float mFadeInDuration;
	private float mFadeOutDuration;
	private Color mFadedInColor;
	private Color mFadedOutColor;

	private TextMesh mTextMesh;

	private float mTimer = 0f;

	// Use this for initialization
	void Start () {
		// Get the text mesh
		mTextMesh = GetComponent<TextMesh>();
		// Rotate 90 degrees around the x-axis
		transform.RotateAround(transform.position, transform.right, 90);
	}

	// Update is called once per frame
	void Update () {

		// Update timer
		mTimer += Time.deltaTime;

		// Lerp between start and end (at end destroy gameObject). Also lerp fade in and fade out of color
		if (mTimer < mTime) {
			// Position
			float perc = mTimer / mTime;
			transform.position = Vector3.Lerp (mStart, mEnd, perc);

			// Color
			if (mTimer < mFadeInDuration) { // Fade in
				// Fade in
				float fadeInPerc = mTimer / mFadeInDuration;
				mTextMesh.color = Color.Lerp(mFadedOutColor, mFadedInColor, fadeInPerc);
			} 

			else if (mTimer >= mTime - mFadeOutDuration) { // Fade out
				// Fade out
				float fadeOutPerc = (mTimer - (mTime - mFadeOutDuration)) / mFadeOutDuration;
				mTextMesh.color = Color.Lerp(mFadedInColor, mFadedOutColor, fadeOutPerc);
			} 

			else {
				// Set color
				mTextMesh.color = mFadedInColor;
			}
		} 

		else {
			Destroy(gameObject);
		}

	}

	// CreditText setup
	public void setupCreditText(string text, Vector3 start, Vector3 end, float time, float fadeIn, float fadeOut, Color fadedInColor, Color fadedOutColor) {
		// Set the variables
		mText = text;
		mStart = start;
		mEnd = end;
		mTime = time;
		mFadeInDuration = fadeIn;
		mFadeOutDuration = fadeOut;
		mFadedInColor = fadedInColor;
		mFadedOutColor = fadedOutColor;
		
		// Update the text of TextMesh
		GetComponent<TextMesh>().text = text;
	}

}
