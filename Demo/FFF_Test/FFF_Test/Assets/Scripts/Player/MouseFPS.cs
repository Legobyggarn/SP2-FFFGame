using UnityEngine;
using System.Collections;

public class MouseFPS : MonoBehaviour {


    // Character controller
    private CharacterController mCharacterController;

    // Rotation
    private float rotX;
    private float rotY;
    public float rotSpeedX;
    public float rotSpeedY;
    public float rotSpeed;


    // Use this for initialization
    void Start()
    {
        
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        // Character controller
        mCharacterController = GetComponent<CharacterController>();


    }

    // Update is called once per frame
    void Update()
    {

        // Rotate camera (Player)
        rotX += -Input.GetAxis("Mouse Y") * rotSpeedX;
        rotY += Input.GetAxis("Mouse X") * rotSpeedY;
		//Vector3 currentAngles = transform.eulerAngles;
		//currentAngles.x += rotX;
		//currentAngles.y += rotY;
        transform.localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
		//transform.localRotation = Quaternion.Euler(currentAngles);
      

    }

    

}
