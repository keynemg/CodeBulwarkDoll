using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Spawn_Waves : MonoBehaviour
{   
    GameObject[] spawnerobj;
    public GameObject[] Enemies;
    public int[] waveEnemyAmount;
    public int currentWave = -1;
    public int aliveEnemies;
    public float[] intervalBetweenSpawn;
    public bool canspawn = true;
    public Button startButton;
    public AudioClip WaveSFX;
    public List<GameObject> waveMonsters = new List<GameObject>();

    public TextMeshProUGUI waveTxT;
    public TextMeshProUGUI aliveEnemiesTxT;

    private static Spawn_Waves instance;

    public GameObject[] buildingsIndicator = new GameObject[5];

    public static Spawn_Waves Instance { get { return instance; } }


    public GameObject endWave_FX;
    public GameObject endWaveDone_FX;
    public GameObject startWave_FX;
    public AudioClip endWave_SFX;


    void Start()
    {
        instance = this;
        spawnerobj = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            spawnerobj[i] = transform.GetChild(i).gameObject;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            KillWave();
        }
    }

    public void StartWave()
    {
        if (Doll_Control.Instance.currentDolls < 1)
        {
            GameManager.Instance.BuySomeDollsWarning();
            return;
        }
        PlayerBase.Instance.transform.GetChild(0).GetComponent<Animator>().SetBool("WaveStarted", true);
        AudioManager.instance.PlaySingle(WaveSFX);
        startWave_FX.GetComponent<ParticleSystem>().Play();
        BuildingsMenu.Instance.RecallBuildingMenu(BuildingsMenu.Instance.opened_Window);
        StartCoroutine(Spawner());
        PlayerBase.Instance.RepairPerWave();

        for (int i = 0; i < buildingsIndicator.Length; i++)
        {
            buildingsIndicator[i].SetActive(false);
        }

    }

    public void KillWave()
    {
        StopAllCoroutines();
        while (waveMonsters.Count > 0)
        {
            Destroy(waveMonsters[0].gameObject);
            waveMonsters.RemoveAt(0);
        }
        aliveEnemies = 0;
        aliveEnemiesTxT.text = aliveEnemies.ToString();
        StartCoroutine(EndWave());
    }
    private IEnumerator Spawner()
    {
        currentWave++;
        waveTxT.text = (1 + currentWave).ToString();
        startButton.gameObject.GetComponent<CanvasGroup>().alpha = 0.3f;
        startButton.interactable = false;

        List<int> oldRandoms = new List<int>();
        int newRandomizer;
        for (int i = 0; i < waveEnemyAmount[currentWave]; i++)
        {
            newRandomizer = Random.Range(0, spawnerobj.Length);

            for (int r = 0; r < oldRandoms.Count; r++)
            {
                if (newRandomizer == oldRandoms[r])
                {
                    newRandomizer = Random.Range(0, spawnerobj.Length);
                    r = 10;
                }
            }
            if (oldRandoms.Count > 4)
            {
                oldRandoms.RemoveAt(0);
            }
            oldRandoms.Add(newRandomizer);


            int enemyToSpawn;
            enemyToSpawn = Random.Range(0, 2);

            int chanceOfMelee = Random.Range(0, currentWave);


            if (chanceOfMelee == 0)
            {
                enemyToSpawn = 0;
            }
            else
            {
                enemyToSpawn = 1;
            }

            aliveEnemies++;
            aliveEnemiesTxT.text = aliveEnemies.ToString();
            GameObject newmonster = Instantiate(Enemies[enemyToSpawn], spawnerobj[newRandomizer].transform.position, Quaternion.identity);
            waveMonsters.Add(newmonster);
            newmonster.GetComponent<NavMeshAgent>().SetDestination(PlayerBase.Instance.transform.position);
            yield return new WaitForSeconds(intervalBetweenSpawn[currentWave]);
        }
        yield return new WaitUntil(() => aliveEnemies == 0);
        StartCoroutine(EndWave());
    }

    public void WaveFinished()
    {
        if (currentWave + 1 == intervalBetweenSpawn.Length)
        {
            PlayerBase.Instance.VictoryScreen();
        }
        else
        {

            startButton.interactable = true;
            startButton.gameObject.GetComponent<CanvasGroup>().alpha = 1f;
            Player_Stats.Instance.RecieveSP(Player_Stats.Instance.maxSp);
            Player_Stats.Instance.HealLife(Player_Stats.Instance.maxHp);
        PlayerBase.Instance.transform.GetChild(0).GetComponent<Animator>().SetBool("WaveStarted", false);

            for (int i = 0; i < buildingsIndicator.Length; i++)
            {
                buildingsIndicator[i].SetActive(true);
            }
        }
    }

    public IEnumerator EndWave()
    {
        endWave_FX.GetComponent<ParticleSystem>().Play();
        AudioManager.instance.PlaySingle(endWave_SFX);
        yield return new WaitForSeconds(2f);
        endWaveDone_FX.GetComponent<ParticleSystem>().Play();
        AudioManager.instance.PlaySingle(WaveSFX);
        WaveFinished();
    }

}
