using UnityEngine;
using System.Collections;
using System;

public class ShellPartPriority { //  : IComparable<ShellPartPriority>

	// Variables
	private float mDistance;
	private GameObject mGameObject;

	// Getter/Setters
	public float Distance {
		get {
			return mDistance;
		}
	}

	public GameObject CloseGameObject {
		get {
			return mGameObject;
		}
	}

	// Constructor
	public ShellPartPriority(float distance, GameObject gameObject) {
		mDistance = distance;
		mGameObject = gameObject;
	}

	// Destructor
	~ShellPartPriority() {
		//...	
	}

	// Operators
	// Equal & not equal
	public static bool operator== (ShellPartPriority spp_one, ShellPartPriority spp_two) {
		return spp_one.Distance == spp_two.Distance;
	}

	public static bool operator!= (ShellPartPriority spp_one, ShellPartPriority spp_two) {
		return spp_one.Distance != spp_two.Distance;
	}

	// Greater & less
	public static bool operator< (ShellPartPriority spp_one, ShellPartPriority spp_two) {
		return spp_one.Distance < spp_two.Distance;
	}

	public static bool operator> (ShellPartPriority spp_one, ShellPartPriority spp_two) {
		return spp_one.Distance > spp_two.Distance;
	}

	// Equal or greater & equal or less
	public static bool operator<= (ShellPartPriority spp_one, ShellPartPriority spp_two) {
		return spp_one.Distance <= spp_two.Distance;
	}

	public static bool operator>= (ShellPartPriority spp_one, ShellPartPriority spp_two) {
		return spp_one.Distance >= spp_two.Distance;
	}

	/*
	// ICombarable, CompareTo (?)
	public int CompareTo(ShellPartPriority spp) {

		// Handle null

		if (spp != null) {
			return this.mDistance.CompareTo(spp.Distance);
		}

		else {
			// TODO: Return something that is sure to work
			return 1;
		}

	}
	*/

	//...

}
