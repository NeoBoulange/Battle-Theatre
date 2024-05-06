using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game_Manager : MonoBehaviour
{
    [HideInInspector]
    public int nbOfChoice;

    [Header("Camera Transition")]
    [SerializeField]
    private Animator cameraAnimator;

    [HideInInspector]
    public List<GameObject> playerArmy;
    [HideInInspector]
    public List<GameObject> opponentArmy;

    [Header("Spawn Points")]
    [SerializeField]
    private List<Transform> positionsOfPlayerArmy;
    [SerializeField]
    private List <Transform> positionsOfOpponentArmy;

    [Header("UI")]
    public GameObject pickUI;
    public GameObject endBattleUI;
    public GameObject endBattleTieUI;
    [SerializeField]
    private PickUI pickPlayerArmyUI;
    [SerializeField]
    private PickUI pickOpponentArmyUI;
    [SerializeField]
    private GameObject endOfGameUI;
    [SerializeField]
    private Sprite winScreen;
    [SerializeField]
    private Sprite loseScreen;


    [HideInInspector]
    public int totalPlayer;
    [HideInInspector]
    public int totalAI;

    [HideInInspector]
    public bool inBattlePhase;
    private bool inPickPhase;

    [HideInInspector]
    public int playerScoreBO;
    [HideInInspector]
    public int opponentScoreBO;

    [HideInInspector]
    public NPC_Destiny destiny;
    private BattlePhase battlePhase;

    void Start()
    {
        destiny = gameObject.GetComponent<NPC_Destiny>();
        battlePhase = gameObject.GetComponent<BattlePhase>();
        inPickPhase = true;
        nbOfChoice = 0;
        playerArmy = new List<GameObject>();
        opponentArmy = new List<GameObject>();
    }

    void Update()
    {
        if (inPickPhase)
        {
            if (nbOfChoice == 5)
            {
                BattleStart();
            }
        }

        if (playerScoreBO >= 3 || opponentScoreBO >= 3)
        {
            EndOfTheGame();
        }
    }

    private void BattleStart()
    {
        StartCoroutine("EndOfPhaseCooldown");
    }

    public void AddInArmy(GameObject unit,string army)
    {
        if (army == "Player")
        {
            pickPlayerArmyUI.NewUnit(unit.GetComponent<NPC_Behavior>().valueOfNPC);
            playerArmy.Add(unit);

            if (unit.GetComponent<Sabotage>() == null)
            {
                unit.GetComponent<Animator>().SetLayerWeight(1, 1);
            }

            else
            {
                unit.GetComponent<Animator>().SetLayerWeight(1, 0);
                unit.GetComponent<Animator>().SetLayerWeight(2, 1);
            }

            unit.transform.SetParent(positionsOfPlayerArmy[nbOfChoice - 1]);
            unit.GetComponent<Animator>().SetTrigger("Zero");
            unit.transform.localScale = new Vector3(0.075f, 0.075f, 0.075f);
        }

        else if (army == "IA")
        {
            pickOpponentArmyUI.NewUnit(unit.GetComponent<NPC_Behavior>().valueOfNPC);
            opponentArmy.Add(unit);
            if (unit.GetComponent<Sabotage>() == null)
            {
                unit.GetComponent<Animator>().SetLayerWeight(2, 1);
            }

            else
            {
                unit.GetComponent<Animator>().SetLayerWeight(1, 0);
                unit.GetComponent<Animator>().SetLayerWeight(3, 1);
            }
            unit.transform.SetParent(positionsOfOpponentArmy[nbOfChoice - 1]);
            unit.GetComponent<Animator>().SetTrigger("Zero");
            unit.transform.localScale = new Vector3(0.075f, 0.075f, 0.075f);
        }
    }

    public void TotalInArmy()
    {
        foreach (GameObject unit in playerArmy)
        {
            if (unit.activeSelf)
            {
                totalPlayer += unit.GetComponent<NPC_Behavior>().valueOfNPC;
            }
        }

        foreach (GameObject unit in opponentArmy)
        {
            if (unit.activeSelf)
            {
                totalAI += unit.GetComponent<NPC_Behavior>().valueOfNPC;
            }
        }

        StartCoroutine("CompareArmies");
    }

    private void SetArmyPositions()
    {
        foreach(GameObject unit in playerArmy)
        {
            unit.transform.localPosition = Vector3.zero;
        }

        foreach (GameObject unit in opponentArmy)
        {
            unit.transform.localPosition = Vector3.zero;
        }
    }

    private IEnumerator CompareArmies()
    {
        yield return new WaitForSeconds(2.5f);

        if(totalPlayer < totalAI)
        {
            battlePhase.BattleResolution("IA");
            opponentScoreBO += 1;
        }

        else if (totalPlayer > totalAI)
        {
            battlePhase.BattleResolution("Player");
            playerScoreBO += 1;
        }

        else if (totalAI == totalPlayer)
        {
            battlePhase.BattleResolution("Tie");
        }
    }

    public void ResetArmy(List<GameObject> armyToClear)
    {
        foreach (GameObject unit in armyToClear)
        {
            Destroy(unit);
        }

        armyToClear.Clear();
    }

    private IEnumerator EndOfPhaseCooldown()
    {

        if (inPickPhase)
        {
            yield return new WaitForSeconds(3);
            pickPlayerArmyUI.DestroyPicutres();
            pickOpponentArmyUI.DestroyPicutres();
            pickUI.SetActive(false);
            cameraAnimator.SetTrigger("Go Down");
            inBattlePhase = true;
            inPickPhase = false;
            StopAllCoroutines();
        }

        else if (inBattlePhase)
        {
            yield return new WaitForSeconds(1.5f);
            cameraAnimator.SetTrigger("Go Up");
            FinalReset();
            inBattlePhase = false;
            inPickPhase=true;
            StopAllCoroutines();
        }

    }

    public void CheckForSabotages(string army)
    {
        if(army == "Player")
        {
            foreach (GameObject unit in playerArmy)
            {
                if (unit.activeSelf)
                {
                    if (unit.GetComponent<Sabotage>() != null)
                    {
                        if(unit.GetComponent<Sabotage>().canSabotage)
                        {
                            unit.GetComponent<Sabotage>().Locate(playerArmy);
                            return;
                        }
                    }
                }
            }

            battlePhase.playerIsReadyBattle = true;
        }

        if (army == "IA")
        {
            foreach (GameObject unit in opponentArmy)
            {
                if (unit.activeSelf)
                {
                    if (unit.GetComponent<Sabotage>() != null)
                    {
                        if (unit.GetComponent<Sabotage>().canSabotage)
                        {
                            unit.GetComponent<Sabotage>().Locate(opponentArmy);
                            return;
                        }
                    }
                }
            }

            battlePhase.IAIsReadyBattle = true;
        }
    }

    public void EndBattle()
    {
        nbOfChoice = 0;
        totalAI = 0;
        totalPlayer = 0;
    }

    private void FinalReset()
    {
        ResetArmy(playerArmy);
        ResetArmy(opponentArmy);
        battlePhase.playerArmyAnimator.SetTrigger("Reset");
        battlePhase.IAArmyAnimator.SetTrigger("Reset");
    }

    public void NextRound()
    {
        endBattleUI.SetActive(false);
        endBattleTieUI.SetActive(false);
        StartCoroutine("EndOfPhaseCooldown");
    }

    private void EndOfTheGame()
    {
        Time.timeScale = 0;

        endOfGameUI.SetActive(true);

        if (playerScoreBO > opponentScoreBO)
        {
            endOfGameUI.GetComponent<Image>().sprite = winScreen;
        }

        else
        {
            endOfGameUI.GetComponent<Image>().sprite = loseScreen;
        }
    }

    public void UnitsAnimationChange(string state, string army)
    {
        if(army == "Player Army")
        {
            foreach (GameObject unit in playerArmy)
            {
                if (unit.CompareTag("NPC"))
                {
                    unit.GetComponent<Animator>().SetTrigger(state);
                }
            }
        }

        if(army == "AI Army")
        {
            foreach (GameObject unit in opponentArmy)
            {
                if (unit.CompareTag("NPC"))
                {
                    unit.GetComponent<Animator>().SetTrigger(state);
                }
            }
        }
    }
}
