using System.Diagnostics;
using UnityEngine;


public enum ModoDeJogo
{
    Nenhum,
    TimeTrial,
    ContraAI
}

public class ModoSelectrInTrack : MonoBehaviour
{
    public static ModoDeJogo ModoAtual = ModoDeJogo.Nenhum;

    public GameObject painelSelecaoModo;  // Painel com os botões de seleção
    public GameObject AIController;       // GameObject ou script que controla a IA

    void Start()
    {
        Time.timeScale = 0f;              // Pausar o jogo até o modo ser selecionado
        painelSelecaoModo.SetActive(true);

        // Inicialmente, desativa a IA (no menu de seleção)
        if (AIController != null)
            AIController.SetActive(false);
    }

    public void SelecionarTimeTrial()
    {
        ModoAtual = ModoDeJogo.TimeTrial;
        ConfigurarModo();
        IniciarCorrida();
    }

    public void SelecionarContraAI()
    {
        ModoAtual = ModoDeJogo.ContraAI;
        ConfigurarModo();
        IniciarCorrida();
    }

    void ConfigurarModo()
    {
        if (ModoAtual == ModoDeJogo.TimeTrial)
        {
            // Desativa IA no Time Trial
            if (AIController != null)
                AIController.SetActive(false);
        }
        else if (ModoAtual == ModoDeJogo.ContraAI)
        {
            // Ativa IA no Contra AI
            if (AIController != null)
                AIController.SetActive(true);
        }
    }

    void IniciarCorrida()
    {
        painelSelecaoModo.SetActive(false);
        Time.timeScale = 1f;
        UnityEngine.Debug.Log("Modo selecionado: " + ModoAtual);
    }
}
