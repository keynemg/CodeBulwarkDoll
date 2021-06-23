using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingsMenu : MonoBehaviour
{
    private static BuildingsMenu instance;
    public static BuildingsMenu Instance { get { return instance; } }

    public AudioClip error_SFX;

    int generator_Cost_Life = 5;
    int generator_Cost_Armor = 5;
    int generator_Cost_Regen = 5;
    int character_Cost_Life = 5;
    int character_Cost_Energy = 5;
    int character_Cost_Regen = 5;
    int doll_Cost_Unlock = 30;

    public bool standing_AtHouse;
    public int standing_House_Index;
    public int opened_Window;

    public GameObject[] building_Canvas = new GameObject[5];

    public AudioClip sfx_OpenMenu;


    void Awake()
    {
        instance = this;
        Doll_Control.Instance.currentDolls = Doll_Control.Instance.p_Dolls.Count;
        transform.GetChild(3).GetChild(2).GetComponent<Slider>().maxValue = Doll_Control.Instance.maxDolls;
        transform.GetChild(3).GetChild(5).GetChild(0).GetComponent<TextMeshProUGUI>().text = Doll_Control.Instance.maxDolls.ToString();
        transform.GetChild(3).GetChild(7).GetComponent<TextMeshProUGUI>().text = doll_Cost_Unlock.ToString() + "g";

    }

    private void Start()
    {
        StartValues();
        for (int i = 1; i <= 5; i++)
        {
            RecallBuildingMenu(i);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            CheatDolls();
        }
        if (Input.GetKeyDown(KeyCode.X) && standing_AtHouse)
        {
            AudioManager.instance.PlaySingle(sfx_OpenMenu);
            if (opened_Window == 0)
            {
                CallBuildingMenu(standing_House_Index);
            }
            else
            {
                RecallBuildingMenu(standing_House_Index);
            }
        }
    }

    public void CheatDolls()
    {
        Doll_Control.Instance.maxDolls++;
        switch (Doll_Control.Instance.maxDolls)
        {
            default:
                doll_Cost_Unlock += 30;
                break;
        }
        transform.GetChild(3).GetChild(2).GetComponent<Slider>().maxValue = Doll_Control.Instance.maxDolls;
        transform.GetChild(3).GetChild(5).GetChild(0).GetComponent<TextMeshProUGUI>().text = Doll_Control.Instance.maxDolls.ToString();
        transform.GetChild(3).GetChild(7).GetComponent<TextMeshProUGUI>().text = doll_Cost_Unlock.ToString() + "g";
    }


    #region Upgrades

    #region Generator
    public void AttGeneratorAll()
    {
        AttGeneratorLife();
        AttGeneratorArmor();
        AttGeneratorRegen();
    }

    public void UpgradeGeneratorLife()
    {
        if (Player_Stats.Instance.DeduceGold(generator_Cost_Life))
        {
            PlayerBase.Instance.hpLV++;
            switch (PlayerBase.Instance.hpLV)
            {
                default:
                    PlayerBase.Instance.maxHp += 5;
                    PlayerBase.Instance.currentHp += 5;
                    generator_Cost_Life += 5;
                    break;
            }
            AttGeneratorLife();
        }
        else
        {
            FailedToBuy();
        }
    }

    public void AttGeneratorLife()
    {
        PlayerBase.Instance.UpdateHealthValue(PlayerBase.Instance.currentHp);
        transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "LV: " + PlayerBase.Instance.hpLV.ToString();
        transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = PlayerBase.Instance.currentHp.ToString() + "/" + PlayerBase.Instance.maxHp.ToString();
        transform.GetChild(1).GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = generator_Cost_Life + "g";
    }

    public void UpgradeGeneratorArmor()
    {
        if (Player_Stats.Instance.DeduceGold(generator_Cost_Armor))
        {
            PlayerBase.Instance.armorLV++;
            switch (PlayerBase.Instance.armorLV)
            {
                default:
                    PlayerBase.Instance.armor += 1;
                    generator_Cost_Armor += 5;

                    break;
            }
            AttGeneratorArmor();
        }
        else
        {
            FailedToBuy();
        }
    }

    public void AttGeneratorArmor()
    {
        transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "LV: " + PlayerBase.Instance.armorLV.ToString();
        transform.GetChild(1).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = PlayerBase.Instance.armor.ToString() + "%";
        transform.GetChild(1).GetChild(5).GetChild(0).GetComponent<TextMeshProUGUI>().text = generator_Cost_Armor + "g";
    }

    public void UpgradeGeneratorRegen()
    {
        if (Player_Stats.Instance.DeduceGold(generator_Cost_Regen))
        {
            PlayerBase.Instance.regenLV++;
            switch (PlayerBase.Instance.regenLV)
            {
                default:
                    PlayerBase.Instance.hpRegen += 5;
                    generator_Cost_Regen += 5;
                    break;
            }
            AttGeneratorRegen();
        }
        else
        {
            FailedToBuy();
        }
    }

    public void AttGeneratorRegen()
    {
        transform.GetChild(1).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "LV: " + PlayerBase.Instance.regenLV.ToString();
        transform.GetChild(1).GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>().text = PlayerBase.Instance.hpRegen.ToString() + " HP/Wave";
        transform.GetChild(1).GetChild(6).GetChild(0).GetComponent<TextMeshProUGUI>().text = generator_Cost_Regen + "g";
    }
    #endregion

    #region Character
    public void AttCharacterAll()
    {
        AttCharacterLife();
        AttCharacterEnergy();
        AttCharacterRegen();
    }

    public void UpgradeCharacterLife()
    {
        if (Player_Stats.Instance.DeduceGold(character_Cost_Life))
        {
            Player_Stats.Instance.hpLV++;
            switch (Player_Stats.Instance.hpLV)
            {
                default:
                    Player_Stats.Instance.maxHp += 5;
                    character_Cost_Life += 5;
                    break;
            }
            AttCharacterLife();
        }
        else
        {
            FailedToBuy();
        }
    }

    public void AttCharacterLife()
    {
        Player_Stats.Instance.HealLife(Player_Stats.Instance.maxHp);
        transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "LV: " + Player_Stats.Instance.hpLV.ToString();
        transform.GetChild(2).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = Player_Stats.Instance.CurrentHp.ToString() + "/" + Player_Stats.Instance.maxHp.ToString();
        transform.GetChild(2).GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = character_Cost_Life + "g";
    }

    public void UpgradeCharacterEnergy()
    {
        if (Player_Stats.Instance.DeduceGold(character_Cost_Energy))
        {
            Player_Stats.Instance.spLV++;
            switch (Player_Stats.Instance.spLV)
            {
                default:
                    Player_Stats.Instance.maxSp += 20;
                    character_Cost_Energy += 5;

                    break;
            }
            AttCharacterEnergy();
        }
        else
        {
            FailedToBuy();
        }
    }

    public void AttCharacterEnergy()
    {
        Player_Stats.Instance.RecieveSP(Player_Stats.Instance.maxSp);
        transform.GetChild(2).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "LV: " + Player_Stats.Instance.spLV.ToString();
        transform.GetChild(2).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = Player_Stats.Instance.CurrentSp.ToString() + "/" + Player_Stats.Instance.maxSp.ToString();
        transform.GetChild(2).GetChild(5).GetChild(0).GetComponent<TextMeshProUGUI>().text = character_Cost_Energy + "g";
    }

    public void UpgradeCharacterRegen()
    {
        if (Player_Stats.Instance.DeduceGold(character_Cost_Regen))
        {
            Player_Stats.Instance.regenLV++;
            switch (Player_Stats.Instance.regenLV)
            {
                default:
                    Player_Stats.Instance.spRegen += 5;
                    character_Cost_Regen += 5;
                    break;
            }
            AttCharacterRegen();
        }
        else
        {
            FailedToBuy();
        }
    }

    public void AttCharacterRegen()
    {
        transform.GetChild(2).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "LV: " + Player_Stats.Instance.regenLV.ToString();
        transform.GetChild(2).GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>().text = Player_Stats.Instance.spRegen.ToString() + "SP/sec";
        transform.GetChild(2).GetChild(6).GetChild(0).GetComponent<TextMeshProUGUI>().text = character_Cost_Regen + "g";
    }
    #endregion

    #region DollHouse
    public void UpgradeDollMax()
    {
        if (Player_Stats.Instance.DeduceGold(doll_Cost_Unlock))
        {
            Doll_Control.Instance.maxDolls++;
            switch (Doll_Control.Instance.maxDolls)
            {
                default:
                    doll_Cost_Unlock += 30;
                    break;
            }
            transform.GetChild(3).GetChild(2).GetComponent<Slider>().maxValue = Doll_Control.Instance.maxDolls;
            transform.GetChild(3).GetChild(5).GetChild(0).GetComponent<TextMeshProUGUI>().text = Doll_Control.Instance.maxDolls.ToString();
            transform.GetChild(3).GetChild(7).GetComponent<TextMeshProUGUI>().text = doll_Cost_Unlock.ToString() + "g";
        }
        else
        {
            FailedToBuy();
        }
    }

    public void AddDollButton()
    {
        if (transform.GetChild(3).GetChild(2).GetComponent<Slider>().value < Doll_Control.Instance.maxDolls)
        {
            transform.GetChild(3).GetChild(2).GetComponent<Slider>().value++;
            AttDollsCount();
        }
    }

    public void RemoveDollButton()
    {
        if (transform.GetChild(3).GetChild(2).GetComponent<Slider>().value > 0)
        {
            transform.GetChild(3).GetChild(2).GetComponent<Slider>().value--;
            Debug.Log(transform.GetChild(3).GetChild(2).GetComponent<Slider>().value);
            AttDollsCount();
        }
    }

    public void AttDollsCount()
    {
        StartCoroutine(AttSlider());
    }

    IEnumerator AttSlider()
    {
        yield return new WaitForSeconds(0.05f);
        if (transform.GetChild(3).GetChild(2).GetComponent<Slider>().value > Doll_Control.Instance.currentDolls)
        {
            Doll_Control.Instance.AddDoll();
        }
        if (transform.GetChild(3).GetChild(2).GetComponent<Slider>().value < Doll_Control.Instance.currentDolls)
        {
            Doll_Control.Instance.RemoveDoll();
        }
        transform.GetChild(3).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = Doll_Control.Instance.p_Dolls.Count.ToString();
        transform.GetChild(3).GetChild(2).GetComponent<Slider>().value = Doll_Control.Instance.p_Dolls.Count;
    }
    #endregion

    #region DollWorkshop

    void StartValues()
    {
        int skillIndex = 0;
        int singleOrAll = 0;

        #region Adjusting Gold Costs
        for (int i = 0; i < 10; i++)
        {
            switch (i)
            {
                case 0:
                    skillIndex = 0;
                    singleOrAll = 0;
                    break;
                case 1:
                    skillIndex = 0;
                    singleOrAll = 1;
                    break;
                case 2:
                    skillIndex = 1;
                    singleOrAll = 0;
                    break;
                case 3:
                    skillIndex = 1;
                    singleOrAll = 1;
                    break;
                case 4:
                    skillIndex = 2;
                    singleOrAll = 0;
                    break;
                case 5:
                    skillIndex = 2;
                    singleOrAll = 1;
                    break;
                case 6:
                    skillIndex = 3;
                    singleOrAll = 0;
                    break;
                case 7:
                    skillIndex = 3;
                    singleOrAll = 1;
                    break;
                case 8:
                    skillIndex = 4;
                    singleOrAll = 0;
                    break;
                case 9:
                    skillIndex = 4;
                    singleOrAll = 1;
                    break;
            }
            if (Doll_Control.Instance.buyed_Skill[i])
            {
                transform.GetChild(4).GetChild(1).GetChild(skillIndex).GetChild(singleOrAll).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Lv:Max";
                transform.GetChild(4).GetChild(1).GetChild(skillIndex).GetChild(singleOrAll).GetChild(2).GetChild(2).GetComponent<Button>().interactable = false;
                transform.GetChild(4).GetChild(1).GetChild(skillIndex).GetChild(singleOrAll).GetChild(2).GetChild(3).GetComponent<TextMeshProUGUI>().text = "-g";
                continue;
            }
            transform.GetChild(4).GetChild(1).GetChild(skillIndex).GetChild(singleOrAll).GetChild(2).GetChild(3).
                        GetComponent<TextMeshProUGUI>().text = Doll_Control.Instance.g_Skill_Cost[i].ToString() + "g";
        }
        #endregion

        for (int i = 0; i < Doll_Control.Instance.img_Skill.Length; i++)
        {
            if (Doll_Control.Instance.buyed_Skill[i])
            {
                continue;
            }
            Doll_Control.Instance.img_Skill[i].color = new Color(255, 255, 255, 0f);
            Doll_Control.Instance.img_Skill[i].transform.parent.GetComponent<Image>().color = new Color(160, 0, 0, 0);
            Doll_Control.Instance.img_Skill[i].transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color(255, 255, 255, 0);
        }
    }

    public void BuySkill(int _whichSkill)
    {

        int skillIndex = 0;
        int singleOrAll = 0;
        switch (_whichSkill)
        {
            case 0:
                skillIndex = 0;
                singleOrAll = 0;
                break;
            case 1:
                skillIndex = 0;
                singleOrAll = 1;
                break;
            case 2:
                skillIndex = 1;
                singleOrAll = 0;
                break;
            case 3:
                skillIndex = 1;
                singleOrAll = 1;
                break;
            case 4:
                skillIndex = 2;
                singleOrAll = 0;
                break;
            case 5:
                skillIndex = 2;
                singleOrAll = 1;
                break;
            case 6:
                skillIndex = 3;
                singleOrAll = 0;
                break;
            case 7:
                skillIndex = 3;
                singleOrAll = 1;
                break;
            case 8:
                skillIndex = 4;
                singleOrAll = 0;
                break;
            case 9:
                skillIndex = 4;
                singleOrAll = 1;
                break;
        }
        if (singleOrAll == 1)
        {
            if (Doll_Control.Instance.buyed_Skill[skillIndex * 2] == false)
            {
                StopCoroutine(FlashSoloSkill(skillIndex));
                StartCoroutine(FlashSoloSkill(skillIndex));
                FailedToBuy();
                return;
            }
        }
        if (Player_Stats.Instance.DeduceGold(Doll_Control.Instance.g_Skill_Cost[_whichSkill]))
        {
            transform.GetChild(4).GetChild(1).GetChild(skillIndex).GetChild(singleOrAll).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Lv:Max";
            transform.GetChild(4).GetChild(1).GetChild(skillIndex).GetChild(singleOrAll).GetChild(2).GetChild(2).GetComponent<Button>().interactable = false;
            transform.GetChild(4).GetChild(1).GetChild(skillIndex).GetChild(singleOrAll).GetChild(2).GetChild(3).GetComponent<TextMeshProUGUI>().text = "-g";
            Doll_Control.Instance.buyed_Skill[_whichSkill] = true;
            Doll_Control.Instance.img_Skill[_whichSkill].color = Color.white;
            Doll_Control.Instance.img_Skill[_whichSkill].transform.parent.GetComponent<Image>().color = new Color(160, 0, 0, 1);
            Doll_Control.Instance.img_Skill[_whichSkill].transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color(255, 255, 255, 150);

        }
        else
        {
            FailedToBuy();
        }
    }

    #endregion

    public void FailedToBuy()
    {
        AudioManager.instance.PlaySingle(error_SFX);
    }

    #endregion


    #region MenuShowingOrHiding

    public void StartStanding(int _index)
    {
        if (Spawn_Waves.Instance.aliveEnemies > 0)
        {
            return;
        }
        building_Canvas[_index - 1].SetActive(true);
        standing_AtHouse = true;
        standing_House_Index = _index;

    }
    public void LeftStanding()
    {
        standing_AtHouse = false;
        RecallBuildingMenu(standing_House_Index);
        building_Canvas[standing_House_Index - 1].SetActive(false);
        standing_House_Index = 0;
    }

    public void CallBuildingMenu(int index)
    {
        if (Spawn_Waves.Instance.aliveEnemies < 1)
        {
            opened_Window = index;
            float callTimer = 0.5f;
            switch (index)
            {
                case 1:
                    iTween.Stop(transform.GetChild(1).gameObject);
                    iTween.MoveTo(transform.GetChild(1).gameObject, transform.GetChild(0).position, callTimer);//Generator
                    AttGeneratorAll();
                    break;
                case 2:
                    iTween.Stop(transform.GetChild(2).gameObject);
                    iTween.MoveTo(transform.GetChild(2).gameObject, transform.GetChild(0).position, callTimer);//Player
                    AttCharacterAll();
                    break;
                case 3:
                    iTween.Stop(transform.GetChild(3).gameObject);
                    iTween.MoveTo(transform.GetChild(3).gameObject, transform.GetChild(0).position, callTimer);//Doll_House
                    break;
                case 4:
                    iTween.Stop(transform.GetChild(4).gameObject);
                    iTween.MoveTo(transform.GetChild(4).gameObject, transform.GetChild(0).position, callTimer);//Workshop
                    break;
                case 5:
                    iTween.Stop(transform.GetChild(5).gameObject);
                    iTween.MoveTo(transform.GetChild(5).gameObject, transform.GetChild(0).position, callTimer);//Factory
                    break;
            }
        }
    }

    public void RecallBuildingMenu(int index)
    {
        if (Spawn_Waves.Instance.aliveEnemies == 0)
        {
            opened_Window = 0;
            float recallTimer = 5f;
            switch (index)
            {
                case 1:
                    iTween.FadeTo(transform.GetChild(1).gameObject, 0, recallTimer);
                    iTween.MoveTo(transform.GetChild(1).gameObject, transform.GetChild(6).position, recallTimer);//Generator
                    break;
                case 2:
                    iTween.MoveTo(transform.GetChild(2).gameObject, transform.GetChild(7).position, recallTimer);//Player
                    break;
                case 3:
                    iTween.MoveTo(transform.GetChild(3).gameObject, transform.GetChild(8).position, recallTimer);//Doll_House
                    break;
                case 4:
                    iTween.MoveTo(transform.GetChild(4).gameObject, transform.GetChild(9).position, recallTimer);//Workshop
                    break;
                case 5:
                    iTween.MoveTo(transform.GetChild(5).gameObject, transform.GetChild(10).position, recallTimer);//Factory
                    break;
            }
        }
    }
    #endregion

    public void CanOrNotBuyButtonChanger(Button _button, int _Cost)
    {
        if (Player_Stats.Instance.p_Current_Gold >= _Cost)
        {
            _button.interactable = true;
        }
        else
        {
            _button.interactable = false;
        }
    }

    public IEnumerator FlashSoloSkill(int _index)
    {
        Image icon = transform.GetChild(4).GetChild(1).GetChild(_index).GetChild(0).GetChild(2).GetChild(0).GetComponent<Image>();
        icon.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        icon.color = new Color(200,200,200,255);
    }

}
