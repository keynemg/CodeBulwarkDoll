using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Entities : MonoBehaviour
{
    public float maxHp;
    protected float currentHp;
    public int hpLV;

    public float maxSp;
    protected float currentSp;
    public int spLV;

    public bool immortal;
    private bool isAlive = true;

    public UnityEvent DeathEvent;
    public UnityEvent DamageEvent;

    public bool IsAlive
    {
        get
        {
            isAlive = CurrentHp > 0;
            return isAlive;
        }

        set
        {
            isAlive = value;
        }
    }

    public float CurrentHp { get { return currentHp; } }
    public float CurrentSp { get { return currentSp; } }

    public virtual void Awake()
    {
        currentHp = maxHp;
    }   

    public virtual void RecieveDamage(float damage)
    {
        if (immortal || !IsAlive)
        {
            return;
        }
        currentHp -= damage;
        OnDamage();

        if (!IsAlive)
        {
            currentHp = 0;
            IsAlive = false;
            OnDeath();
        }
    }

    public void HealLife(float ammount)
    {
        currentHp += ammount;
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
    }

    public virtual void RecieveSP(float addition)
    {
        if (immortal || !IsAlive)
        {
            return;
        }
        if (addition > 0)
        {
            if (currentSp >= maxSp)
            {
                currentSp = maxSp;
                return;
            }
            else
            {
                currentSp += addition;
                if (currentSp > maxSp)
                {
                    currentSp = maxSp;
                    return;
                }
            }
        }
        if (addition < 0)
        {
            if (currentSp <= 0)
            {
                return;
            }
            else
            {
                currentSp += addition;
                if (currentSp < 0)
                {
                    currentSp = 0;
                }
            }
        }
    }

    public float CheckSP()
    {
        return currentSp;
    }

    public virtual void OnDamage()
    {
        iTween.PunchScale(gameObject, Vector3.one / 3, 0.5f);
        DamageEvent.Invoke();
    }

    public virtual void OnDeath()
    {
        DeathEvent.Invoke();
    }
}
