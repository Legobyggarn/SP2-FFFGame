using UnityEngine;
using System.Collections;

public class ScenesTransision : MonoBehaviour {

	//Public
	public Material mat;
	public float lerpTime = 5.0f;
	public bool white;

	//Private
	private Color colorWhite = Color.white;
	private Color coloWhiteTransparent = new Color (0, 0, 0, 0);

	private bool fromWhiteB = false;
	private bool toWhiteB = false;

	private float currentLerpTime;



	// Use this for initialization
	void Start () 
	{
		if (white) 
		{
			mat.color = Color.white;
		}
		else 
		{
			mat.color = new Color (0, 0, 0, 0);
		}

		fromWhiteB = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (fromWhiteB) 
		{
			currentLerpTime += Time.deltaTime;
			float prec1 = currentLerpTime / lerpTime;
			fromWhite (prec1);
		} 
		else if (toWhiteB) 
		{
			currentLerpTime += Time.deltaTime;
			float prec2 = currentLerpTime / lerpTime;
			toWhite (prec2);
		}
	}

	void toWhite (float LerpPrec)
	{
		
		mat.color = Color.Lerp (coloWhiteTransparent, colorWhite, LerpPrec);

		if (LerpPrec > 1) 
		{
			toWhiteB = false;
		}
	}

	void fromWhite (float LerpPrec)
	{
		mat.color = Color.Lerp (colorWhite, coloWhiteTransparent, LerpPrec);

		if (LerpPrec > 1) 
		{
			fromWhiteB = false;
		}
	}

	public void fadeToWhite()
	{
		toWhiteB = true;
		currentLerpTime = 0f;
	}

	public void fadeFromWhite()
	{
		fromWhiteB = true;
		currentLerpTime = 0f;
	}

	public float getLerpTime()
	{
		return lerpTime;
	}
}
