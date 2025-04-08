using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelectScript : MonoBehaviour
{

    public void selectScene()
    {
        switch (this.gameObject.name)
        {
            case "Kart_sel":
                SceneManager.LoadScene("Karts");
                break;
            case "open_world":
                SceneManager.LoadScene("Open_world");
                break;
            case "Monza":
                SceneManager.LoadScene("Pista1_Monza");
                break;
            case "Melga":
                SceneManager.LoadScene("Pista2_Melga");
                break;
            case "Campo":
                SceneManager.LoadScene("Pista3_Campo");
                break;
        }
    }
}