using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    private bool timeStopped = false;
    private string timerToSave;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeStopped)
        {
            return;
        }
        else
        {
            //generates text to timer formatting (mm:ss:milliseconds)
            float t = Time.time - startTime;

            string minutes = ((int)t / 60).ToString();
            string seconds = ((t % 60).ToString("f2"));

            timerText.text = minutes + ":" + seconds;
            timerToSave = timerText.text;
            //Debug.Log(timerToSave);
        }

    }
    public void StopTime()
    {
        timeStopped = true;
    }

    public string getTime()
    {
        if (timerToSave == null)
        {
            Debug.Log("Error: Timer is null");
        }
        //Debug.Log(timerToSave);
        return timerToSave;
    }
}
