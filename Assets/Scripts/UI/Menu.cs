using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [HideInInspector]
    public bool isMenuOpen;
    [SerializeField]
    private GameObject menuWinodw;
    [SerializeField]
    private Tutorial tutorial;
    [SerializeField]
    private GameObject generalCanvas;

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenMenu()
    {
        if (!tutorial.isTutorialOpen)
        {
            if (!isMenuOpen)
            {
                Time.timeScale = 0;
                generalCanvas.SetActive(false);
                menuWinodw.SetActive(true);
                isMenuOpen = true;
            }
        }
        else
        {
            return;
        }
    }

    public void ResumeGame()
    {
        menuWinodw.SetActive(false);
        generalCanvas.SetActive(true);
        Time.timeScale = 1;
        isMenuOpen = false;
    }
}
