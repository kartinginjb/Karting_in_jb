using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class KartAgent : Agent
{
    public Transform pistaCentro;
    public Rigidbody rb;
    public float velocidade = 10f;
    public float virarVelocidade = 100f;
    public LayerMask pistaLayer;

    private Vector3 posInicial;
    private Quaternion rotInicial;

    public override void Initialize()
    {
        posInicial = transform.localPosition;
        rotInicial = transform.localRotation;
    }

    public override void OnEpisodeBegin()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.localPosition = posInicial;
        transform.localRotation = rotInicial;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(transform.forward);
        sensor.AddObservation(rb.velocity);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float acelerar = Mathf.Clamp(actions.ContinuousActions[0], -1f, 1f);
        float virar = Mathf.Clamp(actions.ContinuousActions[1], -1f, 1f);

        rb.AddForce(transform.forward * acelerar * velocidade, ForceMode.Force);
        transform.Rotate(0, virar * virarVelocidade * Time.deltaTime, 0);

        // ----------- RECOMPENSAS -----------

        // 1. Recompensa por andar para a frente
        float direcao = Vector3.Dot(rb.velocity.normalized, transform.forward);
        AddReward(0.05f * direcao);  // Valor pequeno e constante

        // 2. Penalização por sair da pista (com Raycast para verificar o chão)
        if (!Physics.Raycast(transform.position, Vector3.down, 1.0f, pistaLayer))
        {
            AddReward(-1.0f);
            EndEpisode();
        }

        // 3. Penalização por velocidade demasiado baixa
        if (rb.velocity.magnitude < 1f)
        {
            AddReward(-0.01f);
        }

        // 4. Recompensa extra por estar alinhado com a pista
        Vector3 direcaoParaCentro = (pistaCentro.position - transform.position).normalized;
        float alinhamento = Vector3.Dot(transform.forward, direcaoParaCentro);
        AddReward(0.02f * alinhamento);  // Mais se estiver bem orientado

        // 5. Pequena penalização por virar em excesso (evita zigzags)
        AddReward(-0.005f * Mathf.Abs(virar));
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Vertical");
        continuousActionsOut[1] = Input.GetAxis("Horizontal");
    }
}
