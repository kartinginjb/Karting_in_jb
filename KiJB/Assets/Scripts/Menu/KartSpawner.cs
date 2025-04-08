using System.Diagnostics;
using UnityEngine;

public class KartSpawner : MonoBehaviour
{
    public GameObject[] karts; // Array com todos os karts na cena

    private void Start()
    {
        if (KartSelectionManager.Instance == null)
        {
            UnityEngine.Debug.LogError("KartSelectionManager não encontrado!");
            return;
        }

        int kartSelecionado = KartSelectionManager.Instance.kartSelecionado;

        if (kartSelecionado >= 0 && kartSelecionado < karts.Length)
        {
            // Ativa apenas o kart selecionado
            for (int i = 0; i < karts.Length; i++)
            {
                karts[i].SetActive(i == kartSelecionado);
            }
        }
        else
        {
            UnityEngine.Debug.LogError("Índice do kart selecionado inválido!");
        }
    }
}
