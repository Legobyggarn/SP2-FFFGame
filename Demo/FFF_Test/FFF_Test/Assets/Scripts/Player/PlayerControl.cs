using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

    // Character controller
    private CharacterController mCharacterController;

    // Rotation
    private float rotX;
    private float rotY;
    public float rotSpeedX;
    public float rotSpeedY;
    public float rotSpeed;
    public bool noMouse;
    // Oculus direction
    public Transform oculusTransform;
    // Movement
    private float speed;
    public float maxSpeed;
    public float minSpeed;
    public float acceleration;
    public float deacceleration;
    private Vector3 moveDirection;
    private Vector3 lookDireciton;

    // Mouse position
    public float antiJitterX; // ...
    public float antiJitterY; // ...

    // Use this for initialization
    void Start()
    {
       // oculusTransform = transform.Find("CenterEyeAnchor");
        // Rotation
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        // Character controller
        mCharacterController = GetComponent<CharacterController>();

        moveDirection = transform.forward;

    }

    // Update is called once per frame
    void Update()
    {

        /*
        float speed = mSpeed * Time.deltaTime;
        float rot_x_speed = rotSpeedX * Time.deltaTime;
        float rot_y_speed = rotSpeedY * Time.deltaTime;
        */

        /*
        // Rotate camera (Player)
        rotX += -Input.GetAxis("Mouse Y") * rotSpeedX;
        rotY += Input.GetAxis("Mouse X") * rotSpeedY;

        transform.localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        */

        // Rotation
        //Debug.Log("Mouse position: " + Input.mousePosition);
        if (!noMouse) { 
        float ourX = Input.mousePosition.x / Screen.width * 2 - 1;
            float ourY = Input.mousePosition.y / Screen.height * 2 - 1;
            Vector3 ourXYZ = new Vector3(ourX, ourY, 0.0f);
            //Debug.Log("Our position: " + ourXYZ);
            //Debug.Log(ourX);

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
            lookDireciton = oculusTransform.forward;

        }
        // Move
        // If the player is not moving forward, then move forward slowly. (Add a check for direction in x)
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {



            //moveDirection = transform.forward * floatSpeed;
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



            //rotX = Mathf.clamp(rotX, 0, 360);

            //transform.localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
            //moveDirection = Quaternion.Euler(rotX, rotY, 0.0f);
            //moveDirection = transform.forward;
            //moveDirection = transform.forward;
            //moveDirection = moveDirection + lookDireciton;
            //Debug.Log("Move direction: " + moveDirection);

            //moveDirection = moveDirection.normalized;
            //rotSpeed *= Time.deltaTime;
            moveDirection = Vector3.RotateTowards(moveDirection, lookDireciton, rotSpeed * Time.deltaTime, 0.0f);




            if (speed < maxSpeed)
            {
                speed += acceleration * Time.deltaTime;
            }
            else
            {
                speed = maxSpeed;
            }
        }

        //Debug.Log("Horizontal: " + Input.GetAxis("Horizontal"));
        // Set direction
        // moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //moveDirection = transform.forward;
        //moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = moveDirection.normalized;
        //Debug.Log("Move direction: " + moveDirection);
        moveDirection *= speed;



        // Apply gravity...

        // Apply movement
        mCharacterController.Move(moveDirection * Time.deltaTime);

    }

    private void accelerate(float speed, float accelerate)
    {



    }

}
