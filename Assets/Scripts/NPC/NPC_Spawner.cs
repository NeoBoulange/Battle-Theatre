using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Spawner : MonoBehaviour
{
    [Header ("Prefabs")]
    [SerializeField]
    private GameObject infanterie;
    [SerializeField]
    private GameObject tanks;
    [SerializeField]
    private GameObject aviation;
    [SerializeField]
    private GameObject sabotage;

    private NPC_Destiny destiny;

    void Start()
    {
        destiny = gameObject.GetComponent<NPC_Destiny>();
    }

    public void SpawnNext()
    {
        if (gameObject.GetComponent<Game_Manager>().nbOfChoice < 5)
        {
            SpawnUnitPlayerSide();
            SpawnUnitAiSide();
        }
    }

    private void SpawnUnitAiSide()
    {
        int nextUnitAISide = Random.Range(1, 101);

        if (nextUnitAISide <= 10)
        {
            destiny.playerSideUnit = GameObject.Instantiate(infanterie, infanterie.transform.position, infanterie.transform.rotation);
        }
        if (nextUnitAISide >= 11 && nextUnitAISide <= 65)
        {
            destiny.AISideUnit = GameObject.Instantiate(tanks, tanks.transform.position, tanks.transform.rotation);
        }
        else if (nextUnitAISide >= 66 && nextUnitAISide <= 84)
        {
            destiny.AISideUnit = GameObject.Instantiate(sabotage, sabotage.transform.position, sabotage.transform.rotation);
        }
        else if (nextUnitAISide >= 85)
        {
            destiny.AISideUnit = GameObject.Instantiate(aviation, aviation.transform.position, aviation.transform.rotation);
        }
    }

    private void SpawnUnitPlayerSide()
    {
        int nextUnitPlayerSide = Random.Range(1, 101);
        
        if (nextUnitPlayerSide <= 95)
        {
            destiny.playerSideUnit = GameObject.Instantiate(infanterie, infanterie.transform.position, infanterie.transform.rotation);
        }
        else if (nextUnitPlayerSide >= 96 && nextUnitPlayerSide <= 98)
        {
            destiny.playerSideUnit = GameObject.Instantiate(tanks, tanks.transform.position, tanks.transform.rotation);
        }
        else if (nextUnitPlayerSide >= 99)
        {
            destiny.playerSideUnit = GameObject.Instantiate(aviation, aviation.transform.position, aviation.transform.rotation);
        }
    }
}
