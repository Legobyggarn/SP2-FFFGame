using UnityEngine;
using System.Collections;

public class BirthdayBoy : MonoBehaviour {
    public float normalSpeed;
    public float autoSpeedChange;
    public float maxSpeed;
    public float deacceleration;
    public float acceleration;
    public float rotSpeed;
    public Transform oculusTransform;
    private float currentSpeed;
    private Vector3 currentDirection;
    private Vector3 lookDirection;
    private CharacterController characterController;
    private float axisThreshold;
	// Use this for initialization
	void Start () {
        // Initiera oculusTransform
        // oculusTransform = gameObject.Find("centerEyeAnchor").transform;
        axisThreshold = 0.2f;
        currentDirection = transform.forward;
        characterController = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        UpdateSpeed();
        move();
    }
    private void UpdateSpeed()
    {
       // Debug.Log("Inpute Axis Check " + Input.GetAxis("FlyTowards"));
        if (Input.GetAxis("FlyTowards") != 0)
        {
            // Update the direction the player is flying towards.
            lookDirection = oculusTransform.forward;
            currentDirection = Vector3.RotateTowards(currentDirection, lookDirection, rotSpeed * Time.deltaTime, 0.0f);

            // Check if the player is accelerating or deaccelerating
            if (Input.GetAxis("FlyTowards") > axisThreshold)
            {
                //Debug.Log("Update speed, increase it!");
                currentSpeed += acceleration * Time.deltaTime * Input.GetAxis("FlyTowards");
                if(currentSpeed > maxSpeed)
                {
                    currentSpeed = maxSpeed;
                
                }
            }
            else if (Input.GetAxis("FlyTowards") < -axisThreshold)
            {
                //Debug.Log("Update speed, lower it!");
                currentSpeed -= deacceleration * Time.deltaTime * Input.GetAxis("FlyTowards");
            }
        }
        else
        {
            float dif = currentSpeed - normalSpeed;
           // Debug.Log("CurrentSpeed is now " + currentSpeed);
            if (currentSpeed > normalSpeed + autoSpeedChange * Time.deltaTime)
            {
                currentSpeed -= autoSpeedChange * Time.deltaTime;
              //  Debug.Log("CurrentSpeed is now " + currentSpeed + " and was lowered by " + autoSpeedChange * Time.deltaTime);
            }
            else if (currentSpeed < (normalSpeed - autoSpeedChange * Time.deltaTime))
            {
                currentSpeed += autoSpeedChange * Time.deltaTime;
              //  Debug.Log("CurrentSpeed is now " + currentSpeed + " and was increased by " + autoSpeedChange * Time.deltaTime);
            }
            else
            {
              // Debug.Log("CurrentSpeed is now " + currentSpeed + " and was lowered by " + autoSpeedChange * Time.deltaTime);
                currentSpeed = normalSpeed;
            }
        }
    }
    public void move()
    {
        
        characterController.Move(currentDirection * Time.deltaTime * currentSpeed);
    }
    public float getSpeed()
    {
        return currentSpeed;
    }
}
