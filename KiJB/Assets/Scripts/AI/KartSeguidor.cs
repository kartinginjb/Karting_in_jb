using UnityEngine;

public class KartSeguidor : MonoBehaviour
{
    public Transform caminho;
    public float velocidade = 5f;
    public float rotacaoVelocidade = 5f;
    public float distanciaMinima = 1f;

    public bool podeAcelerar = false; // <--- Controlado pelas luzes

    private Transform[] waypoints;
    private int indiceAtual = 0;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        waypoints = new Transform[caminho.childCount];
        for (int i = 0; i < caminho.childCount; i++)
        {
            waypoints[i] = caminho.GetChild(i);
        }
    }

    void FixedUpdate()
    {
        if (!podeAcelerar || waypoints.Length == 0) return;

        Transform alvo = waypoints[indiceAtual];
        Vector3 direcao = (alvo.position - transform.position).normalized;
        direcao.y = 0f;

        rb.linearVelocity = direcao * velocidade;

        if (direcao != Vector3.zero)
        {
            Quaternion rotacaoAlvo = Quaternion.LookRotation(direcao);
            rb.rotation = Quaternion.Slerp(rb.rotation, rotacaoAlvo, rotacaoVelocidade * Time.fixedDeltaTime);
        }

        if (Vector3.Distance(transform.position, alvo.position) < distanciaMinima)
        {
            indiceAtual = (indiceAtual + 1) % waypoints.Length;
        }
    }
}
