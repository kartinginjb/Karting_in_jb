using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonScript : MonoBehaviour
{

    public void backToMainMenu()
    {
        SceneManager.LoadScene("Play");
    }
}