
using UnityEngine;
using System.Collections;


public class firstPersonCam : MonoBehaviour
{
    Quaternion localRotation;
    Vector3 rot;
    private Transform uChanTrans;
    private inventoryScript invScript;
    private uChanController uChanControl;
    private GameObject uChan;

    public float cameraMoveSpeed;
    public float inputSensitivity;
    public float mouseY;
    public float inputY;
    public float rotX;
    public float uChanRotX;
    public float uChanRotY;
    public float clampAngleX1;
    public float clampAngleX2;
    private bool resetAngleFirstCam;

    void Start()
    {
        uChan = GameObject.FindWithTag("Player"); //achar o player
        uChanControl = uChan.GetComponent<uChanController>();

        resetAngleFirstCam = true;
        uChanTrans = gameObject.transform.parent;
        cameraMoveSpeed = 120.0f;
        inputSensitivity = 150.0f;
        rotX = 0.0f;
        rot = transform.localRotation.eulerAngles;
        rotX = rot.x;
        clampAngleX1 = -24.5f;
        clampAngleX2 = 60.0f;
        invScript = GetComponentInParent<inventoryScript>();
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }


    public bool ResetAngleFirstCam
    {
        get
        {
            return resetAngleFirstCam;
        }

        set
        {
            resetAngleFirstCam = value;
        }
    }


    private void Update()
    {

        if (uChanControl.OnLantern == true && !resetAngleFirstCam)
        {
            localRotation = Quaternion.Euler(uChan.transform.eulerAngles);
            transform.rotation = localRotation;
            resetAngleFirstCam = true;
        }

        if (!invScript.inventoryOn)
        {
            uChanRotY = uChanTrans.transform.localEulerAngles.y;
            uChanRotX = uChanTrans.transform.localEulerAngles.x;


            if (Input.GetAxis("RightStickVertical") != 0.0f)
            {
                inputY = Input.GetAxis("RightStickVertical");
                rotX += inputY * inputSensitivity * Time.deltaTime;

                rotX = Mathf.Clamp(rotX, clampAngleX1, clampAngleX2);
                localRotation = Quaternion.Euler(rotX + uChanRotX, transform.localEulerAngles.y + uChanRotY, 0.0f);
                transform.rotation = localRotation;
            }
            else if (Input.GetMouseButton(0))
            {
                mouseY = Input.GetAxis("Mouse Y");
                rotX += mouseY * inputSensitivity * Time.deltaTime;

                rotX = Mathf.Clamp(rotX, clampAngleX1, clampAngleX2);
                localRotation = Quaternion.Euler(rotX + uChanRotX, transform.localEulerAngles.y + uChanRotY, 0.0f);
                transform.rotation = localRotation;
            }
        }
    }
}

