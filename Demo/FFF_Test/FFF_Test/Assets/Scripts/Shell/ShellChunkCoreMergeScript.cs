using UnityEngine;
using System.Collections;

public class ShellChunkCoreMergeScript : MonoBehaviour {

	// Public variables
	public float mergeTime;

	// Private variables
	private bool mIsMerging = false;
	private float mMergeTimer = 0f;

	private Vector3 mMergeStartPos = new Vector3();
	private Vector3 mMergeEndPos = new Vector3();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		// Merging
		if (mIsMerging) {
			float percentage = mMergeTimer / mergeTime;
			if (mMergeTimer < mergeTime) { // Timer still ticking
				// Lerp position towards center of core.
				transform.position = Vector3.Lerp(mMergeStartPos, mMergeEndPos, percentage);
				// Lerp scale towards (0,0,0)
				transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, percentage);
				mMergeTimer += Time.deltaTime; // Tick timer
			}
			else { // Timer ended
				Destroy(gameObject); // Destroy self
			}
		}

	}

	public void MergeWithCore(Vector3 mergeEndPos) {

		// Set up for lerping for position and scale
		mIsMerging = true; // Activate merging
		mMergeStartPos = transform.position; // Set start position of merge lerp
		mMergeEndPos = mergeEndPos; // Set position to merge towards

		// Deactivate collision detection
		GetComponent<MeshCollider>().enabled = false;

	}

}
