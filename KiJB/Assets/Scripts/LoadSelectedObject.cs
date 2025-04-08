using System.Diagnostics;
using UnityEngine;

public class LoadSelectedObject : MonoBehaviour
{
    public GameObject kart; // Refer�ncia ao GameObject pai "kart"

    private void Start()
    {
        // Encontra o GameObject filho "Karts" dentro de "kart"
        Transform karts = kart.transform.Find("Karts");

        // Verifica se o GameObject "Karts" foi encontrado
        if (karts == null)
        {
            //Debug.LogError("Objeto filho 'Karts' n�o encontrado dentro de 'kart'.");
            return;
        }

        // Recupera o �ndice do objeto selecionado
        int selectedIndex = PlayerPrefs.GetInt("SelectedObjectIndex", 0); // 0 � o valor padr�o

        // Verifica se o �ndice � v�lido
        if (selectedIndex >= 0 && selectedIndex < karts.childCount)
        {
            // Desativa todos os objetos filhos de "Karts"
            for (int i = 0; i < karts.childCount; i++)
            {
                karts.GetChild(i).gameObject.SetActive(false);
            }

            // Ativa o objeto selecionado
            karts.GetChild(selectedIndex).gameObject.SetActive(true);
            //Debug.Log("Objeto carregado: " + karts.GetChild(selectedIndex).name);
        }
        else
        {
            //Debug.LogWarning("�ndice do objeto selecionado inv�lido.");
        }
    }
}