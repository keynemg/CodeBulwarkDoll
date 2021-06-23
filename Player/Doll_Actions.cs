using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll_Actions : MonoBehaviour
{
    public int damage = 1;
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.SendMessage("RecieveDamage", damage, SendMessageOptions.DontRequireReceiver);

        }
    }
}
