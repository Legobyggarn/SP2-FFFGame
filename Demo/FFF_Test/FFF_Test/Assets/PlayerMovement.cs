using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerMovement : MonoBehaviour {

    // STRAFE/DIRECTION Variables
    public Transform PlayerTransform;
    public CharacterController charactercontroller;
    enum DIRECTIONS { UP, DOWN, RIGHT, LEFT, FORWARD, BACK };
    public float MaxSpeed;
    public float MinSpeed;
    public bool StaticStrafeSpeed;
    public float StaticSpeed;
    public bool UseSmoothAcceleration;
    public float SmoothAcceleration;
    public float SmoothDeacceleration;
    public bool BelowIsStepSpeedINC;
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
        }
        public float mCurrentSpeed;
        public float mTimeToStepSpeedINC;
        public float mTimeToStepSpeedDEC;
        private int mInputValue;
        public DIRECTIONS mDir;
        string mInputName;
        public bool ButtonIsDown()
        {
            if(Input.GetAxis(mInputName) == mInputValue)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
         
    }
    List<DirectionVariables> mDirList;

    // ROTATE VARIABLES
    Quaternion ForwardRotation;
    Quaternion LookRotation;
    Quaternion newRotation;
    enum ROTATIONS { ROTATERIGHT, ROTATELEFT, ROTATEFORWARD, ROTATEBACKWARD, SPINRIGHT, SPINLEFT}
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
    RotationVariables mRotRight;
    RotationVariables mRotLeft;
    RotationVariables mRotForward;
    RotationVariables mRotBackward;
    RotationVariables mSpinRight;
    RotationVariables mSpinLeft;
    List<RotationVariables> mRotList;
    void Start () {
        InitializeDirectionController();
        InitializeRotation();
    }

	// Update is called once per frame
	void Update () {

        Strafer();
        Rotation();
    }
    private void InitializeRotation()
    {
        mRotList = new List<RotationVariables>();
        mRotRight = new RotationVariables(ROTATIONS.ROTATERIGHT, "RotateVertical");
        mRotLeft = new RotationVariables(ROTATIONS.ROTATELEFT, "RotateVertical");
        mRotForward = new RotationVariables(ROTATIONS.ROTATEFORWARD, "RotateHorizontal");
        mRotBackward = new RotationVariables(ROTATIONS.ROTATEBACKWARD, "RotateHorizontal");
        mSpinRight = new RotationVariables(ROTATIONS.SPINRIGHT, "Spin");
        mSpinLeft = new RotationVariables(ROTATIONS.SPINLEFT, "Spin");
        mRotList.Add(mRotRight);
        mRotList.Add(mRotLeft);
        mRotList.Add(mRotForward);
        mRotList.Add(mRotBackward);
        mRotList.Add(mSpinRight);
        mRotList.Add(mSpinLeft);
    }
    private void Rotation()
    {
       // newRotation = ForwardRotation.
        //Iterate
        for(int i = 0; i < mRotList.Count; i++)
        {
            RotationVariables rot = mRotList[i];
            rot = UpdateInput(rot);
            UpdateRotation(rot);
            rot = UpdateInput(rot);

            mRotList[i] = rot;
            
        }
        
        




    }
   /* private RotationVariables CalculateRotatePercantage(RotationVariables rot)
    {
        float angle = Vector3.Angle(ForwardDirection.transform.forward, LookDirection.transform.forward);

    }*//*
    private void UpdateRotation(Quaternion Rot)
    {
        PlayerTransform.rotation = Rot;

    }*/
    private void UpdateRotation(RotationVariables rot)
    {
        float mult = 0;
        Vector3 Rot = Vector3.zero;
        switch (rot.mRot)
        {
           
            case ROTATIONS.ROTATERIGHT:
               
                 mult = Time.deltaTime * rot.mRotPercantage * RotationSpeed;
                
                 Rot = Vector3.up * mult;
                PlayerTransform.Rotate(Rot);
               
                break;
            case ROTATIONS.ROTATELEFT:

                 mult = Time.deltaTime * rot.mRotPercantage * RotationSpeed;

                 Rot = Vector3.up * mult;
                PlayerTransform.Rotate(Rot);

                break;
            case ROTATIONS.ROTATEFORWARD:

                 mult = Time.deltaTime * rot.mRotPercantage * RotationSpeed;

                 Rot = Vector3.right * mult;
                PlayerTransform.Rotate(Rot);

                break;
            case ROTATIONS.ROTATEBACKWARD:

                 mult = Time.deltaTime * rot.mRotPercantage * RotationSpeed;

                 Rot = Vector3.right * mult;
                PlayerTransform.Rotate(Rot);

                break;
            case ROTATIONS.SPINRIGHT:

                 mult = Time.deltaTime * rot.mRotPercantage * RotationSpeed;

                 Rot = Vector3.forward * mult;
                PlayerTransform.Rotate(Rot);

                break;
            case ROTATIONS.SPINLEFT:
                 mult = Time.deltaTime * rot.mRotPercantage * RotationSpeed;
                 Rot = Vector3.up * mult;
                 PlayerTransform.Rotate(Rot);

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
        mDirUp = new DirectionVariables(DIRECTIONS.UP, "UpDown", 1);
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
        mDirList.Add(mDirRight);
    }
    private void Strafer()
    {
        for (int i = 0; i < mDirList.Count; i++)
        {
            DirectionVariables dir = mDirList[i];
            if (StaticStrafeSpeed)
            {
                dir = CheckStaticInput(dir);
            }
            else if (!StaticStrafeSpeed)
            {
                if (UseSmoothAcceleration)
                {
                    dir = CheckSmoothInput(dir);
                }
                else
                {
                    dir = CheckStepSpeedInput(dir);
                    dir = UpdateTimers(dir);
                }
            }

            dir = SpeedControl(dir);
            CalculateMovement(dir);
            move(MovementVector);
            mDirList[i] = dir;
        }
        MovementVector = Vector3.zero;
    }
    private DirectionVariables CheckStaticInput(DirectionVariables dir)
    {
        if(dir.ButtonIsDown())
        {
            dir.mCurrentSpeed = StaticSpeed;
        }
        else
        {
            dir.mCurrentSpeed = 0;
        }
        return dir;
    }
    private void CalculateMovement(DirectionVariables dir)
    {
        switch (dir.mDir)
        {
            case DIRECTIONS.UP:
                direction = dir.mCurrentSpeed * Time.deltaTime * PlayerTransform.up;
                MovementVector += direction;
                break;
            case DIRECTIONS.DOWN:
                direction = dir.mCurrentSpeed * Time.deltaTime * PlayerTransform.up;
                MovementVector -= direction;
                break;
            case DIRECTIONS.RIGHT:
                direction = dir.mCurrentSpeed * Time.deltaTime * PlayerTransform.right;
                MovementVector += direction;
                break;
            case DIRECTIONS.LEFT:
                direction = dir.mCurrentSpeed * Time.deltaTime * PlayerTransform.right;
                MovementVector -= direction;
                break;
            case DIRECTIONS.FORWARD:
                direction = dir.mCurrentSpeed * Time.deltaTime * PlayerTransform.forward;
                MovementVector += direction;
                Debug.Log("Forward speed " + dir.mCurrentSpeed);
                break;
            case DIRECTIONS.BACK:
                direction = dir.mCurrentSpeed * Time.deltaTime * PlayerTransform.forward;
                MovementVector -= direction;
                break;
        }
    }
    private void move(Vector3 moveV)
    {
        charactercontroller.Move(moveV);
    }
    private DirectionVariables CheckSmoothInput(DirectionVariables dir)
    {
        if (dir.ButtonIsDown())
        {
            dir.mCurrentSpeed += SmoothAcceleration;
        }
        else
        {
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
    private DirectionVariables SpeedControl(DirectionVariables dir)
    {
        if(dir.mCurrentSpeed > MaxSpeed)
        {
            dir.mCurrentSpeed = MaxSpeed;
        }
        else if (dir.mCurrentSpeed < MinSpeed)
        {
            dir.mCurrentSpeed = MinSpeed;
        }
        return dir;
    }
   
}
