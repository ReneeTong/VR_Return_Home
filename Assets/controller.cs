using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class controller : MonoBehaviour
{

    public Light light1;
    public Light light2;
    public Light light3;
    public Light light4;

    public score scoreScript;
    private float timeCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;
        if (timeCount >= 3)
        {
            timeCount = 0;
            int number = scoreScript.getLightNumber();
            light1.intensity = 0;
            light2.intensity = 0;
            light3.intensity = 0;
            light4.intensity = 0;
            if (number == 1)
            {
                light1.intensity = 20;
            }
            else if(number == 2)
            {
                light2.intensity = 20;
            }else if(number == 2)
            {
                light3.intensity = 20;
            }
            else
            {
                light4.intensity = 20;
            } 
        }
    }
}
