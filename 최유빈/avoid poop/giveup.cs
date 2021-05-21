using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class giveup : MonoBehaviour
{

    public void SceneChange()
    {
        SceneManager.LoadScene("GiveUp");
    }
    
}
