using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player_Stats : Entities
{

    public Image hpBar;
    public Image hpRing;
    public Image spBar;
    public Image spRing;
    public float spRegen = 0;
    public int regenLV;
    public int p_Current_Gold;
    public TextMeshProUGUI gold_MeshText;
    public int maxDolls = 0;

    public GameObject mesh;

    Color originalSpBarColor;
    Color originalSpRingColor;
    Color originalHpBarColor;
    Color originalHpRingColor;
    Color originalTubeColor;

    public AudioClip buyedSFX;

    //singleton
    private static Player_Stats instance;
    public static Player_Stats Instance { get { return instance; } }

    public override void Awake()
    {
        instance = this;
        base.Awake();
        currentSp = maxSp;
        AttGoldText();
        originalSpBarColor = spBar.color;
        originalSpRingColor = spRing.color;
        originalHpBarColor = hpBar.color;
        originalHpRingColor = hpRing.color;
        originalTubeColor = spBar.transform.parent.GetChild(1).GetComponent<Image>().color;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            DeduceGold(-500);
            AttGoldText();
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            RecieveDamage(1);
        }
    }


    void FixedUpdate()
    {
        if (IsAlive)
        {
            if (currentSp <= maxSp)
            {
                currentSp += Time.fixedDeltaTime * spRegen;
            }

            if (currentHp <= maxHp)
            {
                currentHp += Time.fixedDeltaTime * 1;
            }
        }


        hpBar.fillAmount = currentHp / maxHp;
        hpRing.fillAmount = 0.5f + 0.5f*(currentHp / maxHp);
        spBar.fillAmount = currentSp / maxSp;
        spRing.fillAmount = 0.5f + 0.5f * (currentSp / maxSp);


    }

    public override void RecieveSP(float _sp)
    {
        base.RecieveSP(_sp);
        StartCoroutine(FlashSPColor());
    }

    public IEnumerator FlashSPColor()
    {
        for (int i = 0; i < 1; i++)
        {
            spBar.color = Color.yellow;
            spRing.color = Color.yellow;
            spBar.transform.parent.GetChild(1).GetComponent<Image>().color = Color.white;
            yield return new WaitForSeconds(0.1f);
            spBar.color = originalSpBarColor;
            spRing.color = originalSpRingColor;
            spBar.transform.parent.GetChild(1).GetComponent<Image>().color = originalTubeColor;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator FlashHPColor()
    {
        for (int i = 0; i < 2; i++)
        {
            hpBar.color = Color.red;
            hpRing.color = Color.red;
            hpBar.transform.parent.GetChild(1).GetComponent<Image>().color = Color.white;
            yield return new WaitForSeconds(0.1f);
            hpBar.color = originalHpBarColor;
            hpRing.color = originalHpRingColor;
            hpBar.transform.parent.GetChild(1).GetComponent<Image>().color = originalTubeColor;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public override void OnDamage()
    {
        StartCoroutine(FlashHPColor());
        iTween.PunchScale(mesh, Vector3.one / 3, 0.5f);
        DamageEvent.Invoke();
    }

    public bool DeduceGold(int _deduced)
    {
        if (p_Current_Gold - _deduced >= 0)
        {
            p_Current_Gold -= _deduced;
            AudioManager.instance.PlaySingle(buyedSFX);
            AttGoldText();
            return true;
        }
        else
        {
            AttGoldText();
            return false;
        }
    }

    public void AttGoldText()
    {
        gold_MeshText.text = p_Current_Gold + "g";
    }

    public override void OnDeath()
    {
        base.OnDeath();
        PlayerBase.Instance.OnDeath();
    }
}
