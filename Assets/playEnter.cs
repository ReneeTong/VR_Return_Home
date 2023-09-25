using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playEnter : MonoBehaviour
{
    public score scoreScript;
    public Light light;
    private float timeCount;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        scoreScript.addScore();
        light.intensity = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCount+= Time.deltaTime;
        if(timeCount >= 3)
        {
            timeCount = 0;
            int number = scoreScript.getLightNumber();
        }
        
    }
}
