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
        rb.centerOfMass = new Vector3(0f, -0.5f, -0.5f); // centro de massa mais baixo para estabilidade

        // Cache dos waypoints
        waypoints = new Transform[caminho.childCount];
        for (int i = 0; i < caminho.childCount; i++)
            waypoints[i] = caminho.GetChild(i);
    }

    void FixedUpdate()
    {
        if (!podeAcelerar || waypoints.Length == 0) return;

        // Direção para o waypoint atual
        Transform alvo = waypoints[indiceAtual];
        Vector3 direcao = alvo.position - transform.position;
        direcao.y = 0f;
        direcao.Normalize();

        // Deteção de IA à frente
        Vector3 origem = transform.position + Vector3.up * 0.5f;
        RaycastHit hit;
        bool haIAaFrente = Physics.SphereCast(origem, raioDetecao, transform.forward, out hit, detecaoDistancia, layerIA);

        if (haIAaFrente)
        {
            Vector3 ladoDireito = transform.position + transform.right * desvioMaximo;
            Vector3 ladoEsquerdo = transform.position - transform.right * desvioMaximo;

            bool espacoDireita = !Physics.CheckSphere(ladoDireito, 0.5f, layerIA);
            bool espacoEsquerda = !Physics.CheckSphere(ladoEsquerdo, 0.5f, layerIA);

            if (espacoDireita)
                desvioAtual = Mathf.Lerp(desvioAtual, desvioMaximo, 0.05f);
            else if (espacoEsquerda)
                desvioAtual = Mathf.Lerp(desvioAtual, -desvioMaximo, 0.05f);
        }
        else
        {
            desvioAtual = Mathf.Lerp(desvioAtual, 0f, 0.05f);
        }

        // Direção final com desvio
        Vector3 desvio = transform.right * desvioAtual;
        Vector3 movimentoFinal = (direcao + desvio).normalized;

        // Movimento SEM travar
        Vector3 novaPos = rb.position + movimentoFinal * velocidade * Time.fixedDeltaTime;
        rb.MovePosition(novaPos);

        // Roda suavemente, evitando giros completos
        if (Vector3.Dot(transform.forward, movimentoFinal) > -0.5f)
        {
            Quaternion rotacaoAlvo = Quaternion.LookRotation(movimentoFinal);
            rb.rotation = Quaternion.Slerp(rb.rotation, rotacaoAlvo, rotacaoVelocidade * Time.fixedDeltaTime);
        }

        // Força descendente para estabilidade
        Vector3 posTraseira = transform.position - transform.forward * 0.5f;
        rb.AddForceAtPosition(Vector3.down * 10f, posTraseira);

        // Próximo waypoint
        if (Vector3.Distance(transform.position, alvo.position) < distanciaMinima)
        {
            indiceAtual = (indiceAtual + 1) % waypoints.Length;
        }
    }
}
