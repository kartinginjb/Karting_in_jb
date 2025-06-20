using UnityEngine;

public class rodas: MonoBehaviour
{
    public float anguloMaximo = 90f; // Ângulo máximo que o volante pode virar
    public float velocidadeRotacao = 5f; // Velocidade da transição

    private float inputHorizontal;
    private Quaternion rotacaoInicial;

    void Start()
    {
        rotacaoInicial = transform.localRotation;
    }

    void Update()
    {
        inputHorizontal = Input.GetAxis("Horizontal");

        // Calcula a rotação desejada
        float angulo = -inputHorizontal * anguloMaximo;

        Quaternion rotacaoDesejada = rotacaoInicial * Quaternion.Euler(0, -angulo, 0); // Ajusta o eixo conforme necessário
        transform.localRotation = Quaternion.Lerp(transform.localRotation, rotacaoDesejada, Time.deltaTime * velocidadeRotacao);
    }
}
