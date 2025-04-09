using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using TMPro;
using System.Diagnostics;

public class Cronometro : MonoBehaviour
{
    public TextMeshProUGUI textoCronometro; // arrastar aqui o componente UI Text
    private float tempo = 0f;
    private bool aContar = false;

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
        int minutos = Mathf.FloorToInt(tempo / 60F);
        int segundos = Mathf.FloorToInt(tempo % 60F);
        int milissegundos = Mathf.FloorToInt((tempo * 1000) % 1000);

        textoCronometro.text = string.Format("{0:00}:{1:00}:{2:000}", minutos, segundos, milissegundos);
    }

    public void ComecarCronometro()
    {
        tempo = 0f;
        aContar = true;
        UnityEngine.Debug.Log("Cronómetro começou!");
    }

    public void PararCronometro()
    {
        aContar = false;
    }

    public float ObterTempoFinal()
    {
        return tempo;
    }
}
