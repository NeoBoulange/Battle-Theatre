using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swiping : MonoBehaviour
{
    [HideInInspector]
    public bool canSwipe;

    private Vector2 swipeStartPos;
    private Vector2 swipeEndPos;

    private NPC_Destiny destiny;

    [SerializeField]
    private float minimumPercentage;

    private void Start()
    {
        destiny = gameObject.GetComponent<NPC_Destiny>();
    }

    void Update()
    {
        Swipe();
    }

    private void Swipe()
    {
        if (canSwipe)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                swipeStartPos = Input.GetTouch(0).position;
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                swipeEndPos = Input.GetTouch(0).position;

                if(PercentOfScreen() >= minimumPercentage)
                {
                    if (swipeEndPos.y < swipeStartPos.y)
                    {
                        destiny.choice = 1;
                    }
                    else //(swipeEndPos.y > swipeStartPos.y)
                    {
                        destiny.choice = -1;
                    }
                }
            }
        }
    }

    private float PercentOfScreen()
    {
        float swipeDistance = Mathf.Abs(swipeStartPos.y - swipeEndPos.y);
        float swipePercent = swipeDistance * 100 / Screen.width;
        return swipePercent;
    }
}
