using System;
using System.Diagnostics;
using UnityEngine;

public class KartSensor : MonoBehaviour
{
    public float sensorDistancia = 5f;
    public LayerMask layerObstaculos;
    private Vector3[] direcoesSensores;
    public bool podeAcelerar = false;

    [Header("Waypoints")]
    public Transform waypointPai;
    private Transform[] waypoints;
    private int indexAtual = 0;

    [Header("Movimento")]
    public float velocidade;
    public float forcaRotacao;
    public float distanciaMinima;

    private Rigidbody rb;

    public enum EstiloConducao { Agressivo, Cauteloso, Desportivo, Impulsivo }
    public int estilo;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        estilo = UnityEngine.Random.Range(1, 4);

        // Aplica o estilo de condução
        switch (estilo)
        {
            case 1:
                velocidade = UnityEngine.Random.Range(11f, 13f);
                forcaRotacao = 10f;
                distanciaMinima = 4f;
                break;
            case 2:
                velocidade = UnityEngine.Random.Range(8f, 9f);
                forcaRotacao = 9f;
                distanciaMinima = 4f;
                break;
            case 3:
                velocidade = UnityEngine.Random.Range(9.5f, 11f);
                forcaRotacao = 8f;
                distanciaMinima = 5f;
                break;
            case 4:
                velocidade = UnityEngine.Random.Range(10f, 12f);
                forcaRotacao = 11f;
                distanciaMinima = 5f;
                break;
        }

        // Waypoints
        waypoints = new Transform[waypointPai.childCount];
        for (int i = 0; i < waypointPai.childCount; i++)
        {
            waypoints[i] = waypointPai.GetChild(i);
        }

        // Direções para sensores (360º)
        direcoesSensores = new Vector3[24];
        for (int i = 0; i < 24; i++)
        {
            float angulo = i * 15f;
            float rad = angulo * Mathf.Deg2Rad;
            direcoesSensores[i] = new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad));
        }
    }

    void FixedUpdate()
    {
        AtualizarSensores();

        if (!podeAcelerar || waypoints.Length == 0) return;

        Transform alvo = waypoints[indexAtual];

        // Permite seguir o waypoint sem ir ao centro
        Vector3 offsetLateral = transform.right * UnityEngine.Random.Range(-2f, 2f);
        Vector3 alvoComOffset = alvo.position + offsetLateral;
        Vector3 direcao = (alvoComOffset - transform.position).normalized;

        // Curvas ajustam velocidade
        float angulo = Vector3.Angle(transform.forward, direcao);
        float fatorCurva = Mathf.InverseLerp(0f, 90f, angulo);
        float velocidadeAtual = Mathf.Lerp(velocidade, velocidade * 0.4f, fatorCurva);
        float rotacaoAtual = Mathf.Lerp(forcaRotacao, forcaRotacao * 2f, fatorCurva);

        // Rodar e mover
        Quaternion rotacaoAlvo = Quaternion.LookRotation(direcao);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacaoAlvo, rotacaoAtual * Time.fixedDeltaTime);
        rb.MovePosition(rb.position + transform.forward * velocidadeAtual * Time.fixedDeltaTime);

        // Passar para o próximo waypoint
        float distancia = Vector3.Distance(transform.position, alvo.position);
        if (distancia < distanciaMinima)
        {
            indexAtual = (indexAtual + 1) % waypoints.Length;
        }

        // Detetar IA e desviar
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, transform.forward, out RaycastHit hit, 5f))
        {
            if (hit.transform.CompareTag("AI"))
            {
                Vector3 direcaoDesvio = UnityEngine.Random.value > 0.5f ? transform.right : -transform.right;
                transform.position += direcaoDesvio * Time.fixedDeltaTime * 2f;
            }
        }
    }

    void AtualizarSensores()
    {
        bool temObstaculoFrontal = false;

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
                    temObstaculoFrontal = true;
                }
            }
            else
            {
                UnityEngine.Debug.DrawRay(ray.origin, direcao * sensorDistancia, Color.green);
            }
        }

    }
}
