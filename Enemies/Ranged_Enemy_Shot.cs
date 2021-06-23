using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged_Enemy_Shot : MonoBehaviour
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
        if (!col.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
        }
    }
}
