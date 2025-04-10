using UnityEngine;
using TMPro;

public class NumeroJogador : MonoBehaviour
{
    [Range(1, 99)]
    public int numero = 1; // Número escolhido pelo jogador

    public TextMeshPro numeroTexto; // Arrastar o TMP do nariz aqui

    void Start()
    {
        AtualizarNumero();
    }

    public void AtualizarNumero()
    {
        if (numeroTexto != null)
        {
            numeroTexto.text = numero.ToString("D2"); // Ex: mostra 01, 09, 23...
        }
    }

    public void DefinirNumero(int novoNumero)
    {
        numero = Mathf.Clamp(novoNumero, 1, 99);
        AtualizarNumero();
    }
}
