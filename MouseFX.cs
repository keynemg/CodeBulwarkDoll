using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFX : MonoBehaviour
{



    private void Update()
    {
        Vector3 screenPoint = Input.mousePosition;
        screenPoint.z = 10.0f;
        transform.GetChild(1).position = Camera.main.ScreenToWorldPoint(screenPoint);
        if (Input.GetMouseButtonDown(0))
        {
            MouseVFX(screenPoint);
        }   
    }


    public void MouseVFX(Vector3 _ScreenPoint)
    {
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            if (!transform.GetChild(0).GetChild(i).GetComponent<ParticleSystem>().isPlaying)
            {

                transform.GetChild(0).GetChild(i).position = Camera.main.ScreenToWorldPoint(_ScreenPoint);
                transform.GetComponent<AudioSource>().Play();
                transform.GetChild(0).GetChild(i).GetComponent<ParticleSystem>().Play();
                return;
            }
        }
    }

    public void ButtonClickSFX(AudioClip _SFX)
    {
        transform.GetComponent<AudioSource>().PlayOneShot(_SFX);
    }
}
