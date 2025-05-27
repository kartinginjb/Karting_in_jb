using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class TrackSelectionManager : MonoBehaviour
{
    public static TrackSelectionManager Instance;

    public GameObject[] tracks; // Array com todos os karts disponíveis
    public int trackSelecionada = 0; // Índice do kart selecionado

    private void Awake()
    {
        // Garante que só existe uma instância
        if (Instance == null)
        {
            Instance = this;
            UnityEngine.Object.DontDestroyOnLoad(gameObject); // Mantém ao trocar de cena
        }
        else
        {
            Destroy(gameObject); // Evita instâncias duplicadas
        }
    }

    public void SelecionarTrack(int trackID)
    {
        trackSelecionada = trackID;
    }

    public void CarregarTrackSelecionado()
    {
        switch (trackSelecionada)
        {
            case 0:
                SceneManager.LoadScene("Pista1_Monza");
                UnityEngine.Debug.Log("Track Selecionada: Monza");
                break;
            case 1:
                SceneManager.LoadScene("Pista2_Melga");
                UnityEngine.Debug.Log("Track Selecionada: Melga");
                break;
            case 2:
                SceneManager.LoadScene("Pista3_Campo");
                UnityEngine.Debug.Log("Track Selecionada: Campo");
                break;
            case 3:
                SceneManager.LoadScene("Pista4_Basica");
                UnityEngine.Debug.Log("Track Selecionada: Basica");
                break;
            case 4:
                SceneManager.LoadScene("Pista5_DiasSpeed");
                UnityEngine.Debug.Log("Track Selecionada: Dias SpeedWay");
                break;
            case 5:
                SceneManager.LoadScene("Pista6_Jedonaco");
                UnityEngine.Debug.Log("Track Selecionada: Jedonaco");
                break;
            default:
                UnityEngine.Debug.LogWarning("Track inválido selecionado.");
                break;
        }
    }


    public GameObject GettrackSelecionado()
    {
        if (trackSelecionada >= 0 && trackSelecionada < tracks.Length)
        {
            return tracks[trackSelecionada];
        }
        return null;
    }
}
