using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]

public class uChanController : MonoBehaviour
{
    Quaternion localRotation;
    Quaternion refRotation;

    private int life; // vida do personagem
    private const int lifeMax = 5; //Vida maxima do personagem
    private int hunger; //pontuação de fome
    private int thirst; //pontuação asede
    private int lanternCatch; //informe se a lanterna já foi pega

    private float animSpeed; //velocidade da animação
    private float dirAnim;
    private float forwardSpeed;
    private float backwardSpeed;
    private float rotateSpeed;
    private float delayCount;
    private const float velUchanRot = 10;

    private bool spawned;
    private bool onLantern;

    private GameObject camOrbit;
    public GameObject thirdCam;
    private GameObject lantern;
    private GameObject firstCamOrbit;
    private Light lanternLight;
    private Transform camOrbitTrans;
    private thirdPersonCam camOrbitControl;
    private firstPersonCam firstCamControl;

    public CapsuleCollider col;
    private Rigidbody rb;
    private Vector3 velocity;
    private float h, v;

    private Animator anim;
    private AnimatorStateInfo currentBaseState;

    private inventoryScript invScript;
    private float delayButton;
    private const float intensityLantern = 10;

    void Start()
    {
        dirAnim = 0;
        animSpeed = 1.5f;
        forwardSpeed = 7.0f;
        backwardSpeed = 2.0f;
        rotateSpeed = 2.0f;
        life = 1;
        delayCount = 10.0f;
        spawned = false;
        onLantern = false;
        Hunger = 0;
        Thirst = 0;
        LanternCatch = 1;

        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        invScript = GetComponent<inventoryScript>();


        camOrbit = GameObject.FindWithTag("camOrbit"); //achar o player
        camOrbitControl = camOrbit.GetComponent<thirdPersonCam>(); //achar o player
        camOrbitTrans = camOrbit.GetComponent<Transform>();

        firstCamOrbit = GameObject.FindWithTag("firstCamOrbit"); //achar o player   
        firstCamControl = firstCamOrbit.GetComponent<firstPersonCam>(); //achar o player

        lantern = GameObject.FindWithTag("Lantern");
        lanternLight = lantern.GetComponent<Light>();
        lanternLight.intensity = 0;

        onLantern = false;
        lanternLight.intensity = 0;
        firstCamControl.ResetAngleFirstCam = true;
        //camOrbitControl.enabled = true;
        camOrbit.SetActive(true);
        firstCamControl.enabled = false;
        firstCamOrbit.SetActive(false);
    }


    public int Life
    {
        get
        {
            return life;
        }

        set
        {
            life = value;
        }
    }


    public int LifeMax
    {
        get
        {
            return lifeMax;
        }
    }

    public int Hunger
    {
        get
        {
            return hunger;
        }

        set
        {
            hunger = value;
        }
    }

    public int Thirst
    {
        get
        {
            return thirst;
        }

        set
        {
            thirst = value;
        }
    }

    public int LanternCatch
    {
        get
        {
            return lanternCatch;
        }

        set
        {
            lanternCatch = value;
        }
    }

    public bool OnLantern
    {
        get
        {
            return onLantern;
        }

        set
        {
            onLantern = value;
        }
    }


    private void resetButtonSelectR()
    {
        if (spawned)
        {
            delayButton -= Time.deltaTime;

            if (Input.GetButton("SelectR"))
            {
                spawned = true;
            }
            else if (delayButton < 0)
            {
                delayButton = 0;
                spawned = false;
            }
        }
    }


