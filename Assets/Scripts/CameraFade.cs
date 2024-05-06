using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using TMPro;

public class CameraFade : MonoBehaviour
{
    [SerializeField]
    private VisualEffect smoke;
    [SerializeField]
    private BattlePhase battlePhase;
    [SerializeField]
    private NPC_Spawner npcSpawner;
    [SerializeField]
    private Game_Manager gameManager;
    [SerializeField]
    private PickPhaseAnnouncement pickAnnouncement;
    [SerializeField]
    private Animator playerAdvanceUI;

    public void SmokeScreen()
    {
        smoke.Play();
    }

    public void Appear()
    {
        playerAdvanceUI.SetTrigger("Appear");
        smoke.gameObject.SetActive(false);
        smoke.gameObject.SetActive(true);
    }

    public void Spawn()
    {
        smoke.gameObject.SetActive(false);
        smoke.gameObject.SetActive(true);
        gameManager.pickUI.SetActive(true);
        pickAnnouncement.PickPhase();
        gameManager.destiny.total = 0;
        gameManager.destiny.totalValue.GetComponent<TextMeshProUGUI>().text = " 0";
    }
}
