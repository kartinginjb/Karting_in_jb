using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectSelectionMenu : MonoBehaviour
{
    public GameObject[] selectableObjects; // Array de objetos selecion�veis
    private int currentIndex = 0; // �ndice do objeto atualmente selecionado

    private void Start()
    {
        // Inicializa o menu, mostrando o primeiro objeto
        UpdateSelection();
    }

    // M�todo para selecionar um objeto pelo �ndice
    public void SelectObject(int index)
    {
        if (index >= 0 && index < selectableObjects.Length)
        {
            currentIndex = index;
            UpdateSelection();
        }
        else
        {
            Debug.LogWarning("�ndice fora dos limites.");
        }
    }

    // M�todo para atualizar a visualiza��o do objeto selecionado
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

    // M�todo para confirmar a sele��o
    public void ConfirmSelection()
    {
        // Salva o �ndice do objeto selecionado (para persistir entre cenas)
        PlayerPrefs.SetInt("SelectedObjectIndex", currentIndex);
        PlayerPrefs.Save(); // Garante que os dados sejam salvos

        Debug.Log("Objeto selecionado: " + selectableObjects[currentIndex].name);

        // Carrega a pr�xima cena (substitua "NextScene" pelo nome da sua cena)
        SceneManager.LoadScene("NextScene");
    }
}