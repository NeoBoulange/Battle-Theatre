using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAnnouncements : MonoBehaviour
{
    private BattlePhase battlePhase;
    private Game_Manager gameManager;
    private Animator animator;

    private void Start()
    {
        battlePhase = GameObject.FindGameObjectWithTag("Overseer").GetComponent<BattlePhase>();
        gameManager = GameObject.FindGameObjectWithTag("Overseer").GetComponent <Game_Manager>();
        animator = gameObject.GetComponent<Animator>();
    }

    public void AdvanceArmy(string army)
    {
        battlePhase.ArmiesAppearing(army);
    }

    public void Disappear()
    {
        animator.SetTrigger("Disappear");
    }

    public void SabotagePhase()
    {
        gameManager.CheckForSabotages("Player");
        gameManager.CheckForSabotages("IA");
    }

    public void BattlePhase()
    {
        battlePhase.playerArmyAnimator.SetTrigger("Battle");
        battlePhase.IAArmyAnimator.SetTrigger("Battle");
    }
}
