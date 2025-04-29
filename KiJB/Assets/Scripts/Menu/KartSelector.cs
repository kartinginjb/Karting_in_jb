using UnityEngine;
using UnityEngine.SceneManagement;

public class KartSelector : MonoBehaviour
{
    public GameObject[] karts; // Array com os 10 karts dispon�veis
    private int kartSelecionado = 0; // �ndice do kart atualmente selecionado

    void Start()
    {
        // Se j� tiver um kart salvo no KartSelectionManager, carrega ele
        if (KartSelectionManager.Instance != null)
        {
            kartSelecionado = KartSelectionManager.Instance.kartSelecionado;
        }

        AtualizarVisualizacao();
    }

    public void MudarKart(int direcao)
    {
        // Avan�a (+1) ou retrocede (-1) na sele��o
        kartSelecionado += direcao;

        // Se passar do �ltimo, volta para o primeiro
        if (kartSelecionado >= karts.Length)
        {
            kartSelecionado = 0;
        }
        // Se for menor que o primeiro, volta para o �ltimo
        else if (kartSelecionado < 0)
        {
            kartSelecionado = karts.Length - 1;
        }

        // Atualiza o gerenciador global com o novo kart selecionado
        if (KartSelectionManager.Instance != null)
        {
            KartSelectionManager.Instance.SelecionarKart(kartSelecionado);
        }

        AtualizarVisualizacao();
    }

    void AtualizarVisualizacao()
    {
        // Ativa apenas o kart atualmente selecionado
        for (int i = 0; i < karts.Length; i++)
        {
            karts[i].SetActive(i == kartSelecionado);
        }
    }
}
