using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cronometro : MonoBehaviour
{
    public TextMeshProUGUI textoCronometro;      // Texto do cronômetro em tempo real
    public TextMeshProUGUI textoUltimaVolta;     // Texto da última volta (novo)

    private float tempo = 0f;                    // Tempo total atual
    private bool aContar = false;                // Se o cronômetro está ativo

    void Update()
    {
        if (aContar)
        {
            tempo += Time.deltaTime;
            AtualizarTexto();  // Atualiza o cronômetro em tempo real
        }
    }

    // Atualiza o texto do cronômetro (tempo atual)
    void AtualizarTexto()
    {
        int minutos = Mathf.FloorToInt(tempo / 60F);
        int segundos = Mathf.FloorToInt(tempo % 60F);
        int milissegundos = Mathf.FloorToInt((tempo * 1000) % 1000);

        textoCronometro.text = string.Format("{0:00}:{1:00}:{2:000}", minutos, segundos, milissegundos);
    }

    // Atualiza o texto da última volta usando o tempo final
    void MostrarUltimaVolta()
    {
        float tempoFinal = ObterTempoFinal();

        int minutos = Mathf.FloorToInt(tempoFinal / 60F);
        int segundos = Mathf.FloorToInt(tempoFinal % 60F);
        int milissegundos = Mathf.FloorToInt((tempoFinal * 1000) % 1000);

        textoUltimaVolta.text = "Ultima Volta: " + string.Format("{0:00}:{1:00}:{2:000}", minutos, segundos, milissegundos);
    }

    // Inicia o cronômetro e zera o tempo
    public void ComecarCronometro()
    {
        tempo = 0f;
        aContar = true;
    }

    // Para o cronômetro e mostra o tempo da última volta
    public void PararCronometro()
    {
        aContar = false;
        MostrarUltimaVolta(); // Exibe o tempo da volta
    }

    // Retorna o tempo final (em segundos)
    public float ObterTempoFinal()
    {
        return tempo;
    }
}
