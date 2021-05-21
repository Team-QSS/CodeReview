using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour
{
    void skrk()
    {
            if (Input.GetKeyDown(KeyCode.Escape))
            { 
                Application.Quit();
            }
    }

}
