using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject versusScreenUI;
    [SerializeField]
    private GameObject pickPhaseUI;
    [SerializeField]
    private GameObject generalUI;
    [SerializeField]
    private PickPhaseAnnouncement pickPhaseAnim;

    public void LaunchGame()
    {
        pickPhaseUI.SetActive(true);
        generalUI.SetActive(true);
        pickPhaseAnim.PickPhase();
        versusScreenUI.SetActive(false);
    }
}
