using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Doll_Control : MonoBehaviour
{
    private static Doll_Control instance;
    public static Doll_Control Instance { get { return instance; } }

    #region KeyBindings Variables
    public KeyCode key_Move = KeyCode.Mouse0;
    public KeyCode key_Charge = KeyCode.Mouse1;
    public KeyCode key_Star = KeyCode.E;
    public KeyCode key_Shake = KeyCode.Q;
    public KeyCode key_Recall = KeyCode.Space;
    #endregion

    int numberOfSkills = 10;
    public float[] cd_Skill = new float[10] { 0, 5, 0, 3, 0, 3, 1, 10, 3, 5 };
    public float[] timer_Skill = new float[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public bool[] buyed_Skill = new bool[10] { true, false, true, false, false, false, false, false, false, false };
    public int[] g_Skill_Cost = new int[10] { -1, 15, 5, 15, 15, 30, 30, 50, 30, 50 };
    public int[] e_cost_Skill = new int[10] { 5, 10, 5, 10, -15, -35, 15, 15, 15, 25 };
    public Image[] img_Skill = new Image[10];


    public List<GameObject> p_Dolls = new List<GameObject>();
    public int maxDolls = 0;
    public int currentDolls = 0;
    public int p_SelectedDoll = 0;
    public GameObject dollPrefab;
    public TextMeshProUGUI txt_dollsNum;

    public LayerMask layer_UI;

    public AudioClip doll_Charge_SFX;
    public AudioClip doll_Spawn_SFX;

    private void Awake()
    {
        instance = this;

        for (int i = 0; i < numberOfSkills; i++)
        {
            timer_Skill[i] = cd_Skill[i];
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            AddDoll();
        }
        if (Input.GetKeyDown(KeyCode.Minus) && p_Dolls.Count > 0)
        {
            RemoveDoll();
            CycleDoll();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            CycleDoll();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (p_Dolls.Count > 0)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    StarDoll(0, e_cost_Skill[8]);// Move the first Doll in a Star Pattern around the Mouse
                }
                else
                {
                    DollCharge(0, e_cost_Skill[6]);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (p_Dolls.Count > 1)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    StarDoll(1, e_cost_Skill[8]);// Move the second Doll in a Star Pattern around the Mouse
                }
                else
                {
                    DollCharge(1, e_cost_Skill[8]);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (p_Dolls.Count > 2)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    StarDoll(2, e_cost_Skill[8]);// Move the third Doll in a Star Pattern around the Mouse
                }
                else
                {
                    DollCharge(2, e_cost_Skill[8]);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (p_Dolls.Count > 3)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    StarDoll(3, e_cost_Skill[8]);// Move the fourth Doll in a Star Pattern around the Mouse
                }
                else
                {
                    DollCharge(3, e_cost_Skill[8]);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (p_Dolls.Count > 4)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    StarDoll(4, e_cost_Skill[8]);// Move the fourth Doll in a Star Pattern around the Mouse
                }
                else
                {
                    DollCharge(4, e_cost_Skill[8]);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (p_Dolls.Count > 5)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    StarDoll(5, e_cost_Skill[8]);// Move the fourth Doll in a Star Pattern around the Mouse
                }
                else
                {
                    DollCharge(5, e_cost_Skill[8]);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            if (p_Dolls.Count > 6)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    StarDoll(6, e_cost_Skill[8]);// Move the fourth Doll in a Star Pattern around the Mouse
                }
                else
                {
                    DollCharge(6, e_cost_Skill[8]);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            if (p_Dolls.Count > 7)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    StarDoll(7, e_cost_Skill[8]);// Move the fourth Doll in a Star Pattern around the Mouse
                }
                else
                {
                    DollCharge(7, e_cost_Skill[8]);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            if (p_Dolls.Count > 8)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    StarDoll(8, e_cost_Skill[8]);// Move the fourth Doll in a Star Pattern around the Mouse
                }
                else
                {
                    DollCharge(8, e_cost_Skill[8]);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            if (p_Dolls.Count > 9)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    StarDoll(9, e_cost_Skill[8]);// Move the fourth Doll in a Star Pattern around the Mouse
                }
                else
                {
                    DollCharge(9, e_cost_Skill[8]);
                }
            }
        }
        if (Input.GetKeyDown(key_Shake))
        {
            if (p_Dolls.Count > 0)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                    ShakeAllDolls(e_cost_Skill[3]);
                else
                    ShakeDoll(p_SelectedDoll, e_cost_Skill[2]);
            }
        }
        if (Input.GetKeyDown(key_Star))
        {
            if (p_Dolls.Count > 0)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                    StarAllDolls(e_cost_Skill[8]);// Move All Dolls in a Star Pattern around itself
                else
                    StarDoll(p_SelectedDoll, e_cost_Skill[8]);// Move 1 Doll in a Star Pattern around the Mouse
            }
        }
        if (Input.GetKeyDown(key_Move) && !IsMouseOverUI())
        {
            if (BuildingsMenu.Instance.opened_Window == 0)
            {
                if (p_Dolls.Count > 0)
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                        MoveAllDolls(e_cost_Skill[1]);
                    else
                        MoveDoll(p_SelectedDoll, e_cost_Skill[0]);
                }
            }
        }
        if (Input.GetKeyDown(key_Charge))
        {
            if (p_Dolls.Count > 0)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                    AllDollsCharge(e_cost_Skill[9]);
                else
                    DollCharge(p_SelectedDoll, e_cost_Skill[8]);
            }
        }
        if (Input.GetKeyDown(key_Recall))
        {
            if (p_Dolls.Count > 0)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                    RecallAllDolls(e_cost_Skill[5]);
                else
                    RecallDoll(p_SelectedDoll, e_cost_Skill[4]);
            }
        }
    }

    public void FixedUpdate()
    {
        for (int i = 0; i < numberOfSkills; i++)
        {
            if (buyed_Skill[i])
            {
                if (timer_Skill[i] <= cd_Skill[i])
                {
                    timer_Skill[i] += Time.fixedDeltaTime;
                    img_Skill[i].fillAmount = timer_Skill[i] / cd_Skill[i];
                }
            }
        }
    }

    public bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    void MoveDoll(int _WhichDoll, int _EnergyCost)
    {
        if (!buyed_Skill[0])
            return;
        if (Player_Stats.Instance.CheckSP() >= _EnergyCost)
        {
            Player_Stats.Instance.RecieveSP(-_EnergyCost);
            AudioManager.instance.PlaySingle(doll_Charge_SFX, 0.3f);

            iTween.MoveTo(p_Dolls[_WhichDoll], iTween.Hash(
                "position", gameObject.GetComponent<Player_Mouse_Control>().targetPoint,
                "orienttopath", true,
                "looktime", 1f,
                "time", 0.5f * Vector3.Distance(p_Dolls[_WhichDoll].transform.position, gameObject.GetComponent<Player_Mouse_Control>().targetPoint)));
            if (p_Dolls[_WhichDoll].transform.GetChild(0).GetComponent<BoxCollider>().enabled)
            {
                p_Dolls[_WhichDoll].transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    void MoveAllDolls(int _EnergyCost)
    {
        if (!buyed_Skill[1])
            return;
        if (timer_Skill[1] < cd_Skill[1])
        {
            _EnergyCost *= 2;
        }
        if (Player_Stats.Instance.CheckSP() >= _EnergyCost * p_Dolls.Count)
        {
            int whichChild;
            int oddOrEven;
            float dividedAngle = 0;
            int angleSideMultiplier = 1;
            if (p_Dolls.Count % 2 == 0)
            {
                oddOrEven = 1;
            }
            else
            {
                oddOrEven = 0;
            }
            if (p_Dolls.Count > 6)
            {
                dividedAngle = 360 / p_Dolls.Count + 1;
                oddOrEven = 0;
            }
            else
            {
                dividedAngle = 180 / p_Dolls.Count + 1;
            }

            for (int i = 0; i < p_Dolls.Count; i++)
            {
                AudioManager.instance.PlaySingle(doll_Charge_SFX, 0.1f);
                if (p_Dolls[i].transform.GetChild(0).GetComponent<BoxCollider>().enabled)
                {
                    p_Dolls[i].transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
                }
                Player_Stats.Instance.RecieveSP(-_EnergyCost);
                whichChild = p_Dolls.Count / 5;
                if (p_Dolls.Count % 5 == 0)
                {
                    whichChild--;
                }
                if (i % 2 == 0)
                {
                    angleSideMultiplier = 1;
                }
                else
                {
                    angleSideMultiplier = -1;
                }
                if (whichChild > 8)
                {
                    whichChild = 8;
                }

                transform.GetChild(0).position = gameObject.GetComponent<Player_Mouse_Control>().targetPoint;

                transform.GetChild(0).Rotate(new Vector3(0, dividedAngle * oddOrEven * angleSideMultiplier, 0));
                iTween.MoveTo(p_Dolls[i], iTween.Hash(
                 "position", transform.GetChild(0).GetChild(whichChild).position,
                  "orienttopath", true,
                   "looktime", 2f,
                     "time", 1f));
                oddOrEven++;
            }

            transform.GetChild(0).rotation = transform.rotation;
            transform.GetChild(0).localPosition = new Vector3(0, -1, 0.5f);
            if (timer_Skill[1] >= cd_Skill[1])
            {
                timer_Skill[1] = 0;
            }
        }
    }

    void StarDoll(int _WhichDoll, int _EnergyCost)
    {
        if (!buyed_Skill[8])
            return;
        if (timer_Skill[8] < cd_Skill[8])
        {
            _EnergyCost *= 2;
        }
        if (Player_Stats.Instance.CheckSP() >= _EnergyCost)
        {
            if (!p_Dolls[_WhichDoll].transform.GetChild(0).GetComponent<BoxCollider>().enabled)
            {
                Player_Stats.Instance.RecieveSP(-_EnergyCost);
                AudioManager.instance.PlaySingle(doll_Charge_SFX, 0.3f);
                float starSize = 1f;
                Vector3[] starPath = new Vector3[7];
                transform.GetChild(0).rotation = transform.rotation;
                transform.GetChild(0).position = p_Dolls[_WhichDoll].transform.position;
                starPath[0] = transform.GetChild(0).position + transform.GetChild(0).forward * starSize;
                starPath[5] = transform.GetChild(0).position + transform.GetChild(0).forward * starSize;
                for (int i = 1; i < 5; i++)
                {
                    transform.GetChild(0).Rotate(0, 144, 0);
                    starPath[i] = transform.GetChild(0).position + transform.GetChild(0).forward * starSize;
                }
                starPath[6] = transform.GetChild(0).position;
                transform.GetChild(0).Rotate(0, -72, 0);

                transform.GetChild(0).rotation = transform.rotation;
                transform.GetChild(0).localPosition = new Vector3(0, -1, 0.5f);

                iTween.MoveTo(p_Dolls[_WhichDoll], iTween.Hash(
                    "path", starPath,
                    "orienttopath", true,
                    "looktime", 2f,
                    "time", 1f,
                    "easetype", "easeOutQuad",
                    "onstart", "CreateDollDamage",
                    "onstarttarget", gameObject,
                    "onstartparams", (p_Dolls[_WhichDoll]),
                    "oncomplete", "RemoveDollDamage",
                    "oncompletetarget", gameObject,
                    "oncompleteparams", (p_Dolls[_WhichDoll])
                    ));
                if (timer_Skill[8] >= cd_Skill[8])
                {
                    timer_Skill[8] = 0;
                }
            }
        }
    }

    void StarAllDolls(int _EnergyCost)
    {
        if (!buyed_Skill[9])
            return;
        if (timer_Skill[9] < cd_Skill[8])
        {
            _EnergyCost *= 2;
        }
        if (Player_Stats.Instance.CheckSP() > _EnergyCost * p_Dolls.Count)
        {
            for (int k = 0; k < p_Dolls.Count; k++)
            {
                if (!p_Dolls[k].transform.GetChild(0).GetComponent<BoxCollider>().enabled)
                {
                    AudioManager.instance.PlaySingle(doll_Charge_SFX, 0.1f);
                    Player_Stats.Instance.RecieveSP(-_EnergyCost);
                    float starSize = 1f;
                    Vector3[] starPath = new Vector3[7];
                    transform.GetChild(0).rotation = transform.rotation;
                    transform.GetChild(0).position = p_Dolls[k].transform.position;
                    starPath[0] = transform.GetChild(0).position + transform.GetChild(0).forward * starSize;
                    starPath[5] = transform.GetChild(0).position + transform.GetChild(0).forward * starSize;
                    for (int i = 1; i < 5; i++)
                    {

                        transform.GetChild(0).Rotate(0, 144, 0);
                        starPath[i] = transform.GetChild(0).position + transform.GetChild(0).forward * starSize;
                    }
                    starPath[6] = transform.GetChild(0).position;
                    transform.GetChild(0).Rotate(0, -72, 0);

                    transform.GetChild(0).rotation = transform.rotation;
                    transform.GetChild(0).localPosition = new Vector3(0, -1, 0.5f);

                    iTween.MoveTo(p_Dolls[k], iTween.Hash(
                        "path", starPath,
                        "orienttopath", true,
                        "looktime", 2f,
                        "time", 1f,
                        "easetype", "easeOutQuad",
                        "onstart", "CreateDollDamage",
                        "onstarttarget", gameObject,
                        "onstartparams", (p_Dolls[k]),
                        "oncomplete", "RemoveDollDamage",
                        "oncompletetarget", gameObject,
                        "oncompleteparams", (p_Dolls[k])
                        ));
                    if (timer_Skill[9] >= cd_Skill[9])
                    {
                        timer_Skill[9] = 0;
                    }
                }
            }
        }
    }

    void ShakeDoll(int _WhichDoll, int _EnergyCost)
    {
        if (!buyed_Skill[2])
            return;
        if (!p_Dolls[_WhichDoll].transform.GetChild(0).GetComponent<BoxCollider>().enabled)
        {
            if (Player_Stats.Instance.CheckSP() >= _EnergyCost)
            {
                Player_Stats.Instance.RecieveSP(-_EnergyCost);
                AudioManager.instance.PlaySingle(doll_Charge_SFX, 0.3f);
                iTween.ShakeScale(p_Dolls[_WhichDoll].transform.GetChild(0).GetChild(0).gameObject, iTween.Hash(
                        "amount", Vector3.one,
                        "time", 0.5f,
                        "onstart", "CreateDollDamage",
                        "onstarttarget", gameObject,
                        "onstartparams", p_Dolls[_WhichDoll],
                        "oncomplete", "RemoveDollDamage",
                        "oncompletetarget", gameObject,
                        "oncompleteparams", p_Dolls[_WhichDoll]
                        ));
                iTween.RotateBy(p_Dolls[_WhichDoll].transform.GetChild(0).GetChild(0).gameObject, iTween.Hash(
                    "time", 0.5f,
                    "easetype", "easeOutBounce",
                    "y", 10
                    ));
            }
        }
    }

    void ShakeAllDolls(int _EnergyCost)
    {
        if (!buyed_Skill[3])
            return;
        if (timer_Skill[3] < cd_Skill[3])
        {
            _EnergyCost *= 2;
        }
        if (Player_Stats.Instance.CheckSP() >= _EnergyCost * p_Dolls.Count)
        {
            for (int i = 0; i < p_Dolls.Count; i++)
            {
                if (!p_Dolls[i].transform.GetChild(0).GetComponent<BoxCollider>().enabled)
                {
                    AudioManager.instance.PlaySingle(doll_Charge_SFX, 0.1f);
                    Player_Stats.Instance.RecieveSP(-_EnergyCost);
                    iTween.ShakeScale(p_Dolls[i].transform.GetChild(0).GetChild(0).gameObject, iTween.Hash(
        "amount", Vector3.one,
        "time", 0.5f,
        "onstart", "CreateDollDamage",
        "onstarttarget", gameObject,
        "onstartparams", p_Dolls[i],
        "oncomplete", "RemoveDollDamage",
        "oncompletetarget", gameObject,
        "oncompleteparams", p_Dolls[i]
        ));
                    iTween.RotateBy(p_Dolls[i].transform.GetChild(0).GetChild(0).gameObject, iTween.Hash(
                        "time", 0.5f,
                        "easetype", "easeOutBounce",
                        "y", 10
                        ));
                    if (timer_Skill[3] >= cd_Skill[3])
                    {
                        timer_Skill[3] = 0;
                    }
                }
            }
        }
    }

    void RecallDoll(int _WhichDoll, int _EnergyCost)
    {
        if (!buyed_Skill[4])
            return;
        if (Vector3.Distance(transform.position, p_Dolls[_WhichDoll].transform.position) > 2.5f)
        {
            Player_Stats.Instance.RecieveSP(-_EnergyCost * 2);
            AudioManager.instance.PlaySingle(doll_Charge_SFX, 0.3f);
            iTween.MoveTo(p_Dolls[_WhichDoll], iTween.Hash(
         "position", transform.GetChild(0).position,
          "orienttopath", true,
           "looktime", 2f,
             "time", 0.5f));
            if (p_Dolls[_WhichDoll].transform.GetChild(0).GetComponent<BoxCollider>().enabled)
            {
                p_Dolls[_WhichDoll].transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
    void RecallAllDolls(int _EnergyCost)
    {
        if (!buyed_Skill[5])
            return;
        if (timer_Skill[5] >= cd_Skill[5])
        {
            if (Player_Stats.Instance.CheckSP() >= _EnergyCost * p_Dolls.Count)
            {
                int whichChild;
                int oddOrEven;
                float dividedAngle = 0;
                int angleSideMultiplier = 1;
                if (p_Dolls.Count % 2 == 0)
                {
                    oddOrEven = 1;
                }
                else
                {
                    oddOrEven = 0;
                }
                if (p_Dolls.Count > 6)
                {
                    dividedAngle = 360 / p_Dolls.Count + 1;
                    oddOrEven = 0;
                }
                else
                {
                    dividedAngle = 180 / p_Dolls.Count + 1;
                }

                for (int i = 0; i < p_Dolls.Count; i++)
                {
                    AudioManager.instance.PlaySingle(doll_Charge_SFX, 0.1f);
                    Player_Stats.Instance.RecieveSP(-_EnergyCost * 4);
                    whichChild = p_Dolls.Count / 5;
                    if (p_Dolls.Count % 5 == 0)
                    {
                        whichChild--;
                    }
                    if (i % 2 == 0)
                    {
                        angleSideMultiplier = 1;
                    }
                    else
                    {
                        angleSideMultiplier = -1;
                    }
                    if (whichChild > 8)
                    {
                        whichChild = 8;
                    }

                    transform.GetChild(0).Rotate(new Vector3(0, dividedAngle * oddOrEven * angleSideMultiplier, 0));
                    iTween.MoveTo(p_Dolls[i], iTween.Hash(
                     "position", transform.GetChild(0).GetChild(whichChild).position,
                      "orienttopath", true,
                       "looktime", 2f,
                         "time", 0.5f));
                    oddOrEven++;
                    if (p_Dolls[i].transform.GetChild(0).GetComponent<BoxCollider>().enabled)
                    {
                        p_Dolls[i].transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
                    }
                }
                transform.GetChild(0).rotation = transform.rotation;
                timer_Skill[5] = 0;
            }
        }
    }

    void DollCharge(int _WhichDoll, int _EnergyCost)
    {
        if (!buyed_Skill[6])
            return;
        if (timer_Skill[6] < cd_Skill[7])
        {
            _EnergyCost *= 2;
        }
        if (Player_Stats.Instance.CheckSP() >= _EnergyCost)
        {
            if (!p_Dolls[_WhichDoll].transform.GetChild(0).GetComponent<BoxCollider>().enabled)
            {
                Player_Stats.Instance.RecieveSP(-_EnergyCost);
                AudioManager.instance.PlaySingle(doll_Charge_SFX, 0.3f);
                iTween.ShakeScale(p_Dolls[_WhichDoll].transform.GetChild(0).GetChild(0).gameObject, iTween.Hash(
                            "amount", Vector3.one * 3,
                            "time", 0.5f));
                iTween.MoveTo(p_Dolls[_WhichDoll], iTween.Hash(
                "position", gameObject.GetComponent<Player_Mouse_Control>().targetPoint,
                "orienttopath", true,
                "looktime", 2f,
                "time", 0.5f,
                "onstart", "CreateDollDamage",
                "onstarttarget", gameObject,
                "onstartparams", p_Dolls[_WhichDoll],
                "oncomplete", "RemoveDollDamage",
                "oncompletetarget", gameObject,
                "oncompleteparams", p_Dolls[_WhichDoll]
        ));
                if (timer_Skill[6] >= cd_Skill[6])
                {
                    timer_Skill[6] = 0;
                }
            }
        }
    }
    void AllDollsCharge(int _EnergyCost)
    {
        if (!buyed_Skill[7])
            return;
        if (timer_Skill[7] < cd_Skill[7])
        {
            _EnergyCost *= 2;
        }
        if (Player_Stats.Instance.CheckSP() >= _EnergyCost * p_Dolls.Count)
        {
            for (int i = 0; i < p_Dolls.Count; i++)
            {
                if (!p_Dolls[i].transform.GetChild(0).GetComponent<BoxCollider>().enabled)
                {
                    AudioManager.instance.PlaySingle(doll_Charge_SFX, 0.1f);
                    Player_Stats.Instance.RecieveSP(-_EnergyCost);
                    iTween.ShakeScale(p_Dolls[i].transform.GetChild(0).GetChild(0).gameObject, iTween.Hash(
                                "amount", Vector3.one * 3,
                                "time", 0.5f));

                    iTween.MoveTo(p_Dolls[i], iTween.Hash(
                    "position", gameObject.GetComponent<Player_Mouse_Control>().targetPoint,
                    "orienttopath", true,
                    "looktime", 2f,
                    "time", 0.5f,
                    "onstart", "CreateDollDamage",
                    "onstarttarget", gameObject,
                    "onstartparams", p_Dolls[i],
                    "oncomplete", "RemoveDollDamage",
                    "oncompletetarget", gameObject,
                    "oncompleteparams", p_Dolls[i]));
                }
                if (timer_Skill[7] >= cd_Skill[7])
                {
                    timer_Skill[7] = 0;
                }
            }
        }
    }

    public void CycleDoll()
    {
        if (p_Dolls.Count > 0)
        {
            if (p_SelectedDoll < p_Dolls.Count)
            {
                p_Dolls[p_SelectedDoll].transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
                if (p_SelectedDoll + 1 < p_Dolls.Count)
                {
                    p_SelectedDoll++;
                }
                else
                {
                    p_SelectedDoll = 0;
                }
                iTween.PunchScale(p_Dolls[p_SelectedDoll].transform.GetChild(0).gameObject, Vector3.one / 3, 1);
                p_Dolls[p_SelectedDoll].transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                p_SelectedDoll = 0;
            }
        }
    }

    public void AddDoll()
    {
        if (p_Dolls.Count < maxDolls)
        {
            AudioManager.instance.PlaySingle(doll_Spawn_SFX);
            GameObject newDoll = Instantiate(dollPrefab, transform.GetChild(0));
            p_Dolls.Add(newDoll);
            newDoll.transform.SetParent(null);
            if (p_Dolls.Count == 1)
            {
                p_Dolls[0].transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
            }
            txt_dollsNum.text = p_Dolls.Count.ToString();
            currentDolls = p_Dolls.Count;
            BuildingsMenu.Instance.transform.GetChild(3).GetChild(2).GetComponent<Slider>().value++;
        }
    }

    public void RemoveDoll()
    {
        if (p_Dolls.Count > 0)
        {
            AudioManager.instance.PlaySingle(doll_Spawn_SFX);
            Destroy(p_Dolls[p_Dolls.Count - 1].gameObject);
            p_Dolls.RemoveAt(p_Dolls.Count - 1);
            if (p_SelectedDoll > p_Dolls.Count)
            {
                p_SelectedDoll = p_Dolls.Count;
            }
            txt_dollsNum.text = p_Dolls.Count.ToString();
            currentDolls = p_Dolls.Count;
            BuildingsMenu.Instance.transform.GetChild(3).GetChild(2).GetComponent<Slider>().value--;
        }
    }

    public void CreateDollDamage(GameObject doll)
    {
        doll.transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;
        doll.transform.GetChild(0).GetChild(3).GetComponent<ParticleSystem>().Play();
    }
    public void RemoveDollDamage(GameObject doll)
    {
        if (doll)
        {
            doll.transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
            doll.transform.GetChild(0).GetChild(3).GetComponent<ParticleSystem>().Stop();
        }
    }
}
