using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public GameObject playerBase;
    public GameObject pauseMenu;


    public GameObject dollWarning;
    public GameObject dollHouse;
    public Material dollHouse_Mat;
    public Material dollWarning_Mat;

    public AudioClip escSFX;



    void Awake()
    {
        instance = this;
        ResumeGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AudioManager.instance.PlaySingle(escSFX);
            CallMenu();
        }
    }

    public void CallMenu()
    {
        if (BuildingsMenu.Instance.opened_Window > 0)
        {
            BuildingsMenu.Instance.RecallBuildingMenu(BuildingsMenu.Instance.opened_Window);
        }
        else
        {
            if (pauseMenu.activeInHierarchy)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }


    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        HideHelp();
        HideOptions();
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void ShowHelp()
    {
        pauseMenu.transform.GetChild(6).gameObject.SetActive(true);
    }

    public void HideHelp()
    {
        pauseMenu.transform.GetChild(6).GetChild(0).gameObject.SetActive(true);
        pauseMenu.transform.GetChild(6).GetChild(1).gameObject.SetActive(false);
        pauseMenu.transform.GetChild(6).GetChild(2).gameObject.SetActive(false);
        pauseMenu.transform.GetChild(6).GetChild(3).gameObject.SetActive(false);
        pauseMenu.transform.GetChild(6).gameObject.SetActive(false);
    }

    public void ShowOptions()
    {
        pauseMenu.transform.GetChild(7).gameObject.SetActive(true);
    }

    public void HideOptions()
    {
        pauseMenu.transform.GetChild(7).gameObject.SetActive(false);
    }




    public void BuySomeDollsWarning()
    {
        BuildingsMenu.Instance.FailedToBuy();
        StopCoroutine(BuySomeDollRoutine());
        StartCoroutine(BuySomeDollRoutine());
    } 
    public IEnumerator BuySomeDollRoutine()
    {
        dollWarning.SetActive(true);
        dollHouse.GetComponent<Renderer>().material = dollWarning_Mat;
        iTween.ShakeScale(dollHouse, Vector3.one*0.2f, 0.5f);
        yield return new WaitForSeconds(1f);
        dollWarning.SetActive(false);
        dollHouse.GetComponent<Renderer>().material = dollHouse_Mat;
    }

}
