using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject tutorialWindow;
    [SerializeField]
    private GameObject[] pages = null;

    private int pageNumber;
    private int previousPage;

    [SerializeField]
    private Swiping swiping;

    [HideInInspector]
    public bool isTutorialOpen;

    [SerializeField]
    private GameObject generalCanvas;

    [SerializeField]
    private GameObject previousPageButton;
    [SerializeField]
    private GameObject nextPageButton;

    public void OpenWindow()
    {
        if (!isTutorialOpen)
        {
            Time.timeScale = 0;
            generalCanvas.SetActive(false);
            tutorialWindow.SetActive(true);
            swiping.canSwipe = false;
            isTutorialOpen = true;
        }
        else
        {
            return;
        }
    }

    public void CloseWindow()
    {
        tutorialWindow.gameObject.SetActive(false);
        generalCanvas.SetActive(true);
        Time.timeScale = 1;
        swiping.canSwipe = true;
        pageNumber = 1;
        RenderChange();
        isTutorialOpen = false;
    }

    public void ChangePage(int direction)
    {
        pageNumber += direction;
        RenderChange();
    }

    private void RenderChange()
    {
        pages[previousPage].SetActive(false);
        pages[pageNumber].SetActive(true);

        if (pageNumber  == 1)
        {
            previousPageButton.SetActive(false);
        }

        else if (pageNumber == pages.Length)
        {
            nextPageButton.SetActive(false);
        }

        else
        {
            previousPageButton.SetActive(true);
            nextPageButton.SetActive(true);
        }
    }
}
