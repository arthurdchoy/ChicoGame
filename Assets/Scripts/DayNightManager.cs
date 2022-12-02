using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.Experimental.Rendering.Universal;

public class DayNightManager : MonoBehaviour
{
    public Gradient lightColor;
    public float tick = 1;
    public float seconds = 0; 
    public int mins = 0;
    public int minsPerDay = 1440;
    public int dayTime = 360;
    public int nightTime = 1200;
    public bool isday = false;
    public int days = 1;
    // Start is called before the first frame update
    void Start()
    {
        changeColor();
        
    }

    // Update is called once per frame
    void Update()
    {
        timeGoes();
        if (Input.GetKeyDown(KeyCode.Y)) days++;
    }
    private void changeColor()
    {
        float timePercent = (float)mins/(float)minsPerDay;
        gameObject.GetComponent<Light2D>().color = lightColor.Evaluate(timePercent);
    }
    private void timeGoes()
    {
        seconds += Time.fixedDeltaTime * tick; 
        if (seconds >= 60) // 60 sec = 1 min
        {
            seconds = 0;
            mins += 1;
            changeColor();
        }
        if(mins==dayTime)
        {
            isday = true;
        }
        if(mins == nightTime)
        {
            isday = false;
        }
        if(mins == minsPerDay)
        {
            mins = 0;
            days++;
        }
    }
}
