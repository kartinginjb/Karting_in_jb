using UnityEngine;
using TMPro;

public class KartSelector : MonoBehaviour
{
    public GameObject[] karts; // Array com os karts disponíveis
    public TextMeshProUGUI nomeDoKartText; // Texto que mostra o nome do kart

    private int kartSelecionado = 0;

    void Start()
    {
        if (KartSelectionManager.Instance != null)
        {
            kartSelecionado = KartSelectionManager.Instance.kartSelecionado;
        }

        AtualizarVisualizacao();
    }

    public void MudarKart(int direcao)
    {
        kartSelecionado += direcao;

        if (kartSelecionado >= karts.Length)
        {
            kartSelecionado = 0;
        }
        else if (kartSelecionado < 0)
        {
            kartSelecionado = karts.Length - 1;
        }

        if (KartSelectionManager.Instance != null)
        {
            KartSelectionManager.Instance.SelecionarKart(kartSelecionado);
        }

        AtualizarVisualizacao();
    }

    void AtualizarVisualizacao()
    {
        for (int i = 0; i < karts.Length; i++)
        {
            karts[i].SetActive(i == kartSelecionado);
        }

        // Atualiza o nome do kart no TextMeshPro diretamente com o nome do GameObject
        if (nomeDoKartText != null)
        {
            nomeDoKartText.text = karts[kartSelecionado].name;
        }
    }
}
