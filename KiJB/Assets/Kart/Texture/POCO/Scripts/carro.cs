using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class carro : MonoBehaviour
{
    public WheelCollider[] guiar;

    float guia=0f;   //Variavel para receber o valor horizontal
    float acc=0f;   //Variavel para receber o valor vertial

    Rigidbody rb;   //Variavel para reconehcer o rigidboddy

    public float maxTorque; //Perminte adicionar uma força para o carro andar

    public float forcaTravagem;

    float veloKMH;
    float rpm;

    public float[] raioMudancas;
    int mudancaAtual = 0;

    public float minRPM;
    public float maxRPM;

    public Vector3 forcaFinal;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
            guia = Input.GetAxis("Horizontal");  //Recebe o valor horizontal 
            acc = Input.GetAxis("Vertical");    //Recebe o valor horizontal
    }
    
    void FixedUpdate()
    {
        for (int i = 0; i < guiar.Length; i++)
        {
            guiar[i].steerAngle = guia * 15f ; //Permite o carro virar
            guiar[i].motorTorque = 1f;              //Nao perminte o carro parar
        }

        //Velocidade e rpm
        veloKMH = rb.linearVelocity.magnitude * 3.6f;
        rpm = veloKMH * raioMudancas[mudancaAtual] * 15f;
        rb.AddForce(-transform.up * 500f); // Aumenta a pressão para o chão

        //mudancas
        if(rpm>maxRPM)
        {
            mudancaAtual++;
            if(mudancaAtual != raioMudancas.Length)
            {
                mudancaAtual--;
            }
        }
        if(rpm<minRPM)
        {
            mudancaAtual--;
            if(mudancaAtual<0)
            {
                mudancaAtual = 0;
            }
        }

        //forcas
        if(acc<-0.1f)
        {
            rb.AddForce(-transform.forward * forcaTravagem);
            acc = 0;
        }

        forcaFinal = transform.forward * (maxTorque / (mudancaAtual + 1) + maxTorque / 3f) * acc;
        rb.AddForce(forcaFinal);   //Permiete o carro andar
    }

    void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 128, 32), veloKMH + "KM/H");
        GUI.Label(new Rect(20, 50, 128, 32), rpm + "RPM");
        GUI.Label(new Rect(20, 70, 128, 32), (mudancaAtual+1).ToString());
        GUI.Label(new Rect(20, 90, 128, 32), forcaFinal.magnitude.ToString());
    }
}