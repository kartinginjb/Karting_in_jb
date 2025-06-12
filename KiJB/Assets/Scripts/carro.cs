using System.Diagnostics;
using UnityEngine;

public class carro : MonoBehaviour
{
    // Array das 4 rodas com WheelCollider para controle físico do kart
    public WheelCollider[] guiar;

    // Variáveis de input horizontal (direção) e vertical (aceleração/freio)
    float guia = 0f;
    float acc = 0f;

    // Referência ao Rigidbody do kart para aplicar física
    Rigidbody rb;

    // Torque máximo aplicado ao motor das rodas traseiras
    public float maxTorque = 3500f;
    // Força de travagem aplicada às rodas traseiras
    public float forcaTravagem = 1400f;

    // Velocidade atual em km/h (calculada a partir do Rigidbody)
    public float veloKMH;
    // RPM do motor (calculado com base na velocidade e relação de marcha)
    public float rpm;

    // Relações das marchas para calcular RPM
    public float[] raioMudancas;
    // Índice da marcha atual
    public int mudancaAtual = 0;

    // Limites mínimo e máximo de RPM para troca de marcha
    public float minRPM = 1000f;
    public float maxRPM = 11000f;

    // Força final aplicada ao carro (vetor), útil para efeitos ou física extra
    public Vector3 forcaFinal;

    // Controla se o carro pode acelerar ou não no inicio da corrida
    public bool podeAcelerar = true;

    // Contador de voltas feitas pelo jogador
    public int voltas = 0;

    // Estado do ângulo de direção atual, para suavizar mudanças
    float steerAtual = 0f;

    // Array de sistemas turbo/DRS (Downforce Reduction System) para aceleração extra
    public turbo[] drs;

    // Som do carro e fonte de áudio para reproduzir o som do motor
    public AudioClip somCarro;
    public AudioSource audioCarro;

    void Start()
    {
        // Pega o Rigidbody do carro e ajusta o centro de massa para estabilidade
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0f, -1.2f, 0f);

        // Define o clip de som no AudioSource
        audioCarro.clip = somCarro;

