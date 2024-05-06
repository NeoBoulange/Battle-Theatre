using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BO_Score : MonoBehaviour
{
    [SerializeField]
    private Game_Manager game_Manager;
    private TextMeshProUGUI boScore;

    private void Start()
    {
        boScore = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        boScore.text = game_Manager.opponentScoreBO + "-" + game_Manager.playerScoreBO;
    }
}
