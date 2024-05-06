using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sabotage : MonoBehaviour
{
    private Game_Manager gameManager;
    private int highestValue;
    private GameObject toBeNeutralized;
    [SerializeField]
    private List<GameObject> potentialNeutralized;
    private string army = null;

    [HideInInspector]
    public bool canSabotage = true;
    private int numberOfPotentialTargets;

    private Animator animator;

    void Start()
    {
        gameObject.GetComponent<NPC_Behavior>().valueOfNPC = 0;
        gameManager = GameObject.FindGameObjectWithTag("Overseer").GetComponent<Game_Manager>();
        List<GameObject> potentialNeutralized = new List<GameObject>();
        animator = gameObject.GetComponent<Animator>();
    }

    public void Locate(List<GameObject> armyToBeSabotaged)
    {
        if (army == null)
        {
            if (armyToBeSabotaged == gameManager.playerArmy)
            {
                army = "Player";
            }

            else if (armyToBeSabotaged == gameManager.opponentArmy)
            {
                army = "IA";
            }
        }

        if (potentialNeutralized != null)
        {
            potentialNeutralized.Clear();
        }

        foreach (GameObject unit in armyToBeSabotaged)
        {
            if (unit.activeSelf)
            {
                if (unit.GetComponent<Sabotage>() == null)
                {
                    potentialNeutralized.Add(unit);
                    numberOfPotentialTargets += 1;
                }
            }
        }

       if (numberOfPotentialTargets == 0)
       {
            canSabotage = false;
            EndOfThisSabotage();
            gameObject.SetActive(false);
       }

       else
       {
            highestValue = 0;
            toBeNeutralized = null;

            foreach (GameObject unit in potentialNeutralized)
            {
                if (highestValue < unit.GetComponent<NPC_Behavior>().valueOfNPC)
                {
                    highestValue = unit.GetComponent<NPC_Behavior>().valueOfNPC;
                    toBeNeutralized = unit;
                }
            }
       }

        gameObject.transform.position = toBeNeutralized.transform.position;
        StartCoroutine("Neutralize");
    }

    private IEnumerator Neutralize()
    {
        yield return new WaitForSeconds(2.5f);
        toBeNeutralized.SetActive(false);
        gameObject.SetActive(false);
        EndOfThisSabotage();
        StopAllCoroutines();
    }

    private void EndOfThisSabotage()
    {
        if (army == "Player")
        {
            gameManager.CheckForSabotages("Player");
        }

        else if (army == "IA")
        {
            gameManager.CheckForSabotages("IA");
        }
    }
}
