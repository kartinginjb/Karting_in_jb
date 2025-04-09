using UnityEngine;

public class rodas: MonoBehaviour
{
    public float anguloMaximo = 90f; // �ngulo m�ximo que o volante pode virar
    public float velocidadeRotacao = 5f; // Velocidade da transi��o

    private float inputHorizontal;
    private Quaternion rotacaoInicial;

    void Start()
    {
        rotacaoInicial = transform.localRotation;
    }

    void Update()
    {
        inputHorizontal = Input.GetAxis("Horizontal");

        // Calcula a rota��o desejada
        float angulo = -inputHorizontal * anguloMaximo;

        Quaternion rotacaoDesejada = rotacaoInicial * Quaternion.Euler(0, -angulo, 0); // Ajusta o eixo conforme necess�rio
        transform.localRotation = Quaternion.Lerp(transform.localRotation, rotacaoDesejada, Time.deltaTime * velocidadeRotacao);
    }
}
