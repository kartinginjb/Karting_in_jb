using UnityEngine;
using UnityEngine.UI;

public class LinhaCorridaManager : MonoBehaviour
{
    public Toggle toggleLinhaCorrida; // Este será nulo noutras cenas
    public GameObject[] linhasCorrida;

    void Start()
    {
        int estadoSalvo = PlayerPrefs.GetInt("LinhaCorridaAtiva", 1);
        bool ativo = estadoSalvo == 1;

        // Se o Toggle existir, atualiza-o
        if (toggleLinhaCorrida != null)
        {
            toggleLinhaCorrida.isOn = ativo;
            toggleLinhaCorrida.onValueChanged.AddListener(AtualizarLinhas);
        }

        AtualizarLinhas(ativo); // Sempre ativa/desativa as linhas
    }

    void AtualizarLinhas(bool ativar)
    {
        PlayerPrefs.SetInt("LinhaCorridaAtiva", ativar ? 1 : 0);

        foreach (GameObject linha in linhasCorrida)
        {
            if (linha != null)
                linha.SetActive(ativar);
        }
    }
}
