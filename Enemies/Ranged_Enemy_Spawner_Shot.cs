using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged_Enemy_Spawner_Shot : MonoBehaviour
{
    public int shotspeed;
    public Transform origin;
    public GameObject bullet;

    public void Shooting(int _dmg)
    {
        Vector3 direction = origin.position - transform.position;
        GameObject projectile = Pooling.PoolThis(bullet,transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().velocity = -direction * shotspeed;
        projectile.GetComponent<Ranged_Enemy_Shot>().damage = _dmg;
    }
}
