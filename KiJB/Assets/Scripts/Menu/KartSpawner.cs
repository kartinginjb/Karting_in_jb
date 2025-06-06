using System.Collections;
using UnityEngine;

public class KartSpawner : MonoBehaviour
{
    public GameObject[] karts; // Array com todos os karts na cena

    private IEnumerator Start()
{
    // Espera um frame para garantir que tudo foi carregado
    yield return null;

    if (KartSelectionManager.Instance == null)
    {
        Debug.LogError("KartSelectionManager não encontrado!");
        yield break;
    }

    int kartSelecionado = KartSelectionManager.Instance.kartSelecionado;
    Debug.Log("Kart selecionado recebido na pista: " + kartSelecionado);

    if (kartSelecionado >= 0 && kartSelecionado < karts.Length)
    {
        for (int i = 0; i < karts.Length; i++)
        {
            karts[i].SetActive(i == kartSelecionado);
        }
    }
    else
    {
        Debug.LogError("Índice do kart selecionado inválido!");
    }
}

}
