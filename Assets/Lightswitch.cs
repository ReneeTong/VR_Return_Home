using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightswitch : MonoBehaviour
{
    public Light light;
    private Color[] colorSet = { Color.blue, Color.green, Color.red, Color.white, Color.black, Color.yellow };
    private int colorIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (colorIndex+1 > colorSet.Length) colorIndex = 0;
            light.color = colorSet[colorIndex];
            colorIndex++;
        }
            
        
    }
}
