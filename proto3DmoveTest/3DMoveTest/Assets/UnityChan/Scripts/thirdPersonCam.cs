using UnityEngine;
using System.Collections;


public class thirdPersonCam : MonoBehaviour
{
    Quaternion localRotation;
    Vector3 rot;
    private Transform parentChanTrans;
    private inventoryScript invScript;

    private uChanController uChanControl;
    private firstPersonCam firstCamControl;
    private GameObject uChan;
    private GameObject firstCam;

    public float cameraMoveSpeed;
    public float inputSensitivity;
    public float mouseX;
    public float mouseY;
    public float inputX;
    public float inputY;
    public float rotX;
    public float rotY;
    public float uChanRotX;
    public float uChanRotY;
    public float clampAngleX1;
    public float clampAngleX2;
    public float clampAngleY1;
    public float clampAngleY2;

    void Awake()
    {

        uChan = GameObject.FindWithTag("Player"); //achar o player
        uChanControl = uChan.GetComponent<uChanController>();
        firstCam = GameObject.FindWithTag("firstCamOrbit");
        firstCamControl = firstCam.GetComponent<firstPersonCam>();

        parentChanTrans = gameObject.transform.parent;

        cameraMoveSpeed = 120.0f;
        inputSensitivity = 150.0f;
        rotX = 0.0f;
        rotY = 0.0f;
        rot = transform.localRotation.eulerAngles;
        rotX = rot.x;
        rotY = rot.y;
        clampAngleX1 = -12.5f;
        clampAngleX2 = 70.0f;
        clampAngleY1 = -180.0f;
        clampAngleY2 = 180.0f;
        invScript = uChan.GetComponentInParent<inventoryScript>();
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }


    private void Update()
    {
        if (!invScript.inventoryOn && !uChanControl.OnLantern)
        {
            transform.position = new Vector3(uChan.transform.position.x, transform.position.y, uChan.transform.position.z);

            uChanRotY = parentChanTrans.transform.localEulerAngles.y;
            uChanRotX = parentChanTrans.transform.localEulerAngles.x;

            if (Input.GetAxis("RightStickHorizontal") != 0.0f)
            {
                inputX = Input.GetAxis("RightStickHorizontal");
                rotY += inputX * inputSensitivity * Time.deltaTime;            
            }
            else if (Input.GetMouseButton(0))
            {
                mouseX = Input.GetAxis("Mouse X");
                rotY += mouseX * inputSensitivity * Time.deltaTime;            
            }

            if (Input.GetAxis("RightStickVertical") != 0.0f)
            {
                inputY = Input.GetAxis("RightStickVertical");
                rotX += inputY * inputSensitivity * Time.deltaTime;
            }
            else if (Input.GetMouseButton(0))
            {
                mouseY = Input.GetAxis("Mouse Y");
                rotX += mouseY * inputSensitivity * Time.deltaTime;               
            }

            rotX = Mathf.Clamp(rotX, clampAngleX1, clampAngleX2);
            localRotation = Quaternion.Euler(rotX + uChanRotX, transform.localEulerAngles.y + uChanRotY, 0.0f);
            transform.rotation = localRotation;

            localRotation = Quaternion.Euler(transform.localEulerAngles.x, rotY + uChanRotY, 0.0f);
            transform.rotation = localRotation;
        }
        else if(uChanControl.OnLantern)
        {
            rotY = firstCamControl.uChanRotY;
            localRotation = Quaternion.Euler(transform.localEulerAngles.x, rotY + uChanRotY, 0.0f);
            transform.rotation = localRotation;
        }
    }
}


