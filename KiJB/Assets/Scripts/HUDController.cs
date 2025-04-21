using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    public TextMeshProUGUI textRPM;
    public TextMeshProUGUI textMudanca;
    public TextMeshProUGUI textVelocidade;

    public carro carro; // Referência ao script do carro

    void Update()
    {
        textRPM.text = Mathf.RoundToInt(carro.rpm).ToString() + " RPM";             //recebe o valor das RPM do script carro
        textVelocidade.text = Mathf.RoundToInt(carro.veloKMH).ToString() + " KM/H"; //recebe o valor da velocidade do script carro
            
            //verifica se o jogador está a acelerar para a frente // trás 
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
   
