using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    //movement and dash, floats && bools:
    public float x;
    public float z;
    public float walkSpeed;
    public float dashSpeed;
    public float endDash;
    public float dashCooldown;
    public bool dashing, canDash;

    public GameObject dashFX;

    //RigidBody
    public Rigidbody RB;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
        dashing = false;
        canDash = true;
    }

    void Update()

    {
        //Movement Controls

        if (dashing == false)
        {
            x = Input.GetAxisRaw("Horizontal");
            z = Input.GetAxisRaw("Vertical");
            Vector3 mov = new Vector3(x, 0, z).normalized*walkSpeed;
            mov.y = RB.velocity.y;
            RB.velocity = mov;
        }

        //DashControls

        if (Input.GetKeyDown(KeyCode.Z) && canDash == true  && (x+z != 0) && Player_Stats.Instance.CheckSP() >= 50)
        {
            Player_Stats.Instance.RecieveSP(-40);

            dashing = true;
            dashFX.GetComponent<ParticleSystem>().Play();
            canDash = false;

            float speedY = RB.velocity.y;
            RB.velocity = new Vector3(x, RB.velocity.y, z).normalized * dashSpeed;
            RB.velocity = new Vector3(RB.velocity.x, speedY, RB.velocity.z);

        }

        if (dashing == true)
        {
            endDash -= Time.deltaTime;
        }

        if (endDash <= 0)
        {
            if (dashing)
            {
                RB.velocity = new Vector3(0, RB.velocity.y, 0);
            }
            dashing = false;

            //transform.GetChild(2).gameObject.SetActive(false);
            dashCooldown -= Time.deltaTime;          
        }

        if (dashCooldown <= 0)
        {
            endDash = 0.3f;
            canDash = true;
            dashCooldown = 1;
        }
    }
}
