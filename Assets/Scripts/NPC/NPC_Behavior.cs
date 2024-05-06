using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Behavior : MonoBehaviour
{
    private Animator animator;

    private Slider slider;

    private NPC_Destiny destiny;

    public int valueOfNPC;

    void Start()
    {
        destiny = GameObject.FindGameObjectWithTag("Overseer").GetComponent<NPC_Destiny>();
        animator = gameObject.GetComponent<Animator>();
        if(gameObject == destiny.playerSideUnit)
        {
            animator.SetTrigger("Marche");
        }
    }

    public void Resolution(string ending)
    {
        if(ending == "Life")
        {
            animator.SetBool("Life", true);
        }

        else if (ending == "Death")
        {
            animator.SetBool("Death", true);
        }
    }

    public void Exchange()
    {
        animator.SetTrigger("Exchange");
    }

    private void Ready()
    {
        destiny.isReadyForJudgement = true;
    }

    private void NextUnit()
    {

        destiny.gameObject.GetComponent<NPC_Spawner>().SpawnNext();
    }

    public void AddInPlayerArmy()
    {
        GameObject unit = gameObject;
        destiny.gameObject.GetComponent<Game_Manager>().AddInArmy(unit, "Player");
    }

    public void AddInAIArmy()
    {
        GameObject unit = gameObject;
        destiny.gameManager.AddInArmy(unit, "IA");
    }
}
