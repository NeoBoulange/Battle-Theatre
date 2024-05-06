using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    void Update()
    {
        if (Input.touchCount > 0)
        {
            SceneManager.LoadScene(1);
        }
    }
}
