using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Enemy_Hit : MonoBehaviour
{
    public float damage;
    public string PlayerTag;
    public string playerBaseTag;

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(PlayerTag) || col.CompareTag(playerBaseTag))
        {
            if (col.CompareTag(PlayerTag))
            {
                Player_Stats.Instance.RecieveDamage(damage);
            }
            else
            {
            PlayerBase.Instance.GetComponent<PlayerBase>().ReceiveDamage(damage);
            }
        }
    }
}
