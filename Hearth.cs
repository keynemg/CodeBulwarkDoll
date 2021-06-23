using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearth : MonoBehaviour
{
    int maxHP;
    int currentHP;

    public Transform hpGroup;

    void Start()
    {
        maxHP = hpGroup.transform.childCount;
    }



    void AttLife()
    {
        for (int i = 0; i < hpGroup.transform.childCount; i++)
        {
            hpGroup.GetChild(i).GetChild(0).gameObject.SetActive(false);
            if (currentHP > i)
            {
                hpGroup.GetChild(i).GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
