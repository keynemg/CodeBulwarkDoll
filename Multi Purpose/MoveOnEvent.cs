using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnEvent : MonoBehaviour
{
    public void MoveToPosition(Transform target)
    {
        iTween.MoveTo(gameObject, target.position, 2);
    }

}