        // Configura a fricção e suspensão de cada roda no começo do jogo
        for (int i = 0; i < guiar.Length; i++)
        {
            AjustarFriccao(guiar[i], i >= 2); // traseira = true para as duas últimas rodas
            AjustarSuspensao(guiar[i]);
        }
    }

    void Update()
    {
        // Lê inputs do jogador (horizontal para direção, vertical para aceleração/freio)
        guia = Input.GetAxis("Horizontal");
        acc = Input.GetAxis("Vertical");

        // Verifica se algum sistema de DRS está ativo para alterar torque e arrasto
        bool algumPodeUsarDRS = false;
        foreach (var sistemaDRS in drs)
        {
            if (sistemaDRS.podeUsarDRS)
            {
                algumPodeUsarDRS = true;
                break;
            }
        }

        // Ajusta torque e arrasto baseado no DRS
        if (algumPodeUsarDRS)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                maxTorque = 6000;
                rb.linearDamping = 0.05f;
            }
            else
            {
                maxTorque = 3500;
                rb.linearDamping = 0.1f;
            }
        }
    }

    // Função chamada para somar uma volta completa feita pelo jogador
    public void SomarVolta()
    {
        voltas++;
        UnityEngine.Debug.Log("Volta Realizada " + voltas);
    }

 void FixedUpdate()
{
    // Calcula a velocidade do kart em km/h na direção local para considerar marcha ré ou frente
    veloKMH = transform.InverseTransformDirection(rb.linearVelocity).z * 3.6f;

    // Input vertical bruto (pode ser negativo para freio, positivo para acelerar)
    float rawInput = podeAcelerar ? Input.GetAxis("Vertical") : 0f;
    // Aplica deadzone para ignorar inputs muito pequenos (evita aceleração residual)
    float input = Mathf.Abs(rawInput) < 0.1f ? 0f : rawInput;

    // Input horizontal para direção (steer)
    float steerInput = Input.GetAxis("Horizontal");

    // Calcula ângulo máximo de direção baseado na velocidade (mais rápido = ângulo menor)
    float steerMax = Mathf.Lerp(18f, 8f, Mathf.Abs(veloKMH) / 100f);
    float steerDesejado = steerInput * steerMax;

    // Suaviza a mudança do ângulo da direção para evitar mudanças bruscas
    steerAtual = Mathf.Lerp(steerAtual, steerDesejado, Time.fixedDeltaTime * 8f);

    // Aplica o ângulo de direção às rodas da frente
    guiar[0].steerAngle = steerAtual;
    guiar[1].steerAngle = steerAtual;

    // Inicializa força de travagem como zero
    float travagem = 0f;

    // Se o input for negativo (freio) e velocidade maior que 1 km/h, aplica travagem ativa
    if (input < -0.1f && veloKMH > 1f)
    {
        travagem = forcaTravagem;
        // Reduz arrasto para travagem ativa (mais realista)
        rb.linearDamping = Mathf.Lerp(rb.linearDamping, 0.1f, Time.fixedDeltaTime * 5f);
    }
    // Se não houver input vertical significativo, aplica travagem passiva suave
    else if (Mathf.Abs(input) < 0.05f)
    {
        // Define força base da travagem passiva (30%)
        float targetBrakeForce = forcaTravagem * 0.1f;

        // Se a velocidade for abaixo de 20 km/h, reduz travagem passiva para 10%
        if (Mathf.Abs(veloKMH) < 20f)
            targetBrakeForce = forcaTravagem * 0.05f;

        // Interpola suavemente a travagem para evitar mudanças bruscas
        travagem = Mathf.Lerp(guiar[2].brakeTorque, targetBrakeForce, Time.fixedDeltaTime * 1.5f);

        // Aumenta arrasto para desaceleração natural, suavizando a transição
        rb.linearDamping = Mathf.Lerp(rb.linearDamping, 1.5f, Time.fixedDeltaTime * 2f);
    }
    else
    {
        // Se estiver acelerando, mantém arrasto baixo para não prejudicar aceleração
        rb.linearDamping = Mathf.Lerp(rb.linearDamping, 0.1f, Time.fixedDeltaTime * 5f);
    }

    // Aplica a força de travagem às rodas traseiras
    guiar[2].brakeTorque = travagem;
    guiar[3].brakeTorque = travagem;

    // Aplica torque do motor somente se o input vertical for significativo (>= 0.05)
    float torqueFinal = Mathf.Abs(input) >= 0.05f ? input * maxTorque : 0f;

    // Aplica torque às rodas traseiras
    guiar[2].motorTorque = torqueFinal;
    guiar[3].motorTorque = torqueFinal;

    // Calcula o RPM do motor baseado na velocidade, marcha e fator constante
    rpm = Mathf.Abs(veloKMH) * raioMudancas[Mathf.Clamp(mudancaAtual, 0, raioMudancas.Length - 1)] * 15f;

    // Troca de marchas automática, sobe se RPM passar do máximo
    if (rpm > maxRPM)
        mudancaAtual = Mathf.Min(mudancaAtual + 1, raioMudancas.Length - 1);
    // Diminui marcha se RPM ficar abaixo do mínimo
    else if (rpm < minRPM)
        mudancaAtual = Mathf.Max(mudancaAtual - 1, 0);

    // Guarda a força final aplicada para efeitos visuais ou físicos adicionais
    forcaFinal = transform.forward * torqueFinal;

    // Aplica força aerodinâmica descendente para estabilidade em alta velocidade
    if (Mathf.Abs(veloKMH) > 10f)
        rb.AddForce(-transform.up * Mathf.Abs(veloKMH) * 40f);

    // Força extra para ajudar em subidas, baseada na inclinação do terreno e input de aceleração
    Vector3 direcaoSubida = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
    float inclinacao = Vector3.Dot(transform.forward, Vector3.up);

    if (inclinacao > 0.1f && input > 0.1f)
    {
        float compensacao = inclinacao * 1000f;
        rb.AddForce(direcaoSubida * compensacao, ForceMode.Force);
    }

    // Aplica força anti-roll para reduzir inclinação lateral nas curvas (frente e trás)
    AplicarAntiRoll(guiar[0], guiar[1], 10000f);
    AplicarAntiRoll(guiar[2], guiar[3], 8000f);

    // Ajusta o pitch do som do motor baseado na velocidade para dar feedback sonoro
    audioCarro.pitch = 0.6f + Mathf.Abs(veloKMH) / 50f;
}



    // Ajusta a fricção lateral e longitudinal das rodas
    void AjustarFriccao(WheelCollider roda, bool traseira)
    {
        WheelFrictionCurve fric = roda.sidewaysFriction;
        fric.extremumSlip = 0.3f;
        fric.extremumValue = 1.2f;
        fric.asymptoteSlip = 0.6f;
        fric.asymptoteValue = 0.9f;
        fric.stiffness = traseira ? 1.1f : 1.3f;
        roda.sidewaysFriction = fric;

        WheelFrictionCurve forward = roda.forwardFriction;
        forward.extremumSlip = 0.4f;
        forward.extremumValue = 1.25f;
        forward.asymptoteSlip = 0.6f;
        forward.asymptoteValue = 0.9f;
        forward.stiffness = traseira ? 1.0f : 1.15f;
        roda.forwardFriction = forward;
    }

    // Ajusta a suspensão das rodas (rigidez, amortecimento, distância)
    void AjustarSuspensao(WheelCollider roda)
    {
        JointSpring spring = roda.suspensionSpring;
        spring.spring = 50000f;
        spring.damper = 1200f;
        roda.suspensionSpring = spring;
        roda.suspensionDistance = 0.35f;
        roda.forceAppPointDistance = 0.02f;
    }

    // Aplica força anti-roll para reduzir inclinação lateral do carro nas curvas
    void AplicarAntiRoll(WheelCollider rodaEsq, WheelCollider rodaDir, float forca)
    {
        WheelHit hit;
        float travelEsq = 1f;
        float travelDir = 1f;

        // Verifica se a roda esquerda está tocando o chão e calcula deslocamento da suspensão
        bool toqueEsq = rodaEsq.GetGroundHit(out hit);
        if (toqueEsq)
            travelEsq = (-rodaEsq.transform.InverseTransformPoint(hit.point).y - rodaEsq.radius) / rodaEsq.suspensionDistance;

        // Verifica se a roda direita está tocando o chão e calcula deslocamento da suspensão
        bool toqueDir = rodaDir.GetGroundHit(out hit);
        if (toqueDir)
            travelDir = (-rodaDir.transform.InverseTransformPoint(hit.point).y - rodaDir.radius) / rodaDir.suspensionDistance;

        // Calcula força anti-roll proporcional à diferença de deslocamento das suspensões
        float forcaAntiRoll = (travelEsq - travelDir) * forca;

        // Aplica força para empurrar/suportar a carroceria e reduzir rolamento lateral
        if (toqueEsq)
            rb.AddForceAtPosition(rodaEsq.transform.up * -forcaAntiRoll, rodaEsq.transform.position);
        if (toqueDir)
            rb.AddForceAtPosition(rodaDir.transform.up * forcaAntiRoll, rodaDir.transform.position);
    }
}
