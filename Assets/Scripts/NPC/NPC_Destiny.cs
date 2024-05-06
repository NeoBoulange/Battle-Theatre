using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;

public class NPC_Destiny : MonoBehaviour
{
    [HideInInspector]
    public GameObject playerSideUnit;
    [HideInInspector]
    public GameObject AISideUnit;

    [SerializeField]
    private Swiping swiping;

    public int choice;

    [HideInInspector]
    public Game_Manager gameManager;
    
    public GameObject totalValue;
    [HideInInspector]
    public int total = 0;

    [HideInInspector]
    public bool isReadyForJudgement = false;

    private void Start()
    {
        gameManager = gameObject.GetComponent<Game_Manager>();
    }

    private void Update()
    {
        Chosen();
        CanPlayerSwipe();
    }

    public void Chosen()
    {
        if (isReadyForJudgement)
        {
            if (choice >= 1)
            {
                gameManager.nbOfChoice += 1;
                isReadyForJudgement = false;
                playerSideUnit.GetComponent<NPC_Behavior>().Resolution("Life");
                AISideUnit.GetComponent<NPC_Behavior>().AddInAIArmy();
                choice = 0;
                ScoreUIUpdate("Player");
            }

            else if (choice <= -1)
            {
                gameManager.nbOfChoice += 1;
                isReadyForJudgement = false;
                playerSideUnit.GetComponent<NPC_Behavior>().Resolution("Death");
                AISideUnit.GetComponent<NPC_Behavior>().Exchange();
                choice = 0;
                ScoreUIUpdate("AI");
            }
        }
    }

    private void ScoreUIUpdate(string who)
    {
        if(who == "Player")
        {
            total = playerSideUnit.GetComponent<NPC_Behavior>().valueOfNPC + total;
        }

        else if (who == "AI")
        {
            total = AISideUnit.GetComponent<NPC_Behavior>().valueOfNPC + total;
        }

        if (total < 10)
        {
            totalValue.GetComponent<TextMeshProUGUI>().text = " " + total.ToString();
        }

        else if (total >= 10)
        {
            totalValue.GetComponent<TextMeshProUGUI>().text = total.ToString();
        }
    }

    private void CanPlayerSwipe()
    {
        if (!isReadyForJudgement)
        {
            swiping.canSwipe = false;
        }

        else if (isReadyForJudgement)
        {
            swiping.canSwipe = true;
        }
    }
}
