using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battlephase_Link : MonoBehaviour
{
    private BattlePhase battlePhase;
    private Game_Manager gameManager;
    [SerializeField]
    private Animator playerAdvanceUI;
    [SerializeField]
    private Animator AIAdvanceUI;

    private void Start()
    {
        battlePhase = GameObject.FindGameObjectWithTag("Overseer").GetComponent<BattlePhase>();
        gameManager = GameObject.FindGameObjectWithTag("Overseer").GetComponent <Game_Manager>();
    }

    public void IntoBattle()
    {
        battlePhase.Battleing();
    }

    public void OnPositionForSabotages(string who)
    {
        if (who == "Player")
        {
            battlePhase.playerIsReady = true;
        }

        else if (who == "AI")
        {
            battlePhase.IAIsReady = true;
        }
    }

    public void UnitsAnimation(string state)
    {
        gameManager.UnitsAnimationChange(state, gameObject.name);
    }

    public void ShowEnnemyArmy()
    {
        AIAdvanceUI.SetTrigger("Appear");
    }
}
