using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationOverTime : MonoBehaviour
{
    public int rotation1;
    public int rotation2;
    public int rotation3;

    void FixedUpdate()
    {
        transform.Rotate(rotation1, rotation2 , rotation3);
    }
}
