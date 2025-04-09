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
        red1.enabled = false;
        red2.enabled = false;
        red3.enabled = false;
        green.enabled = false;

        carro.podeAcelerar = false;

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

        red1.enabled = false;
        red2.enabled = false;
        red3.enabled = false;

        green.enabled = true;
        UnityEngine.Debug.Log("Green ON");

        carro.podeAcelerar = true;

        cronometro.ComecarCronometro();

        yield return new WaitForSeconds(1.5f);
        green.enabled = false;
    }

}
