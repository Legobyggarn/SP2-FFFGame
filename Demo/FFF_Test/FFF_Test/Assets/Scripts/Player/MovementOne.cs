using UnityEngine;
using System.Collections;

public class MovementOne : MonoBehaviour {
    public float MaxSpeed = 10f;
    public float MinSpeed = 0f;
    public float Acceleration = 0.2f;
    public float Deacceleration = 0.2f;
    public float MaxRotationSpeed = 10f;
    public float RotationAcceleration = 1f;
    public float RotationDeacceleration = 1f;
    // Use this for initialization
    public Transform mPlayerTransform;
    public Transform mOculusLookTransform;
    public CharacterController mCharController;
    public float YrotationAxel;
    private float mCurrentYRotationSpeed;
    private Vector3 mMoveDirection;
    private Vector3 mLookDirection;
    public float FlyTowardsCurve;
    public float FlyTowardAxel;
    private float mCurrentFlySpeed;
    public bool EasyMode;
    public KeyCode UpdateLookDirection;
	void Start () {
        mPlayerTransform = gameObject.GetComponent<Transform>();
        mOculusLookTransform = GameObject.Find("CenterEyeAnchor").GetComponent<Transform>();
        mCharController = gameObject.GetComponent<CharacterController>();
        mMoveDirection = mPlayerTransform.forward;
	}
	
	// Update is called once per frame
	void Update () {

        DoRotation();
        Debug.Log("move vector is " + mMoveDirection);

        flyTowards();
    }
    private void flyTowards()
    {   
       // Need to set mMoveDirection start value, bug when it was initialized in Start() function
        if(mMoveDirection == Vector3.zero)
        {
            mMoveDirection = mPlayerTransform.forward;
        }

        // If button is pressed update look function
        if (Input.GetKey(UpdateLookDirection))
        {
            mLookDirection = mOculusLookTransform.forward;
           
        }

       
        if (Input.GetAxis("FlyTowards") > 0f )
        {
            // Update lookDirection
            if (EasyMode) {             
                mLookDirection = mOculusLookTransform.forward;
             
            }

            mMoveDirection = Vector3.RotateTowards(mMoveDirection, mLookDirection, FlyTowardsCurve * Time.deltaTime, 0.0f);
            mMoveDirection = mMoveDirection.normalized;

        }
        
        
        // Calculate speed
        FlyTowardSpeed();
        // Move player
        move();
    }
    private void move()
    {
        Vector3 thaThing = mMoveDirection * mCurrentFlySpeed * Time.deltaTime;
        Debug.Log("move vector is " + thaThing + " " + mCurrentFlySpeed + " " + " " + mMoveDirection);
        mCharController.Move(thaThing);
    }
    private void DoRotation()
    {
        // Calculate rotation speed
        //Debug joystick rotation and rotation speed calculator
        /* Debug.Log("rotation speed is " + mCurrentYRotationSpeed);
         Debug.Log("rotation Axis is " + YrotationAxel);*/
        RotateYSpeed();

        // Rotate Player
        Rotate();
    }
    private void Rotate()
    {
        float mult = Time.deltaTime * mCurrentYRotationSpeed;

        Vector3 Rot = Vector3.up * mult;
        mPlayerTransform.Rotate(Rot);
    }
    public void FlyTowardSpeed()
    {
        FlyTowardAxel = Input.GetAxis("FlyTowards");

        if (0 < FlyTowardAxel && mCurrentFlySpeed  < MaxSpeed)
        {

            mCurrentFlySpeed += Acceleration * Mathf.Abs(FlyTowardAxel) * Time.deltaTime; 

        }       
            // if rotation speed is positive and higher then deaccleration, lower it!
        else  if (mCurrentFlySpeed >  MinSpeed)
        {
        mCurrentFlySpeed -= Deacceleration * Time.deltaTime;
        }
          
        else
        {
            mCurrentFlySpeed = MinSpeed;
        }
        
    }

    public void RotateYSpeed()
    {
        YrotationAxel = Input.GetAxis("Rotate_Y_AXIS");

        if (0 < YrotationAxel && mCurrentYRotationSpeed < MaxRotationSpeed )
        {
         //   Debug.Log("INCREASE SPEED, current speed " + mCurrentYRotationSpeed + " limit: " + MaxRotationSpeed);
            mCurrentYRotationSpeed += RotationAcceleration * Mathf.Abs(YrotationAxel) * Time.deltaTime; // mult absolut( YrotationAxel) för mer smooth acceleration!

        }
        else if (0 > YrotationAxel && mCurrentYRotationSpeed > -MaxRotationSpeed)
        {
          //  Debug.Log ("DECREASE SPEED, current speed " + mCurrentYRotationSpeed + " limit: " + MaxRotationSpeed);
            mCurrentYRotationSpeed -= RotationAcceleration * Mathf.Abs(YrotationAxel) * Time.deltaTime;

        }
        else if(YrotationAxel == 0)
        {
            // if rotation speed is positive and higher then deaccleration, lower it!
            if(mCurrentYRotationSpeed > RotationDeacceleration)
            {
                mCurrentYRotationSpeed -= RotationDeacceleration * Time.deltaTime;
            }
            // if rotation speed is negative and lower then negative deaccleration, increase it!
            else if (mCurrentYRotationSpeed < -RotationDeacceleration)
            {
                mCurrentYRotationSpeed += RotationDeacceleration * Time.deltaTime;
            }
            else
            {
                mCurrentYRotationSpeed = 0;
            }
        }
    }
}
