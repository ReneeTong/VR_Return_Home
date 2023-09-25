using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class score : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUI;
    private int scoreCount;
    private int lightNumber;

    // Start is called before the first frame update
    void Start()
    {
        textMeshProUI = GetComponent<TextMeshProUGUI>();
        scoreCount = 0;
        Debug.Log("get the object");
        
    }

    // Update is called once per frame
    void Update()
    {
        //textMeshProUI.text = "Your Score: " + scoreCount;
    }

    public void addScore()
    {
        scoreCount++;
        textMeshProUI.text = "Your Score: " + scoreCount;
        lightNumber = Random.Range(1, 5);

        Debug.Log("score++");
        Debug.Log(lightNumber);
    }

    public int getLightNumber()
    {
        return lightNumber;
    }

}
