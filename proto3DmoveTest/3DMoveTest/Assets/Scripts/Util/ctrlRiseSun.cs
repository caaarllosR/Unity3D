using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ctrlRiseSun : MonoBehaviour {

    public float countTimeSunRise;
    public float sunAngle;
    public float sumAngle;
    public Light sunLight;

    // Use this for initialization
    void Start () {

        sunAngle = -150f;
        sumAngle = 1f;
        sunLight = GetComponent<Light>();
    }
	
	// Update is called once per frame
	void Update () {

        sunAngle = transform.eulerAngles.x;
        if (countTimeSunRise >= 0.2f)
        {
            if(transform.eulerAngles.x == 90f)
            {
                sumAngle = -1f;
            }
            if(transform.eulerAngles.x == 270f)
            {
                sumAngle = 1f;
            }
            transform.eulerAngles = new Vector3(transform.eulerAngles.x + sumAngle, 0f, 0f);
            sunLight.intensity += sumAngle * 1f/(sunAngle*4f);
            countTimeSunRise = 0f;
        }
        else
        {
            countTimeSunRise += Time.deltaTime;
        }

    }
}
