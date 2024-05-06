using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;

public class BattlePhase : MonoBehaviour
{
    [Header ("Animator")]
    public Animator playerArmyAnimator;
    public Animator IAArmyAnimator;

    [Header("Battle Cloud")]
    [SerializeField]
    private VisualEffect battleCloud;
    [SerializeField]
    private bool activeBattleCloud;
    private bool battleCloudSafety;

    private Game_Manager gameManager;

    [Header("UI")]
    public TextMeshProUGUI playerValueUI;
    public TextMeshProUGUI IAValueUI;
    public TextMeshProUGUI textWinOrLose;
    public TextMeshProUGUI armiesValueTieUI;

    [HideInInspector]
    public bool playerIsReady;
    [HideInInspector]
    public bool IAIsReady;

    [HideInInspector]
    public bool playerIsReadyBattle;
    [HideInInspector]
    public bool IAIsReadyBattle;

    [SerializeField]
    private Animator sabotagePhaseUI;
    [SerializeField]
    private Animator ChargeUI;

    private void Start()
    {
        gameManager = gameObject.GetComponent<Game_Manager>();
        battleCloudSafety = true;
    }

    private void Update()
    {
        if (gameManager.inBattlePhase)
        {
            BattleStarting();
            Armiespositionned();
        }

        if (activeBattleCloud)
        {
            BattleCloud();
        }
    }

    private void BattleStarting()
    {
        if (playerIsReadyBattle && IAIsReadyBattle)
        {
            playerIsReadyBattle = false;
            IAIsReadyBattle = false;
            ChargeUI.SetTrigger("Appear");
        }
    }

    public void ArmiesAppearing(string army)
    {
        if(army == "Player")
        {
            playerArmyAnimator.SetTrigger("Ascend");
        }
        
        if(army == "AI")
        {
            IAArmyAnimator.SetTrigger("Descend");
        }
    }

    private void Armiespositionned()
    {
        if(playerIsReady && IAIsReady)
        {
            playerIsReady = false;
            IAIsReady = false;
            sabotagePhaseUI.SetTrigger("Appear");
        }
    }

    public void Battleing()
    {
        activeBattleCloud = true;
        gameManager.TotalInArmy();
    }

    public void BattleResolution(string resolution)
    {
        if(resolution == "Player")
        {
            playerArmyAnimator.SetTrigger("Win");
            IAArmyAnimator.SetTrigger("Lost");
            gameManager.ResetArmy(gameManager.opponentArmy);
            playerValueUI.text = gameManager.totalPlayer.ToString();
            IAValueUI.text = gameManager.totalAI.ToString();
            textWinOrLose.text = "You win !";
            gameManager.endBattleUI.SetActive(true);
        }

        if(resolution == "IA")
        {
            playerArmyAnimator.SetTrigger("Lost");
            IAArmyAnimator.SetTrigger("Win");
            gameManager.ResetArmy(gameManager.playerArmy);
            playerValueUI.text = gameManager.totalPlayer.ToString();
            IAValueUI.text = gameManager.totalAI.ToString();
            textWinOrLose.text = "You lost !";
            gameManager.endBattleUI.SetActive(true);
        }

        if (resolution == "Tie")
        {
            playerArmyAnimator.SetTrigger("Tie");
            IAArmyAnimator.SetTrigger("Tie");
            armiesValueTieUI.text = gameManager.totalPlayer.ToString();
            gameManager.endBattleTieUI.SetActive(true);
        }

        activeBattleCloud = false;
        gameManager.EndBattle();
    }

    private void BattleCloud()
    {
        if (battleCloudSafety)
        {
            battleCloudSafety = false;
            battleCloud.SendEvent("Starting fight");
            StartCoroutine("BattleCloudCooldown");
        }

        else
        {
            return;
        }
    }

    private IEnumerator BattleCloudCooldown()
    {
        yield return new WaitForSeconds(0.25f);
        battleCloudSafety = true;
        StopAllCoroutines();
    }
}
