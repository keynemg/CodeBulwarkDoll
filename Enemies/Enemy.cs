using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entities
{
    public Rigidbody myRigidBody;
    public Animator MyAnimator;
    public NavMeshAgent myNavMeshAgent;
    public int e_Gold_Dropped;

    [Range(0,1)][SerializeField]
    private float damageResistance = 0;

    public override void Awake()
    {
        base.Awake();
        MyAnimator = GetComponent<Animator>();
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        myRigidBody = GetComponent<Rigidbody>();
    }

    public void GoToLocation(Transform Destination)
    {
        if (myNavMeshAgent)
        {
            myNavMeshAgent.SetDestination(Destination.position);
        }
    }

    public override void RecieveDamage(float damage)
    {
        base.RecieveDamage(damage * (1 - damageResistance));
    }

    public override void OnDeath()
    {
        Player_Stats.Instance.p_Current_Gold += e_Gold_Dropped;
        Player_Stats.Instance.AttGoldText();
        Spawn_Waves.Instance.aliveEnemies--;
        Spawn_Waves.Instance.aliveEnemiesTxT.text = Spawn_Waves.Instance.aliveEnemies.ToString();
        base.OnDeath();
        transform.GetChild(1).gameObject.SetActive(false);
        gameObject.GetComponent<Collider>().enabled = false;
        Destroy(gameObject, 2);
    }
}
