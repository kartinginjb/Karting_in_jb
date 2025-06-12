using UnityEngine;

public class KartSeguidor : MonoBehaviour
{
    public Transform caminho;
    public float velocidade = 5f;
    public float rotacaoVelocidade = 5f;
    public float distanciaMinima = 1f;
    public bool podeAcelerar = false;

    private Transform[] waypoints;
    private int indiceAtual = 0;
    private Rigidbody rb;

    public float desvioMaximo = 2f;
    public LayerMask layerIA;
    public float detecaoDistancia = 5f;
    public float raioDetecao = 1f;

    private float desvioAtual = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        waypoints = new Transform[caminho.childCount];
        for (int i = 0; i < caminho.childCount; i++)
            waypoints[i] = caminho.GetChild(i);
    }

    void FixedUpdate()
    {
        if (!podeAcelerar || waypoints.Length == 0) return;

        Transform alvo = waypoints[indiceAtual];
        Vector3 direcao = (alvo.position - transform.position);
        direcao.y = 0f;
        direcao.Normalize();

        // Detetar IA à frente com SphereCast
        Vector3 origem = transform.position + Vector3.up * 0.5f;
        RaycastHit hit;
        bool haIAaFrente = Physics.SphereCast(origem, raioDetecao, transform.forward, out hit, detecaoDistancia, layerIA);

        if (haIAaFrente)
        {
            // Decide se há espaço à esquerda ou direita
            Vector3 ladoDireito = transform.position + transform.right * desvioMaximo;
            Vector3 ladoEsquerdo = transform.position - transform.right * desvioMaximo;

            bool espacoDireita = !Physics.CheckSphere(ladoDireito, 0.5f, layerIA);
            bool espacoEsquerda = !Physics.CheckSphere(ladoEsquerdo, 0.5f, layerIA);

            if (espacoDireita)
                desvioAtual = Mathf.Lerp(desvioAtual, desvioMaximo, 0.1f);
            else if (espacoEsquerda)
                desvioAtual = Mathf.Lerp(desvioAtual, -desvioMaximo, 0.1f);
        }
        else
        {
            // Volta gradualmente ao centro da pista
            desvioAtual = Mathf.Lerp(desvioAtual, 0f, 0.05f);
        }

        // Aplica o desvio lateral
        Vector3 desvio = transform.right * desvioAtual;
        Vector3 movimentoFinal = (direcao + desvio).normalized;

        // Aplicar velocidade mantendo Y
        Vector3 velocidadeDesejada = movimentoFinal * velocidade;
        rb.linearVelocity = new Vector3(velocidadeDesejada.x, rb.linearVelocity.y, velocidadeDesejada.z);

        // Suavizar rotação
        if (movimentoFinal != Vector3.zero)
        {
            Quaternion rotacaoAlvo = Quaternion.LookRotation(movimentoFinal);
            rb.rotation = Quaternion.Slerp(rb.rotation, rotacaoAlvo, rotacaoVelocidade * Time.fixedDeltaTime);
        }

        // Passar para o próximo waypoint
        if (Vector3.Distance(transform.position, alvo.position) < distanciaMinima)
        {
            indiceAtual = (indiceAtual + 1) % waypoints.Length;
        }
    }
}
