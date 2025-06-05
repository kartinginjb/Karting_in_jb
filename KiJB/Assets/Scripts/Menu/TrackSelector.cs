using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TrackSelector : MonoBehaviour
{
    public GameObject[] tracks; // Array com os tracks disponíveis
    public TextMeshProUGUI nomeDaPistaText; // Referência ao TextMeshPro que mostra o nome da pista

    private int trackSelecionada = 0;

    void Start()
    {
        if (TrackSelectionManager.Instance != null)
        {
            trackSelecionada = TrackSelectionManager.Instance.trackSelecionada;
        }

        AtualizarVisualizacao();
    }

    public void Mudartrack(int direcao)
    {
        trackSelecionada += direcao;

        if (trackSelecionada >= tracks.Length)
        {
            trackSelecionada = 0;
        }
        else if (trackSelecionada < 0)
        {
            trackSelecionada = tracks.Length - 1;
        }

        if (TrackSelectionManager.Instance != null)
        {
            TrackSelectionManager.Instance.SelecionarTrack(trackSelecionada);
        }

        AtualizarVisualizacao();
    }

    void AtualizarVisualizacao()
    {
        for (int i = 0; i < tracks.Length; i++)
        {
            tracks[i].SetActive(i == trackSelecionada);
        }

        // Atualiza o texto com o nome da pista
        if (nomeDaPistaText != null)
        {
            nomeDaPistaText.text = tracks[trackSelecionada].name;
        }
    }
}
