using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            this.transform.position = new Vector3(0, 0, 0);
            Debug.Log("pressed alpha0");
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            this.transform.position = new Vector3(85, 0, 10);
            Debug.Log("pressed alpha9");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (UnityEditor.EditorApplication.isPlaying)
            {
                UnityEditor.EditorApplication.isPlaying = false;
            }
            else
            {
                Application.Quit();
            }
        }
    }
}
