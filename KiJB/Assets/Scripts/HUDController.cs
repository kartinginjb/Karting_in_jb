using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    public TextMeshProUGUI textRPM;
    public TextMeshProUGUI textMudanca;
    public TextMeshProUGUI textVelocidade;
    public TextMeshProUGUI textVoltas;

    public carro carro; // Referência ao script do carro

    void Update()
    {
        // Mostrar número de voltas
        textVoltas.text = Mathf.RoundToInt(carro.voltas).ToString() + " Voltas";

        // Mostrar RPM e Velocidade
        textRPM.text = Mathf.RoundToInt(carro.rpm).ToString() + " RPM";
        textVelocidade.text = Mathf.RoundToInt(carro.veloKMH).ToString() + " KM/H";

        // Lógica realista da marcha
        if (carro.emMarchaRe)
        {
            textMudanca.text = "R";
        }
        else if (carro.veloKMH < 1f)
        {
            textMudanca.text = "N";
        }
        else
        {
            textMudanca.text = "1";
        }
    }
}
