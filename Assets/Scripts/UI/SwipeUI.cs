using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeUI : MonoBehaviour
{
    [SerializeField]
    private NPC_Destiny destiny;
    [SerializeField]
    private GameObject swipeUI;

    void Update()
    {
        swipeUI.SetActive(destiny.isReadyForJudgement);
    }
}
