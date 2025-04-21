using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using TMPro;
using System.Diagnostics;

public class Cronometro : MonoBehaviour
{
    public TextMeshProUGUI textoCronometro;
    private float tempo = 0f;       // Variável que guarda o tempo total que passou
    private bool aContar = false;   // Controla se o cronómetro está a contar ou não

    void Update()
    {
        // Se o cronómetro estiver ativo, soma o tempo passado
        if (aContar)
        {
            tempo += Time.deltaTime;  // Soma o tempo desde o último frame
            AtualizarTexto();         // Atualiza o texto no ecrã
        }
    }

    // Atualiza o texto do cronómetro no formato MM:SS:MMM
    void AtualizarTexto()
    {
        // Converte o tempo total para minutos, segundos e milissegundos
        int minutos = Mathf.FloorToInt(tempo / 60F);
        int segundos = Mathf.FloorToInt(tempo % 60F);
        int milissegundos = Mathf.FloorToInt((tempo * 1000) % 1000);

        // Formata o texto com dois dígitos para minutos/segundos e três para milissegundos
        textoCronometro.text = string.Format("{0:00}:{1:00}:{2:000}", minutos, segundos, milissegundos);
    }

    // Inicia o cronómetro e reinicia o tempo
    public void ComecarCronometro()
    {
        tempo = 0f;
        aContar = true;
    }

    // Para o cronómetro sem apagar o tempo
    public void PararCronometro()
    {
        aContar = false;
    }

    // Retorna o tempo final (em segundos, como float)
    public float ObterTempoFinal()
    {
        return tempo;
    }
}
