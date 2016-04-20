using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerMovement : MonoBehaviour {

	// STRAFE/DIRECTION Variables
	public Transform PlayerStrafeTransform;
	public Transform PlayerRotationTransform;
	private Transform OculusTransform;
	public CharacterController charactercontroller;
	enum DIRECTIONS { UP, DOWN, RIGHT, LEFT, FORWARD, BACK, DIR_X, DIR_Y, DIR_Z };

	public bool OculusSuperMod;
	public float MaxSpeedStrafe;
	public float MinSpeedStrafe;
	public bool StaticStrafeSpeed;
	public float StaticSpeed;
	public bool UseSmoothAcceleration;
	public float SmoothAcceleration;
	public float SmoothDeacceleration;

	public float TimeToSpeedINC;
	public float TimeToSpeedDEC;
	public float SpeedINC;
	public float SpeedDEC;

	private DirectionVariables mDirUp;
	private DirectionVariables mDirDown;
	private DirectionVariables mDirForward;
	private DirectionVariables mDirBack;
	private DirectionVariables mDirLeft;
	private DirectionVariables mDirRight;

	private DirectionVariables mDirX;
	private DirectionVariables mDirY;
	private DirectionVariables mDirZ;
	private Vector3 MovementVector;
	private Vector3 direction;
	struct DirectionVariables
	{
		public DirectionVariables(DIRECTIONS dir, string inputname, int inputvalue)
		{
			mCurrentSpeed = 0;
			mTimeToStepSpeedINC = 0;
			mTimeToStepSpeedDEC = 0;
			mDir = dir;
			mInputName = inputname;
			mInputValue = inputvalue;
			mAxisPercentage = 0;

		}
		public DirectionVariables(DIRECTIONS dir, string inputname)
		{
			mCurrentSpeed = 0;
			mTimeToStepSpeedINC = 0;
			mTimeToStepSpeedDEC = 0;
			mDir = dir;
			mInputName = inputname;
			mInputValue = 0;
			mAxisPercentage = 0;

		}
		public float mCurrentSpeed;
		public float mTimeToStepSpeedINC;
		public float mTimeToStepSpeedDEC;
		public float mAxisPercentage;
		public int mInputValue;
		public DIRECTIONS mDir;
		public string mInputName;
		public void UpdateInputValue()
		{
			float x = Input.GetAxis(mInputName);
			mAxisPercentage = Mathf.Abs(x);

		}
		public bool ButtonIsDown()
		{
			float value = mInputValue / 2;
			if(mInputValue == 1)
			{
				if(Input.GetAxis(mInputName) > value)
				{
					return true;
				}
				else
				{
					return false;
				}

			}
			else
			{
				if(Input.GetAxis(mInputName) < value)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}
		public bool getInput()
		{
			return true;

		}
		public float getPosOrNeg()
		{
			float tresh = 0.2f;
			float r = 1f;
			float input = Input.GetAxis (mInputName);
			if (input < -tresh) {
				Debug.Log ("move back!");
				return -r;

			}
			else if (input > tresh) {
				Debug.Log ("move forward!");
				return r;
			}
			else  {
				Debug.Log ("move Dont!");
				return 0;
			}
			return 0;
		}



	}
	List<DirectionVariables> mDirList;

	// ROTATE VARIABLES
	Quaternion ForwardRotation;
	Quaternion LookRotation;
	Quaternion newRotation;
	enum ROTATIONS { ROTATE_Y_AXIS, ROTATELEFT, ROTATE_X_AXIS, ROTATEBACKWARD, ROTATE_Z_AXIS, SPINLEFT}
	public float RotationSpeed;

	struct RotationVariables
	{
		public RotationVariables(ROTATIONS rot, string inputname)
		{
			mRot = rot;
			mRotPercantage = 0;
			mInputName = inputname;
		}
		public ROTATIONS mRot;
		public float mRotPercantage;
		public string mInputName;
	}
	RotationVariables mRotY;
	RotationVariables mRotX;
	RotationVariables mRotZ;
	RotationVariables mRotBackward;
	RotationVariables mSpinRight;
	RotationVariables mSpinLeft;
	List<RotationVariables> mRotList;
	void Start () {
		charactercontroller = GetComponent<CharacterController>();
		InitializeDirectionController();
		InitializeRotation();
		//	OculusSuperMod = false;
	}

	// Update is called once per frame
	void Update () {

		Strafer();
		Rotation();
	}


	private void InitializeRotation()
	{
		mRotList = new List<RotationVariables>();
		mRotY = new RotationVariables(ROTATIONS.ROTATE_Y_AXIS, "Rotate_Y_AXIS");
		// mRotLeft = new RotationVariables(ROTATIONS.ROTATELEFT, "RotateVertical");
		mRotX = new RotationVariables(ROTATIONS.ROTATE_X_AXIS, "Rotate_X_AXIS");
		// mRotBackward = new RotationVariables(ROTATIONS.ROTATEBACKWARD, "RotateHorizontal");
		mRotZ = new RotationVariables(ROTATIONS.ROTATE_Z_AXIS, "Rotate_Z_AXIS");
		// mSpinLeft = new RotationVariables(ROTATIONS.SPINLEFT, "Spin");
		mRotList.Add(mRotY);
		mRotList.Add(mRotX);
		mRotList.Add(mRotZ);
		/*  mRotList.Add(mRotBackward);
        mRotList.Add(mSpinRight);
        mRotList.Add(mSpinLeft);*/
	}
	private void Rotation()
	{
		// newRotation = ForwardRotation.
		//Iterate
		for(int i = 0; i < mRotList.Count; i++)
		{
			RotationVariables rot = mRotList[i];

			if (!OculusSuperMod) { 
				rot = UpdateInput(rot);
			}
			else if(OculusSuperMod)
			{
				Quaternion qLook = OculusTransform.rotation;
				Vector3 AngleVector = qLook.eulerAngles;
				rot = OculusRotateValue(rot, AngleVector);
			}

			UpdateRotation(rot);


			mRotList[i] = rot;

		}  





	}

	/*
    // Mouse handler for "vr simulation"
	private float rotX;
	private float rotY;
	private Vector3 moveDirection;
	private Vector3 lookDireciton;
	private float speed;
	//public
	bool noMouse;
	float antiJitterX;
	float antiJitterY;
	float rotSpeedX;
	float rotSpeedY;
	float mouseRotSpeed;
	float maxSpeed;
	float minSpeed;
	float acceleration;
	float deacceleration;
	void MouseInput()
	{
		rotX = transform.localRotation.eulerAngles.x;
		rotY = transform.localRotation.eulerAngles.y;
		if (!noMouse)
		{
			float ourX = Input.mousePosition.x / Screen.width * 2 - 1;
			float ourY = Input.mousePosition.y / Screen.height * 2 - 1;    
			if (ourX <= antiJitterX && ourX >= -antiJitterX)
			{
				ourX = 0.0f;
			}
			if (ourY <= antiJitterY && ourY >= -antiJitterY)
			{
				ourY = 0.0f;
			}
			rotX += -ourY * rotSpeedX;
			rotY += ourX * rotSpeedY;
			// Change to get the child that is a camera
			transform.localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
			lookDireciton = transform.forward;
		}
		else
		{
			lookDireciton = OculusTransform.forward;
		}
		// Move
		// If the player is not moving forward, then move forward slowly. (Add a check for direction in x)
		if (Input.GetAxis("FlyTowards") == 0)
		{            
			if (speed > minSpeed)
			{
				speed -= deacceleration * Time.deltaTime;
			}
			else
			{
				speed = minSpeed;
			}
		}
		else
		{
			moveDirection = Vector3.RotateTowards(moveDirection, lookDireciton, mouseRotSpeed * Time.deltaTime, 0.0f);




			if (speed < maxSpeed)
			{
				speed += acceleration * Time.deltaTime;
			}
			else
			{
				speed = maxSpeed;
			}
		}


		moveDirection = moveDirection.normalized;

		moveDirection *= speed;




		charactercontroller.Move(moveDirection * Time.deltaTime);
	}
	*/
	RotationVariables OculusRotateValue(RotationVariables rot, Vector3 angleVector)
	{

		switch (rot.mRot)
		{

		case ROTATIONS.ROTATE_Y_AXIS:
			rot.mRotPercantage = Mathf.Sin(angleVector.y / 57.2958f);
			break;
		case ROTATIONS.ROTATE_X_AXIS:

			rot.mRotPercantage = Mathf.Sin(angleVector.x / 57.2958f);

			break;



		case ROTATIONS.ROTATE_Z_AXIS:

			rot.mRotPercantage = Mathf.Sin(angleVector.z / 57.2958f);
			break;
		}
		return rot;
	}



	private void UpdateRotation(RotationVariables rot)
	{
		float mult = 0;
		Vector3 Rot = Vector3.zero;
		switch (rot.mRot)
		{

		case ROTATIONS.ROTATE_Y_AXIS:

			mult = Time.deltaTime * rot.mRotPercantage * RotationSpeed;

			Rot = Vector3.up * mult;
			PlayerRotationTransform.Rotate(Rot);

			break;



		case ROTATIONS.ROTATE_X_AXIS:

			mult = Time.deltaTime * rot.mRotPercantage * RotationSpeed;

			Rot = Vector3.right * mult;
			PlayerRotationTransform.Rotate(Rot);

			break;



		case ROTATIONS.ROTATE_Z_AXIS:

			mult = Time.deltaTime * rot.mRotPercantage * RotationSpeed;

			Rot = Vector3.forward * mult;
			PlayerRotationTransform.Rotate(-Rot);

			break;

		}
	}
	private RotationVariables UpdateInput(RotationVariables rot)
	{
		rot.mRotPercantage = Input.GetAxis(rot.mInputName);
		// Debug.Log("RotationPerc " + rot.mRotPercantage);
		return rot;
	}
	private void InitializeDirectionController()
	{
		mDirList = new List<DirectionVariables>();
		/*mDirUp = new DirectionVariables(DIRECTIONS.UP, "UpDown", 1);
		mDirDown = new DirectionVariables(DIRECTIONS.DOWN, "UpDown", -1);
		mDirForward = new DirectionVariables(DIRECTIONS.FORWARD, "Vertical", 1);
		mDirBack = new DirectionVariables(DIRECTIONS.BACK, "Vertical", -1);
		mDirLeft = new DirectionVariables(DIRECTIONS.RIGHT, "Horizontal", 1);
		mDirRight = new DirectionVariables(DIRECTIONS.LEFT, "Horizontal", -1);

		mDirList.Add(mDirUp);
		mDirList.Add(mDirDown);
		mDirList.Add(mDirForward);
		mDirList.Add(mDirBack);
		mDirList.Add(mDirLeft);
		mDirList.Add(mDirRight);*/

		mDirX = new DirectionVariables (DIRECTIONS.DIR_X, "Horizontal");
		mDirY = new DirectionVariables (DIRECTIONS.DIR_Y, "UpDown");
		mDirZ = new DirectionVariables (DIRECTIONS.DIR_Z, "Vertical");

		mDirList.Add(mDirX);
		mDirList.Add(mDirY);
		mDirList.Add(mDirZ);
	}
	private void Strafer()
	{
		for (int i = 0; i < mDirList.Count; i++)
		{
			DirectionVariables dir = mDirList[i];
			if (StaticStrafeSpeed)
			{
				
				//dir = CheckStaticInput(dir);
				dir = DynamicStaticInput(dir);
			}
			else if (!StaticStrafeSpeed)
			{
				if (UseSmoothAcceleration)
				{
					dir = CheckSmoothInput(dir);
					//dir = SpeedControlStrafe(dir);
				}
				else
				{
					dir = CheckStepSpeedInput(dir);
					dir = UpdateTimers(dir);
				}
			}

			//dir = SpeedControlStrafe(dir);
			CalculateMovement(dir);

			mDirList[i] = dir;
		}
		move(MovementVector);
		MovementVector = Vector3.zero;
	}

	private DirectionVariables DynamicStaticInput(DirectionVariables dir)
	{
		dir.mCurrentSpeed = Input.GetAxis (dir.mInputName) * StaticSpeed;

		return dir;
	}

	private DirectionVariables CheckStaticInput(DirectionVariables dir)
	{
		
		dir.mCurrentSpeed = StaticSpeed * dir.getPosOrNeg();
		
		return dir;
	}
	private void CalculateMovement(DirectionVariables dir)
	{
		switch (dir.mDir)
		{
		/*
		case DIRECTIONS.UP:
			direction = dir.mCurrentSpeed * Time.deltaTime * PlayerStrafeTransform.up;
			MovementVector += direction;
			//  Debug.Log("Up speed " + direction);

			break;
		case DIRECTIONS.DOWN:
			direction = dir.mCurrentSpeed * Time.deltaTime * PlayerStrafeTransform.up;
			MovementVector -= direction;
			//  Debug.Log("Down speed " + direction);
			break;
		case DIRECTIONS.RIGHT:
			direction = dir.mCurrentSpeed * Time.deltaTime * PlayerStrafeTransform.right;
			MovementVector += direction;
			//  Debug.Log("Right speed " + direction);
			break;
		case DIRECTIONS.LEFT:
			direction = dir.mCurrentSpeed * Time.deltaTime * PlayerStrafeTransform.right;
			MovementVector -= direction;
			//  Debug.Log("Left speed " + direction);
			break;
		case DIRECTIONS.FORWARD:
			direction = dir.mCurrentSpeed * Time.deltaTime * PlayerStrafeTransform.forward;
			MovementVector += direction;
			//   Debug.Log("Forward speed " + direction);
			break;
		case DIRECTIONS.BACK:
			direction = dir.mCurrentSpeed * Time.deltaTime * PlayerStrafeTransform.forward;
			MovementVector -= direction;
			//   Debug.Log("Back speed " + direction);
			break;
			*/
		case DIRECTIONS.DIR_X:
			direction = dir.mCurrentSpeed * Time.deltaTime * PlayerStrafeTransform.right;
		//	Debug.Log ("X dir is " + direction);
			MovementVector += direction;
			//  Debug.Log("Left speed " + direction);
			break;
		case DIRECTIONS.DIR_Y:
			direction = dir.mCurrentSpeed * Time.deltaTime * PlayerStrafeTransform.up;
		//	Debug.Log("Y dir is " + direction);
			MovementVector += direction;
			//   Debug.Log("Forward speed " + direction);
			break;
		case DIRECTIONS.DIR_Z:
			
			direction = dir.mCurrentSpeed * Time.deltaTime * PlayerStrafeTransform.forward;
			MovementVector += direction;
			//   Debug.Log("Back speed " + direction);
			break;
		}
	}
	private void move(Vector3 moveV)
	{
		//Debug.Log ("move vector " + moveV);
		charactercontroller.Move(moveV);
	}
	private DirectionVariables CheckSmoothInput(DirectionVariables dir)
	{/*
		if (dir.ButtonIsDown())
		{
			dir.UpdateInputValue();
			if (dir.mCurrentSpeed < MaxSpeedStrafe * dir.mAxisPercentage)
			{
				dir.mCurrentSpeed += SmoothAcceleration;

			}
			else {
				dir.mCurrentSpeed -= SmoothDeacceleration;
			}
		}
		else
		{
			dir.mCurrentSpeed -= SmoothDeacceleration;
		}

		*/
		if (dir.mCurrentSpeed < MaxSpeedStrafe * Input.GetAxis (dir.mInputName)) {
			Debug.Log ("INCREASE SPEED, current speed " + dir.mCurrentSpeed + " limit: " + MaxSpeedStrafe * Input.GetAxis (dir.mInputName));
			dir.mCurrentSpeed += SmoothAcceleration;

		} else if (dir.mCurrentSpeed > MaxSpeedStrafe * Input.GetAxis (dir.mInputName)) {
			Debug.Log ("DECREASE SPEED, current speed " + dir.mCurrentSpeed + " limit: " + MaxSpeedStrafe * Input.GetAxis (dir.mInputName));
			dir.mCurrentSpeed -= SmoothDeacceleration;

		} 
		return dir;
	}
	private DirectionVariables CheckStepSpeedInput(DirectionVariables dir)
	{
		if (dir.ButtonIsDown())
		{
			dir = CalculateStepSpeedINC(dir);
		}
		else
		{
			dir = CalculateStepSpeedDEC(dir);
		}
		return dir;
	}

	private DirectionVariables CalculateStepSpeedINC(DirectionVariables dir)
	{
		if (dir.mTimeToStepSpeedINC <= 0)
		{
			dir.mCurrentSpeed += SpeedINC;
			dir.mTimeToStepSpeedINC = TimeToSpeedINC;
		}

		return dir;
	}
	private DirectionVariables CalculateStepSpeedDEC(DirectionVariables dir)
	{
		if (dir.mTimeToStepSpeedDEC <= 0)
		{
			dir.mCurrentSpeed -= SpeedDEC;
			dir.mTimeToStepSpeedDEC = TimeToSpeedDEC;
		}

		return dir;
	}
	private DirectionVariables UpdateTimers(DirectionVariables dir)
	{
		dir.mTimeToStepSpeedDEC -= Time.deltaTime;
		dir.mTimeToStepSpeedINC -= Time.deltaTime;
		return dir;
	}
	private DirectionVariables SpeedControlStrafe(DirectionVariables dir)
	{
		if(dir.mCurrentSpeed > MaxSpeedStrafe)
		{
			dir.mCurrentSpeed = MaxSpeedStrafe;
		}
		else if (dir.mCurrentSpeed < MinSpeedStrafe)
		{
			dir.mCurrentSpeed = MinSpeedStrafe;
		}
		return dir;
	}

}
