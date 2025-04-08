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
        textRPM.text = Mathf.RoundToInt(carro.rpm).ToString() + " RPM";
        textMudanca.text =(carro.mudancaAtual + 1).ToString();
        textVelocidade.text = Mathf.RoundToInt(carro.veloKMH).ToString() + " KM/H";
    }
}
