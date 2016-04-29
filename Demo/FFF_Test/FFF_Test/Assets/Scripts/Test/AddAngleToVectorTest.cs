using UnityEngine;
using System.Collections;

public class AddAngleToVectorTest : MonoBehaviour {

	/*
		float angle = Mathf.Acos (Vector3.Dot (outVec, compVec) / (outVec.magnitude * compVec.magnitude));
		angle *= Mathf.Rad2Deg;
	*/

	// Hypotenuse = Adjacent / cos(angle)

	// Ex: OutVec = normalVec / cos(Mathf.dgr2rad(angle);

	// Use this for initialization
	void Start () {

		// Debug line for up "normal"
		/*
		Vector3 startPos = transform.position;
		Vector3 endPos = (startPos + transform.up) * 10f;
		Debug.DrawLine(startPos, endPos, Color.blue, 10f, false);
		*/

		// Test (Vector3 OrthoNormalize)
		/*
		Vector3 normal = transform.up;
		Vector3 tangent = Vector3.one;
		Vector3 binormal = Vector3.one;
		Vector3.OrthoNormalize(ref normal, ref tangent, ref binormal);

		// Debug line the axis
		Vector3 startPoint = transform.position;
		// Normal
		Vector3 endNormal = (startPoint + normal) * 10f;
		Debug.DrawLine(startPoint, endNormal, Color.blue, 10f, false);
		// Tangent
		Vector3 endTangent = (startPoint + tangent) * 10f;
		Debug.DrawLine(startPoint, endTangent, Color.green, 10f, false);
		// Binormal
		Vector3 endBinormal = (startPoint + binormal) * 10f;
		Debug.DrawLine(startPoint, endBinormal, Color.red, 10f, false);
		*/


		/*
		// Create orthogonal vectors
		Vector3 n; // Normal(?)
		Vector3 v; // Tangent(?)
		Vector3 u; // Binormal(?)
		n = transform.up;
		//v = Vector3.one;
		//u = Vector3.one;

		v = Vector3.Cross(n, u);
		v.Normalize();

		u = Vector3.Cross(n, v);
		u.Normalize();

		// Debug line the axis
		Vector3 startPoint = transform.position;
		// Normal
		Vector3 endNormal = (startPoint + n) * 10f;
		Debug.DrawLine(startPoint, endNormal, Color.blue, 10f, false);
		// Tangent
		Vector3 endTangent = (startPoint + v) * 10f;
		Debug.DrawLine(startPoint, endTangent, Color.green, 10f, false);
		// Binormal
		Vector3 endBinormal = (startPoint + u) * 10f;
		Debug.DrawLine(startPoint, endBinormal, Color.red, 10f, false);
		*/




		// Calculate out vector
		//Vector3 outVec = Quaternion.AngleAxis(25f, transform.right).eulerAngles;
		//transform.rotation = Quaternion.AngleAxis(25f, transform.right);
		// Debug line for out vector
		/*
		startPos = transform.position;
		endPos = (startPos + transform.up) * 10f;
		Debug.DrawLine(startPos, endPos, Color.green, 10f, false);

		// Debug

		Debug.Log("Normal: " + transform.up + " | OutVec: " + outVec);
		*/



		// Vector3 outVec = transform.up / Mathf.Cos(Mathf.Deg2Rad(25)); // New vector with 25 degree difference

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void createOrtogonalVectors(ref Vector3 normal, ref Vector3 tangent, ref Vector3 binormal) {

		//...

	}

}
