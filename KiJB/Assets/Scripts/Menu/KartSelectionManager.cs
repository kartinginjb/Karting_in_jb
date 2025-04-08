using System.Diagnostics;
using UnityEngine;

public class KartSelectionManager : MonoBehaviour
{
    public static KartSelectionManager Instance;

    public GameObject[] karts; // Array com todos os karts disponíveis
    public int kartSelecionado = 0; // Índice do kart selecionado

    private void Awake()
    {
        // Garante que só existe uma instância
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantém ao trocar de cena
        }
        else
        {
            Destroy(gameObject); // Evita instâncias duplicadas
        }
    }

    public void SelecionarKart(int kartID)
    {
        if (kartID < 0 || kartID >= karts.Length) return; // Evita erro de índice inválido

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
