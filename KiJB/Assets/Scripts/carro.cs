using System.Diagnostics;
using UnityEngine;

public class carro : MonoBehaviour
{
    public WheelCollider[] guiar; // [0]=frente esq, [1]=frente dir, [2]=trás esq, [3]=trás dir

    float guia = 0f;
    float acc = 0f;

    Rigidbody rb;

    public float maxTorque = 3500f;
    public float forcaTravagem = 400f;

    public float veloKMH;
    public float rpm;

    public float[] raioMudancas;
    public int mudancaAtual = 0;

    public float minRPM = 1000f;
    public float maxRPM = 7000f;

    public Vector3 forcaFinal;

    public bool podeAcelerar = true;
    private bool emMarchaRe = false;

    public int voltas = 0;

    float steerAtual = 0f;

    public turbo drs;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0f, -1f, 0f); // 🔽 baixar bem o centro de massa

        for (int i = 0; i < guiar.Length; i++)
        {
            AjustarFriccao(guiar[i], i >= 2);
            AjustarSuspensao(guiar[i]);
        }


    }

    void Update()
    {
        guia = Input.GetAxis("Horizontal");
        acc = Input.GetAxis("Vertical");

        if(drs.podeUsarDRS != false)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                maxTorque = 4000;
            }
            else
            {
                maxTorque = 3500;
            }
        }
    }

    public void SomarVolta()
    {
        voltas++;
        UnityEngine.Debug.Log("Volta Realizada " + voltas);
    }

    void FixedUpdate()
    {
        veloKMH = rb.linearVelocity.magnitude * 3.6f;
        float input = podeAcelerar ? acc : 0f;

        // Direção firme e controlada
        float steerMax = Mathf.Lerp(18f, 8f, rb.linearVelocity.magnitude / 100f);
        float steerDesejado = guia * steerMax;
        steerAtual = Mathf.Lerp(steerAtual, steerDesejado, Time.fixedDeltaTime * 8f);

        guiar[0].steerAngle = steerAtual;
        guiar[1].steerAngle = steerAtual;

        // Travagem sensata
        float travagem = 0f;
        if (input < -0.1f && veloKMH > 2f && !emMarchaRe)
        {
            travagem = forcaTravagem;
        }
        else if (Mathf.Abs(input) < 0.05f && veloKMH > 5f)
        {
            travagem = forcaTravagem * 0.3f;
        }

        guiar[2].brakeTorque = travagem;
        guiar[3].brakeTorque = travagem;

        // Lógica de marcha atrás com suavidade
        if (veloKMH < 1f && input < -0.1f)
            emMarchaRe = true;
        else if (input > 0.1f)
            emMarchaRe = false;

        float torqueFinal = 0f;

        if (emMarchaRe)
            torqueFinal = Mathf.Clamp(input, -1f, 0f) * maxTorque * 0.4f;
        else if (input > 0.01f)
            torqueFinal = input * maxTorque;

        guiar[2].motorTorque = torqueFinal;
        guiar[3].motorTorque = torqueFinal;

        // Lógica de mudanças
        rpm = veloKMH * raioMudancas[Mathf.Clamp(mudancaAtual, 0, raioMudancas.Length - 1)] * 15f;

        if (rpm > maxRPM)
            mudancaAtual = Mathf.Min(mudancaAtual + 1, raioMudancas.Length - 1);
        else if (rpm < minRPM)
            mudancaAtual = Mathf.Max(mudancaAtual - 1, 0);

        forcaFinal = transform.forward * torqueFinal;

        // Força descendente constante
        rb.AddForce(-transform.up * veloKMH * 50f);

        // Aplica barra estabilizadora nos dois eixos
        AplicarAntiRoll(guiar[0], guiar[1], 10000f); // frente
        AplicarAntiRoll(guiar[2], guiar[3], 8000f);  // trás

    }

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

    void AjustarSuspensao(WheelCollider roda)
    {
        JointSpring spring = roda.suspensionSpring;
        spring.spring = 50000f;    // ainda mais firme
        spring.damper = 1200f;     // mais controle nos saltos
        roda.suspensionSpring = spring;
        roda.suspensionDistance = 0.35f; // menor distância ainda

        roda.forceAppPointDistance = 0.02f; // bem junto à roda
    }

    void AplicarAntiRoll(WheelCollider rodaEsq, WheelCollider rodaDir, float forca)
    {
        WheelHit hit;
        float travelEsq = 1f;
        float travelDir = 1f;

        bool toqueEsq = rodaEsq.GetGroundHit(out hit);
        if (toqueEsq)
            travelEsq = (-rodaEsq.transform.InverseTransformPoint(hit.point).y - rodaEsq.radius) / rodaEsq.suspensionDistance;

        bool toqueDir = rodaDir.GetGroundHit(out hit);
        if (toqueDir)
            travelDir = (-rodaDir.transform.InverseTransformPoint(hit.point).y - rodaDir.radius) / rodaDir.suspensionDistance;

        float forcaAntiRoll = (travelEsq - travelDir) * forca;

        if (toqueEsq)
            rb.AddForceAtPosition(rodaEsq.transform.up * -forcaAntiRoll, rodaEsq.transform.position);
        if (toqueDir)
            rb.AddForceAtPosition(rodaDir.transform.up * forcaAntiRoll, rodaDir.transform.position);
    }
}

