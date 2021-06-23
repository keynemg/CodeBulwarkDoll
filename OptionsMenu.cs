using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public enum Indicators { none=0,bars=1,circles=2,both=3}

public class OptionsMenu : MonoBehaviour
{

    bool currentMinimap;
    public GameObject minimap_UIOBj;
    public TextMeshProUGUI txt_Minimap;

    public Indicators currentIndicators;
    public GameObject bars_UIOBj;
    public TextMeshProUGUI txt_Bars;
    public GameObject circleHpSP_UIOBj;
    public GameObject circleBase_UIOBj;
    public TextMeshProUGUI txt_Circle;


    bool currentSkill;
    public GameObject[] skill_UIOBj = new GameObject[10];
    public TextMeshProUGUI txt_Skills;

    bool currentMusic = true;
    public TextMeshProUGUI txt_Music;



    private void Start()
    {
        currentMinimap = false;
        minimap_UIOBj.SetActive(false);
        txt_Minimap.text = "DEACTIVATED";

        currentIndicators = Indicators.both;
        bars_UIOBj.SetActive(true);
        circleHpSP_UIOBj.SetActive(true);
        circleBase_UIOBj.SetActive(true);
        txt_Bars.text = "ON";
        txt_Circle.text = "ON";


        currentSkill = true;
        for (int i = 0; i < skill_UIOBj.Length; i++)
        {
            skill_UIOBj[i].SetActive(true);
        }
        txt_Skills.text = "ACTIVATED";

    }

    public void ChangeMinimap()
    {
        currentMinimap = !currentMinimap;
        if (currentMinimap)
        {
            minimap_UIOBj.SetActive(true);
            txt_Minimap.text = "ACTIVATED";
        }
        else
        {
            minimap_UIOBj.SetActive(false);
            txt_Minimap.text = "DEACTIVATED";
        }
    }

    public void ChangeIndicators()
    {
        if (currentIndicators == Indicators.both)
        {
            currentIndicators = 0;
        }
        else
        {
            currentIndicators += 1;
        }
        switch (currentIndicators)
        {
            case Indicators.none:
                bars_UIOBj.SetActive(false);
                circleHpSP_UIOBj.SetActive(false);
                circleBase_UIOBj.SetActive(false);
                txt_Bars.text = "OFF";
                txt_Circle.text = "OFF";
                break;
            case Indicators.bars:
                bars_UIOBj.SetActive(true);
                circleHpSP_UIOBj.SetActive(false);
                circleBase_UIOBj.SetActive(false);
                txt_Bars.text = "ON";
                txt_Circle.text = "OFF";
                break;
            case Indicators.circles:
                bars_UIOBj.SetActive(false);
                circleHpSP_UIOBj.SetActive(true);
                circleBase_UIOBj.SetActive(true);
                txt_Bars.text = "OFF";
                txt_Circle.text = "ON";
                break;
            case Indicators.both:
                bars_UIOBj.SetActive(true);
                circleHpSP_UIOBj.SetActive(true);
                circleBase_UIOBj.SetActive(true);
                txt_Bars.text = "ON";
                txt_Circle.text = "ON";
                break;
        }
    }

    public void ChangeSkill()
    {
        currentSkill = !currentSkill;
        if (currentSkill)
        {
            for (int i = 0; i < skill_UIOBj.Length; i++)
            {
                skill_UIOBj[i].SetActive(true);
            }
            txt_Skills.text = "ACTIVATED";
        }
        else
        {
            for (int i = 0; i < skill_UIOBj.Length; i++)
            {
                skill_UIOBj[i].SetActive(false);
            }
            txt_Skills.text = "DEACTIVATED";
        }
    }

    public void ChangeMusic()
    {
        currentMusic = !currentMusic;
        if (currentMusic)
        {
            AudioManager.instance.PlayMusic();
            txt_Music.text = "ACTIVATED";
        }
        else
        {
            AudioManager.instance.StopMusic();
            txt_Music.text = "DEACTIVATED";
        }
    }


}
