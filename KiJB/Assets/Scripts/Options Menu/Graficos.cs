using UnityEngine;
using TMPro;

public class GraficosManager : MonoBehaviour
{
    public TMP_Dropdown dropdownGraficos;

    void Start()
    {
        int qualidadeSalva = PlayerPrefs.GetInt("QualidadeGrafica", 1); // 1 = Medium por padrão
        dropdownGraficos.value = qualidadeSalva;
        MudarQualidade(qualidadeSalva);

        dropdownGraficos.onValueChanged.AddListener(MudarQualidade);
    }

    void MudarQualidade(int index)
    {
        QualitySettings.SetQualityLevel(index);
        PlayerPrefs.SetInt("QualidadeGrafica", index);
    }
}
