using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickPhaseAnnouncement : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private NPC_Spawner spawner;

    public void PickPhase()
    {
        animator.SetTrigger("Pick Phase");
    }

    public void SpawnFirst()
    {
        spawner.SpawnNext();
    }
}
