using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float startTime = 100f;
    private float currentTime;
    public TMP_Text timer; 
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTime > 0f)
        {
            currentTime -= Time.deltaTime;
            updateTimer();
        }
        else{
            currentTime = 0;
        }
    }

    void updateTimer()
    {
        timer.text = currentTime.ToString("F2");
    }
}
