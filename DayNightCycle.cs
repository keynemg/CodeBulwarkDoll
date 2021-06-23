using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    Vector3[] sun_Positions = new Vector3[4] { new Vector3(30, -90, 0), new Vector3(60, -180, 0), new Vector3(30, 90, 0), new Vector3(-60, 0, 0) };
    int dayTime = 0;

    // Start is called before the first frame update
    void Awake()
    {
        dayTime = 1;
        CallPassTime();
    }

    public void PassTime()
    {
        if (dayTime < 3)
        {
            dayTime++;
        }
        else
        {
            dayTime = 0;
        }

        float timer = 30;

        iTween.RotateTo(gameObject, iTween.Hash(
            "rotation", sun_Positions[dayTime],
            "time", timer,
            "easetype", "linear",
            "oncomplete", "CallPassTime",
            "oncompletetarget", gameObject));

        /*  iTween.ColorTo(sun, iTween.Hash(
            "color", sun_Colors[dayTime],
            "time", 240f,
            "easetype", "linear"));    */

    }

    public void CallPassTime()
    {
        PassTime();
    }

}
