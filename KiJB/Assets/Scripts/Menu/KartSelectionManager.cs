using System.Diagnostics;
using UnityEngine;

public class KartSelectionManager : MonoBehaviour
{
    public static KartSelectionManager Instance;

    public GameObject[] karts; // Array com todos os karts dispon�veis
    public int kartSelecionado = 0; // �ndice do kart selecionado

    private void Awake()
    {
        // Garante que s� existe uma inst�ncia
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mant�m ao trocar de cena
        }
        else
        {
            Destroy(gameObject); // Evita inst�ncias duplicadas
        }
    }

    public void SelecionarKart(int kartID)
    {
        if (kartID < 0 || kartID >= karts.Length) return; // Evita erro de �ndice inv�lido

        kartSelecionado = kartID;
        UnityEngine.Debug.Log("Kart Selecionado: " + kartSelecionado);
    }

    public GameObject GetKartSelecionado()
    {
        if (kartSelecionado >= 0 && kartSelecionado < karts.Length)
        {
            return karts[kartSelecionado];
        }
        return null;
    }
}
