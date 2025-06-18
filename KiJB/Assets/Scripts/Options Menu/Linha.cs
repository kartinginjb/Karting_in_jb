using UnityEngine;
using UnityEngine.UI;

public class LinhaCorridaManager : MonoBehaviour
{
    public Toggle toggleLinhaCorrida;
    public GameObject[] linhasCorrida; // 6 objetos (1 para cada pista)

    void Start()
    {
        // Carrega valor salvo, padrão é 1 (ativo)
        int estadoSalvo = PlayerPrefs.GetInt("LinhaCorridaAtiva", 1);
        bool ativo = estadoSalvo == 1;

        toggleLinhaCorrida.isOn = ativo;
        AtualizarLinhas(ativo);

        toggleLinhaCorrida.onValueChanged.AddListener(AtualizarLinhas);
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
