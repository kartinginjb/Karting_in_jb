using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartingLights : MonoBehaviour
{
    public Cronometro cronometro;
    public Image red1;
    public Image red2;
    public Image red3;
    public Image green;

    public carro carro; // Referência ao script "carro"

    void Start()
    {
        StartCoroutine(StartSequence());
    }

    IEnumerator StartSequence()
    {
        // Desliga todas as luzes
        red1.enabled = false;
        red2.enabled = false;
        red3.enabled = false;
        green.enabled = false;

        // Impede o carro de acelerar
        carro.podeAcelerar = false;

        // Liga as luzes vermelhas uma a uma
        yield return new WaitForSeconds(1f);
        red1.enabled = true;
        Debug.Log("Red1 ON");
        yield return new WaitForSeconds(1f);
        red2.enabled = true;
        Debug.Log("Red2 ON");
        yield return new WaitForSeconds(1f);
        red3.enabled = true;
        Debug.Log("Red3 ON");
        yield return new WaitForSeconds(1f);

        // Desliga as luzes vermelhas
        red1.enabled = false;
        red2.enabled = false;
        red3.enabled = false;

        // Liga a luz verde
        green.enabled = true;
        Debug.Log("Green ON");

        // Permite o carro acelerar
        carro.podeAcelerar = true;

        // 👉 INICIA O CRONÓMETRO AQUI
        cronometro.ComecarCronometro();

        // Desliga a luz verde após 1.5 segundos
        yield return new WaitForSeconds(1.5f);
        green.enabled = false;
    }
}