    private void run(float h, float v, float v2, float dirAnim)
    {
        if ((Mathf.Abs(v) > 0) || (Mathf.Abs(h) > 0))
        {
            dirAnim = 1f;
        }
        else
        {
            dirAnim = 0;
        }

        anim.SetFloat("Speed", dirAnim);
        anim.speed = animSpeed;
        currentBaseState = anim.GetCurrentAnimatorStateInfo(0);

        if (v == 0f && h == 0f)
        {
            v = 0f;
            h = 0f;
        }
        else
        {
            if (((v >= 0f) && (Mathf.Abs(v) <= 0.009)) && h > 0f)
            {
                refRotation = Quaternion.Euler(0, 90.0f + camOrbitTrans.eulerAngles.y, 0.0f);
                localRotation = refRotation;

                velocity.Set(0, 0, -v + h);
                velocity = transform.TransformDirection(velocity);
                velocity *= Time.fixedDeltaTime * v2;
                transform.localPosition += velocity;
            }
            else if (((v >= 0f) && (Mathf.Abs(v) <= 0.009)) && h < 0f)
            {
                refRotation = Quaternion.Euler(0, -90.0f + camOrbitTrans.eulerAngles.y, 0.0f);
                localRotation = refRotation;

                velocity.Set(0, 0, v - h);
                velocity = transform.TransformDirection(velocity);
                velocity *= Time.fixedDeltaTime * v2;
                transform.localPosition += velocity;
            }
            else if (((h >= 0f) && (Mathf.Abs(h) <= 0.009)) && v > 0f)
            {
                refRotation = Quaternion.Euler(0.0f, 0.0f + camOrbitTrans.eulerAngles.y, 0.0f);
                localRotation = refRotation;

                velocity.Set(0, 0, v + h);
                velocity = transform.TransformDirection(velocity);
                velocity *= Time.fixedDeltaTime * v2;
                transform.localPosition += velocity;
            }
            else if (((h >= 0f) && (Mathf.Abs(h) <= 0.009)) && v < 0f)
            {
                refRotation = Quaternion.Euler(0, 180.0f + camOrbitTrans.eulerAngles.y, 0.0f);
                localRotation = refRotation;

                velocity.Set(0, 0, -v + h);
                velocity = transform.TransformDirection(velocity);
                velocity *= Time.fixedDeltaTime * v2;
                transform.localPosition += velocity;
            }
            else if (v > 0f && h > 0f)
            {
                refRotation = Quaternion.Euler(0, Mathf.Abs((v * 90f) - 90f) + camOrbitTrans.eulerAngles.y, 0.0f);
                localRotation = refRotation;

                velocity.Set(0, 0, v + h);
                velocity = transform.TransformDirection(velocity);
                velocity *= Time.fixedDeltaTime * v2;
                transform.localPosition += velocity;
            }
            else if (v > 0f && h < 0f)
            {
                refRotation = Quaternion.Euler(0, Mathf.Abs((h * 90f) + 360f) + camOrbitTrans.eulerAngles.y, 0.0f);
                localRotation = refRotation;

                velocity.Set(0, 0, v - h);
                velocity = transform.TransformDirection(velocity);
                velocity *= Time.fixedDeltaTime * v2;
                transform.localPosition += velocity;
            }
            else if (v < 0f && h < 0f)
            {
                refRotation = Quaternion.Euler(0, Mathf.Abs((h * 90f) - 180f) + camOrbitTrans.eulerAngles.y, 0.0f);
                localRotation = refRotation;

                velocity.Set(0, 0, -v - h);
                velocity = transform.TransformDirection(velocity);
                velocity *= Time.fixedDeltaTime * v2;
                transform.localPosition += velocity;
            }
            else if (v < 0f && h > 0f)
            {
                refRotation = Quaternion.Euler(0, Mathf.Abs((v * 90f) - 90f) + camOrbitTrans.eulerAngles.y, 0.0f);
                localRotation = refRotation;

                velocity.Set(0, 0, -v + h);
                velocity = transform.TransformDirection(velocity);
                velocity *= Time.fixedDeltaTime * v2;
                transform.localPosition += velocity;
            }
            transform.rotation = localRotation;
        }

        Debug.Log(camOrbitTrans.eulerAngles.y.ToString() + "   " + h.ToString() + "   " + v.ToString());
    }

    private void countHungerThirst() //contador de fome e sede
    {
        delayCount -= Time.deltaTime;

        if (delayCount <= 0)
        {
            if ((Hunger >= 100) || (Thirst >= 100))
            {
                delayCount = 10.0f;
                Life--;
            }
            else
            {
                delayCount = 10.0f;
                Hunger++;
                Thirst++;
            }
        }
    }


    private void dead()
    {
        if (Life <= 0)
        {
            SceneManager.LoadScene("test");// retornando a scena inicial ao morrer
        }
    }


    private void lanternActivy() //ativa desativa a lanterna
    {
        if (Input.GetButton("SelectR") && !spawned)
        {
            delayButton = 0.2f; //controla delay por segundo emq ue se pode apertar botão
            spawned = true; //Enquanto for verdadeiro botão não volta a funcionar

            if (LanternCatch > 0)
            {
                if (!onLantern)
                {
                    onLantern = true;
                    lanternLight.intensity = intensityLantern;
                    firstCamControl.ResetAngleFirstCam = false;
                    //camOrbitControl.enabled = false;
                    thirdCam.SetActive(false);
                    firstCamControl.enabled = true;
                    firstCamOrbit.SetActive(true);
                }
                else
                {
                    onLantern = false;
                    lanternLight.intensity = 0;
                    firstCamControl.ResetAngleFirstCam = true;
                    //camOrbitControl.enabled = true;
                    thirdCam.SetActive(true);
                    firstCamControl.enabled = false;
                    firstCamOrbit.SetActive(false);
                }
            }
        }
        resetButtonSelectR();
    }

    private void run(float h, float v)
    {
        anim.SetFloat("Speed", v);
        anim.SetFloat("Direction", h);
        anim.speed = animSpeed;
        currentBaseState = anim.GetCurrentAnimatorStateInfo(0);

        velocity.Set(0, 0, v);
        velocity = transform.TransformDirection(velocity);

        if (v > 0.1)
        {
            velocity *= forwardSpeed;
        }
        else if (v < -0.1)
        {
            velocity *= backwardSpeed;
        }

        transform.localPosition += velocity * Time.fixedDeltaTime;
        transform.Rotate(0, h * rotateSpeed, 0);
    }

    void Update()
    {
        lanternActivy();
        dead();
        countHungerThirst();
        rb.useGravity = true;

        if (!invScript.inventoryOn)
        {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
        }
        else
        {
            h = 0;
            v = 0;
        }

        if (!onLantern)
        {
            anim.SetFloat("Direction", 0f);
            run(h, v, 6.5f, dirAnim);
        }
        else
        {
            run(h, v);
        }
    }
}