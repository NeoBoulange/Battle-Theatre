using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PickUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform[] spritesLocations;
    private Game_Manager manager;
    [SerializeField]
    private GameObject[] prefabImageUnits;

    private List<GameObject> shownPictures;

    private void Start()
    {
        shownPictures = new List<GameObject>();
        manager = GameObject.FindGameObjectWithTag("Overseer").GetComponent<Game_Manager>();
    }
    public void NewUnit(int unit)
    {
        GameObject newPicture = null;

        if (unit == 0)
        {
            //Sabotage
            newPicture = Instantiate(prefabImageUnits[0]);
        }

        else if (unit == 1)
        {
            //Infanterie
            newPicture = Instantiate(prefabImageUnits[1]);
        }

        else if (unit == 5)
        {
            //Tank
            newPicture = Instantiate(prefabImageUnits[2]);
        }

        else if (unit == 10)
        {
            //Aviation
            newPicture = Instantiate(prefabImageUnits[3]);
        }

        shownPictures.Add(newPicture);
        newPicture.transform.SetParent(spritesLocations[manager.nbOfChoice - 1]);
        newPicture.transform.localPosition = Vector3.zero;
        newPicture.transform.localRotation = Quaternion.identity;
        newPicture.transform.localScale = new Vector3(6f,6f,6f);
    }

    public void DestroyPicutres()
    {
        foreach (GameObject p in shownPictures)
        {
            Destroy(p);
        }

        shownPictures.Clear();
    }
}


