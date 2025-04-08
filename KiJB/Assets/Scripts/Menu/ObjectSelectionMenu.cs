using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectSelectionMenu : MonoBehaviour
{
    public GameObject[] selectableObjects; // Array de objetos selecionáveis
    private int currentIndex = 0; // Índice do objeto atualmente selecionado

    private void Start()
    {
        // Inicializa o menu, mostrando o primeiro objeto
        UpdateSelection();
    }

    // Método para selecionar um objeto pelo índice
    public void SelectObject(int index)
    {
        if (index >= 0 && index < selectableObjects.Length)
        {
            currentIndex = index;
            UpdateSelection();
        }
        else
        {
            Debug.LogWarning("Índice fora dos limites.");
        }
    }

    // Método para atualizar a visualização do objeto selecionado
    private void UpdateSelection()
    {
        // Desativa todos os objetos
        foreach (var obj in selectableObjects)
        {
            obj.SetActive(false);
        }

        // Ativa o objeto selecionado
        selectableObjects[currentIndex].SetActive(true);
    }

    // Método para confirmar a seleção
    public void ConfirmSelection()
    {
        // Salva o índice do objeto selecionado (para persistir entre cenas)
        PlayerPrefs.SetInt("SelectedObjectIndex", currentIndex);
        PlayerPrefs.Save(); // Garante que os dados sejam salvos

        Debug.Log("Objeto selecionado: " + selectableObjects[currentIndex].name);

        // Carrega a próxima cena (substitua "NextScene" pelo nome da sua cena)
        SceneManager.LoadScene("NextScene");
    }
}