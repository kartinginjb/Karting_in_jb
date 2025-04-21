using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;

public class StartingLights : MonoBehaviour
{
    public Cronometro cronometro;
    public UnityEngine.UI.Image red1;
    public UnityEngine.UI.Image red2;
    public UnityEngine.UI.Image red3;
    public UnityEngine.UI.Image green;

    public carro carro;             // Referência ao script "carro"

    void Start()
    {
        StartCoroutine(StartSequence());
    }

    IEnumerator StartSequence()
    {
        //desliga todas as luzes
        red1.enabled = false;
        red2.enabled = false;
        red3.enabled = false;
        green.enabled = false;

        //não permite o carro acelerar
        carro.podeAcelerar = false;

        //liga sequencialmente as luzes de apagado a amarelo
        yield return new WaitForSeconds(1f);
        red1.enabled = true;
        UnityEngine.Debug.Log("Red1 ON");
        yield return new WaitForSeconds(1f);
        red2.enabled = true;
        UnityEngine.Debug.Log("Red2 ON");
        yield return new WaitForSeconds(1f);
        red3.enabled = true;
        UnityEngine.Debug.Log("Red3 ON");
        yield return new WaitForSeconds(1f);

        //desliga as luzes de apagado a amarelo
        red1.enabled = false;
        red2.enabled = false;
        red3.enabled = false;

        //liga a luz verde
        green.enabled = true;
        UnityEngine.Debug.Log("Green ON");

        //permite o carro acelerar 
        carro.podeAcelerar = true;

        //desliga a luz verde
        yield return new WaitForSeconds(1.5f);
        green.enabled = false;
    }

}
