using UnityEngine;
using UnityEngine.SceneManagement;

public class KartSelector : MonoBehaviour
{
    public GameObject[] karts; // Array com os 10 karts disponíveis
    private int kartSelecionado = 0; // Índice do kart atualmente selecionado

    void Start()
    {
        // Se já tiver um kart salvo no KartSelectionManager, carrega ele
        if (KartSelectionManager.Instance != null)
        {
            kartSelecionado = KartSelectionManager.Instance.kartSelecionado;
        }

        AtualizarVisualizacao();
    }

    public void MudarKart(int direcao)
    {
        // Avança (+1) ou retrocede (-1) na seleção
        kartSelecionado += direcao;

        // Se passar do último, volta para o primeiro
        if (kartSelecionado >= karts.Length)
        {
            kartSelecionado = 0;
        }
        // Se for menor que o primeiro, volta para o último
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
