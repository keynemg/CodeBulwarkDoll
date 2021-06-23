using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Events;


public class PlayerBase : MonoBehaviour
{
    public UnityEvent deathEvent;
    public UnityEvent damageEvent;
    public string EnemyTag;
    public Image healthBar;
    public Image healthRing;
    public GameObject Victory;
    public GameObject Defeat;
    public float maxHp = 100;
    public int hpLV;
    public float currentHp;
    public int armor;
    public int armorLV;
    public float hpRegen = 20;
    public int regenLV;
    public float shakeIntensity = 1;
    public bool immortal;
    private bool isAlive = true;
    int counter;

    public AudioClip hitSFX;
    public AudioClip victorySFX;
    public AudioClip defeatSFX;

    Color originalColor;
    Color originalTubeColor;

    private static PlayerBase instance;
    public static PlayerBase Instance { get { return instance; } }

    public bool IsAlive
    {
        get
        {
            isAlive = currentHp > 0;
            return isAlive;
        }
        set
        {
            isAlive = value;
        }
    }

    public void Awake()
    {
        instance = this;
        currentHp = maxHp;
        originalColor = healthRing.color;
        originalTubeColor = healthBar.transform.parent.GetChild(1).GetComponent<Image>().color; ;
    }

    public void RepairPerWave()
    {
        if (currentHp + hpRegen <= maxHp)
        {
            currentHp += hpRegen;
            UpdateHealthValue(currentHp);
        }
        else
        {
           currentHp = maxHp;
            UpdateHealthValue(currentHp);
        }
    }


    public virtual void ReceiveDamage(float damage)
    {
        if (immortal || !IsAlive)
            return;
        currentHp -= damage * (1 - (armor * 0.01f));
        OnDamage();
        if (!IsAlive)
        {
            currentHp = 0;
            OnDeath();
        }
        float startHp = currentHp;
        iTween.ValueTo(gameObject, iTween.Hash("from", startHp, "to", currentHp, "time", 0.5f, "easeType", iTween.EaseType.linear, "onUpdate", "UpdateHealthValue"));
    }

    public virtual void OnDamage()
    {
        damageEvent.Invoke();
        DamageFX();
    }

    public void DamageFX()
    {
        StartCoroutine(FlashGenHPColor());
        iTween.ShakeScale(gameObject.transform.GetChild(0).gameObject, iTween.Hash("amount", Vector3.one * shakeIntensity, "time", 0.5f));

        for (int i = 0; i < transform.GetChild(2).childCount; i++)
        {
            if (!transform.GetChild(2).GetChild(i).GetComponent<ParticleSystem>().isPlaying)
            {
                transform.GetChild(2).GetChild(i).GetComponent<ParticleSystem>().Play();
                return;
            }
        }       
    }

    public IEnumerator FlashGenHPColor()
    {
        for (int i = 0; i < 4; i++)
        {
            healthRing.color = Color.gray;
            healthBar.color = Color.gray;
            healthBar.transform.parent.GetChild(1).GetComponent<Image>().color = Color.white;
            yield return new WaitForSeconds(0.05f);
            healthRing.color = originalColor;
            healthBar.color = originalColor;
            healthBar.transform.parent.GetChild(1).GetComponent<Image>().color = originalTubeColor;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public virtual void OnDeath()
    {
        if (!Defeat.activeInHierarchy)
        {
            deathEvent.Invoke();
            DefeatScreen();
        }
    }

    public void UpdateHealthValue(float value)
    {
        healthBar.fillAmount = value / maxHp;
        healthRing.fillAmount = 0.5f + 0.5f*(value / maxHp);
    }

    public void VictoryScreen()
    {
        Victory.SetActive(true);
        AudioManager.instance.PlaySingle(victorySFX);

    }

    void DefeatScreen()
    {
        Defeat.SetActive(true);
        AudioManager.instance.PlaySingle(defeatSFX);
    }
}
