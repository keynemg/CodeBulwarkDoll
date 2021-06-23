using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    public Transform target_Camera;

    void Update()
    {
        transform.LookAt(target_Camera);
        Quaternion canvasrotation = transform.rotation;
        canvasrotation.y = 0;
        canvasrotation.z = 0;
        transform.rotation = canvasrotation;
    }
}
