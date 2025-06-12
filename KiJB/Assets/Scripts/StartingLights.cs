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

    // Referência ao script "KartSeguidor" e todos os karts com AI
    public KartSeguidor piloto1;
    public KartSeguidor piloto2;
    public KartSeguidor piloto3;
    public KartSeguidor piloto4;
    public KartSeguidor piloto5;
    public KartSeguidor piloto6;
    public KartSeguidor piloto7;

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
        piloto1.podeAcelerar = false;
        piloto2.podeAcelerar = false;
        piloto3.podeAcelerar = false;
        piloto4.podeAcelerar = false;
        piloto5.podeAcelerar = false;
        piloto6.podeAcelerar = false;
        piloto7.podeAcelerar = false;

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
        piloto1.podeAcelerar = true;
        piloto2.podeAcelerar = true;
        piloto3.podeAcelerar = true;
        piloto4.podeAcelerar = true;
        piloto5.podeAcelerar = true;
        piloto6.podeAcelerar = true;
        piloto7.podeAcelerar = true;

        // 👉 INICIA O CRONÓMETRO AQUI
        cronometro.ComecarCronometro();

        // Desliga a luz verde após 1.5 segundos
        yield return new WaitForSeconds(1.5f);
        green.enabled = false;
    }
}
