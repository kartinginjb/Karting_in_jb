using UnityEngine;
using TMPro;
using System;

public class HUDController : MonoBehaviour
{
    public TextMeshProUGUI textRPM;
    public TextMeshProUGUI textMudanca;
    public TextMeshProUGUI textVelocidade;
    public TextMeshProUGUI textVoltas;

    public carro carro; // Refer�ncia ao script do carro

    void Update()
    {
        textVoltas.text = Mathf.RoundToInt(carro.voltas).ToString() + " Voltas";
        textRPM.text = Mathf.RoundToInt(carro.rpm).ToString() + " RPM";             //recebe o valor das RPM do script carro
        textVelocidade.text = Mathf.RoundToInt(carro.veloKMH).ToString() + " KM/H"; //recebe o valor da velocidade do script carro
            
            //verifica se o jogador est� a acelerar para a frente // tr�s 
        if (Input.GetAxis("Vertical") == 0)
        {
            textMudanca.text = "N";
        }
            if (Input.GetAxis("Vertical") == 1)
            {
                textMudanca.text = "1";
            } 
                if (Input.GetAxis("Vertical") == -1)
                {
                    textMudanca.text = "R";
                }
    }
}
   
