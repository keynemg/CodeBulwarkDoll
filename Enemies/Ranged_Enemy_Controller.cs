using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Ranged_Enemy_Controller : Enemy
{
    public GameObject spawner;
    public float counter;

    public bool Attacking;

    void Start()
    {
        myNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {        
        if (myNavMeshAgent.destination != null)
        {
            transform.LookAt(myNavMeshAgent.destination);

            if (Vector3.Distance(transform.position, myNavMeshAgent.destination) <= myNavMeshAgent.stoppingDistance)
            {
                if (counter <= 0)
                {
                   // spawner.GetComponent<Ranged_Enemy_Spawner_Shot>().Shooting(dmg);
                    //counter = atkspeed;
                }

                counter -= Time.deltaTime;
            }
        }
    }

    public override void OnDeath()
    {
        base.OnDeath();

        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        gameObject.GetComponent<Ranged_Enemy_Controller>().enabled = false;
        transform.GetChild(5).gameObject.SetActive(false);
    }
}
