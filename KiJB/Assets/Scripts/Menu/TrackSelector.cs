using UnityEngine;
using UnityEngine.SceneManagement;

public class TrackSelector : MonoBehaviour
{
    public GameObject[] tracks; // Array com os 10 tracks dispon�veis
    private int trackSelecionada = 0; // �ndice do track atualmente selecionado

    void Start()
    {
        // Se j� tiver um track salvo no trackSelectionManager, carrega ele
        if (TrackSelectionManager.Instance != null)
        {
            trackSelecionada = TrackSelectionManager.Instance.trackSelecionada;
        }

        AtualizarVisualizacao();
    }

    public void Mudartrack(int direcao)
    {
        // Avan�a (+1) ou retrocede (-1) na sele��o
        trackSelecionada += direcao;

        // Se passar do �ltimo, volta para o primeiro
        if (trackSelecionada >= tracks.Length)
        {
            trackSelecionada = 0;
        }
        // Se for menor que o primeiro, volta para o �ltimo
        else if (trackSelecionada < 0)
        {
            trackSelecionada = tracks.Length - 1;
        }

        // Atualiza o gerenciador global com o novo track selecionado
        if (TrackSelectionManager.Instance != null)
        {
            TrackSelectionManager.Instance.SelecionarTrack(trackSelecionada);
        }

        AtualizarVisualizacao();
    }

    void AtualizarVisualizacao()
    {
        // Ativa apenas o track atualmente selecionado
        for (int i = 0; i < tracks.Length; i++)
        {
            tracks[i].SetActive(i == trackSelecionada);
        }
    }
}
