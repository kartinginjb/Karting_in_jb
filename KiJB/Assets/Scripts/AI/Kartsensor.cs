using System.Diagnostics;
using UnityEngine;

public class KartSensor : MonoBehaviour
{
    public float sensorDistancia = 5f;
    public LayerMask layerObstaculos;
    private Vector3[] direcoesSensores;
    public bool podeAcelerar = true;

    [Header("Waypoints")]
    public Transform waypointPai; // Objeto pai com os waypoints como filhos
    private Transform[] waypoints;
    private int indexAtual = 0;
    public float velocidade = 10f;
    public float forcaRotacao = 5f;
    public float distanciaMinima = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Obter todos os filhos do objeto pai como waypoints
        waypoints = new Transform[waypointPai.childCount];
        for (int i = 0; i < waypointPai.childCount; i++)
        {
            waypoints[i] = waypointPai.GetChild(i);
        }

        // Criar 16 direções distribuídas a 360 graus
        direcoesSensores = new Vector3[16];
        for (int i = 0; i < 16; i++)
        {
            float angulo = i * 22.5f;
            float rad = angulo * Mathf.Deg2Rad;
            direcoesSensores[i] = new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad));
        }
    }

    void FixedUpdate()
    {
        AtualizarSensores();

        if (!podeAcelerar || waypoints.Length == 0) return;

        Transform alvo = waypoints[indexAtual];
        Vector3 direcao = (alvo.position - transform.position).normalized;

        // Rotação suave
        Quaternion rotacaoAlvo = Quaternion.LookRotation(direcao);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacaoAlvo, forcaRotacao * Time.fixedDeltaTime);

        // Movimento para a frente
        rb.MovePosition(rb.position + transform.forward * velocidade * Time.fixedDeltaTime);

        // Mudar de waypoint
        float distancia = Vector3.Distance(transform.position, alvo.position);
        if (distancia < distanciaMinima)
        {
            indexAtual = (indexAtual + 1) % waypoints.Length;
        }
    }

    void AtualizarSensores()
    {
        podeAcelerar = true;

        for (int i = 0; i < direcoesSensores.Length; i++)
        {
            Vector3 direcao = transform.TransformDirection(direcoesSensores[i]);
            Ray ray = new Ray(transform.position + Vector3.up * 0.5f, direcao);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, sensorDistancia, layerObstaculos))
            {
                UnityEngine.Debug.DrawRay(ray.origin, direcao * hit.distance, Color.red);

                if (i == 15 || i == 0 || i == 1 || i == 2)
                {
                    podeAcelerar = false;
                }
            }
            else
            {
                UnityEngine.Debug.DrawRay(ray.origin, direcao * sensorDistancia, Color.green);
            }
        }
    }
}
