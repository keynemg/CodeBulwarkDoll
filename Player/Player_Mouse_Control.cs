using UnityEngine;
using System.Collections;

public class Player_Mouse_Control : MonoBehaviour
{
    private static Player_Mouse_Control instance;
    public static Player_Mouse_Control Instance { get { return instance; } }

    public float speed;
    [System.NonSerialized]
    public Vector3 targetPoint;

    private void Awake()
    {
        instance = this;
    }


    void FixedUpdate()
    {
        Plane playerPlane = new Plane(Vector3.up,Vector3.zero);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float hitdist = 0.0f;
        if (playerPlane.Raycast(ray, out hitdist))
        {
            targetPoint = ray.GetPoint(hitdist);

            iTween.LookUpdate(gameObject, iTween.Hash(
                "looktarget", targetPoint,
                "axis","y",
                "time ",0.5f
                ));
        }
    }
}