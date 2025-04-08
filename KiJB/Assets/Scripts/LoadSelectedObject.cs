using System.Diagnostics;
using UnityEngine;

public class LoadSelectedObject : MonoBehaviour
{
    public GameObject kart; // Referência ao GameObject pai "kart"

    private void Start()
    {
        // Encontra o GameObject filho "Karts" dentro de "kart"
        Transform karts = kart.transform.Find("Karts");

        // Verifica se o GameObject "Karts" foi encontrado
        if (karts == null)
        {
            //Debug.LogError("Objeto filho 'Karts' não encontrado dentro de 'kart'.");
            return;
        }

        // Recupera o índice do objeto selecionado
        int selectedIndex = PlayerPrefs.GetInt("SelectedObjectIndex", 0); // 0 é o valor padrão

        // Verifica se o índice é válido
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
            //Debug.LogWarning("Índice do objeto selecionado inválido.");
        }
    }
}