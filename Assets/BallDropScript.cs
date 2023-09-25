using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDropScript : MonoBehaviour
{
    public GameObject ball;

    void OnTriggerEnter(Collider other)
    {
        Rigidbody rigidbody = ball.GetComponent<Rigidbody>();
        rigidbody.useGravity = true;
        Debug.Log("trigger");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
