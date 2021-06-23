using UnityEngine;


public class Enemy_Controller : Enemy
{

    public GameObject spawner;

    public Transform target;

    public float dmg;

    public EnemyAggro aggro_Zone;

    void FixedUpdate()
    {
        if (IsAlive)
        {
            if (aggro_Zone.target)
            {
                myNavMeshAgent.SetDestination(aggro_Zone.target.position);
            }
            else
            {
                myNavMeshAgent.SetDestination(PlayerBase.Instance.transform.position);
            }

            if (myNavMeshAgent.destination != null)
            {
                MyAnimator.SetFloat("MovSpeed", myNavMeshAgent.velocity.magnitude);
                MyAnimator.SetFloat("DistanceToTarget", Vector3.Distance(transform.position, myNavMeshAgent.destination));
                if (Vector3.Distance(transform.position, myNavMeshAgent.destination) <= myNavMeshAgent.stoppingDistance)
                {
                    MyAnimator.SetBool("AttackTarget", true);
                }
                else
                {
                    MyAnimator.SetBool("IsMoving", true);
                }
            }
        }
    }

    public override void RecieveDamage(float damage)
    {
        base.RecieveDamage(damage);
        if (currentHp > 0)
        {
            MyAnimator.SetTrigger("Staggered");
        }
    }

    public override void OnDeath()
    {
        IsAlive = false;
        DeathEvent.Invoke();
        Player_Stats.Instance.p_Current_Gold += e_Gold_Dropped;
        Player_Stats.Instance.AttGoldText();
        Spawn_Waves.Instance.aliveEnemies--;
        Spawn_Waves.Instance.aliveEnemiesTxT.text = Spawn_Waves.Instance.aliveEnemies.ToString();
        transform.GetChild(4).GetChild(1).gameObject.SetActive(false);
        transform.GetChild(4).GetChild(2).gameObject.SetActive(false);
        transform.GetChild(4).GetChild(4).gameObject.SetActive(false);
        gameObject.GetComponent<Collider>().enabled = false;
        Destroy(gameObject, 3);
        myNavMeshAgent.speed = 0;
        MyAnimator.SetTrigger("IsDead");
        gameObject.GetComponent<Enemy_Controller>().enabled = false;
    }


    public void Shoot()
    {
        spawner.GetComponent<Ranged_Enemy_Spawner_Shot>().Shooting((int)dmg);
    }

}
