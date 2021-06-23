using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggro : MonoBehaviour
{
    public Transform target;

    private void Start()
    {
        target = null;
    }

    private void OnTriggerEnter(Collider other)
    {
            if (other.CompareTag("Player"))
            {
                target = other.transform;
            }
    }

    private void OnTriggerExit(Collider other)
    {
            if (other.CompareTag("Player"))
            {
            target = null;
            }
    }
}
