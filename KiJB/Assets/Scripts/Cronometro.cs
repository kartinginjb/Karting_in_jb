using UnityEngine;
using TMPro;

public class Cronometro : MonoBehaviour
{
    public TextMeshProUGUI textoCronometro;      // Texto do cronômetro em tempo real
    public TextMeshProUGUI textoUltimaVolta;     // Texto da última volta
    public TextMeshProUGUI textoMelhorVolta;     // Texto do melhor tempo
    
    // declaração de variáveis
    private float tempo = 0f;
    private float melhorTempo = Mathf.Infinity;
    private float ultimaVolta = 0f;
    private bool aContar = false;

    public float MelhorTempo => melhorTempo; // 👈 Aqui está a propriedade pública
    void Update()
    {
        if (aContar)
        {
            tempo += Time.deltaTime;
            AtualizarTexto();
        }
    }

    void AtualizarTexto()
    {
        textoCronometro.text = FormatTime(tempo);
    }

    void MostrarUltimaVolta()
    {
        ultimaVolta = tempo;  // Armazena o tempo atual como última volta
        textoUltimaVolta.text = "Ultima Volta: " + FormatTime(ultimaVolta);

        VerificarMelhorTempo();
    }

    void VerificarMelhorTempo()
    {
        if (ultimaVolta <= 0f)
            return;

        if (melhorTempo == Mathf.Infinity || ultimaVolta < melhorTempo)
        {
            melhorTempo = ultimaVolta;
        }

        AtualizarMelhorTempoUI();
    }

    void AtualizarMelhorTempoUI()
    {
        textoMelhorVolta.text = "Melhor: " + FormatTime(melhorTempo);
        Debug.Log("Melhor tempo atualizado: " + melhorTempo);
    }

    string FormatTime(float tempo)
    {
        int minutos = Mathf.FloorToInt(tempo / 60F);
        int segundos = Mathf.FloorToInt(tempo % 60F);
        int milissegundos = Mathf.FloorToInt((tempo * 1000) % 1000);
        return string.Format("{0:00}:{1:00}:{2:000}", minutos, segundos, milissegundos);
    }

    public void ComecarCronometro()
    {
        tempo = 0f;
        aContar = true;
    }

    public void PararCronometro()
    {
        aContar = false;
        MostrarUltimaVolta();
    }

    public float ObterTempoFinal()
    {
        return tempo;
    }
}
