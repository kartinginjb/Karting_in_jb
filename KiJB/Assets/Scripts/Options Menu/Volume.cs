using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeManager : MonoBehaviour
{
    public Slider sliderVolume;
    public TextMeshProUGUI textoVolume;

    void Start()
    {
        // Carrega valor salvo (0 a 100)
        float valorSalvo = PlayerPrefs.GetFloat("VolumeInvertido", 0f);
        valorSalvo = Mathf.Clamp(valorSalvo, 0f, 100f);
        sliderVolume.value = valorSalvo;

        AlterarVolume(valorSalvo);
        sliderVolume.onValueChanged.AddListener(AlterarVolume);
    }

    void AlterarVolume(float valorSlider)
    {
        valorSlider = Mathf.Clamp(valorSlider, 0f, 100f);

        // Inverte o valor para o volume real (0 a 1)
        float volumeReal = 1f - (valorSlider / 100f);
        AudioListener.volume = volumeReal;

        PlayerPrefs.SetFloat("VolumeInvertido", valorSlider);

        if (textoVolume != null)
        {
            int porcentagem = Mathf.RoundToInt(volumeReal * 100f);
            textoVolume.text = porcentagem + "%";
        }
    }
}
