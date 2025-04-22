using System.Diagnostics;
using UnityEngine;

public class turbo : MonoBehaviour
{
    DRSsys[] drs;
    DRSsys[] carros;
    carro carro;

    private void Awake()
    {
        //Carregar checkpoints
        drs = FindObjectsOfType<DRSsys>();
    }

    private void OnTriggerEnter(Collider other)
    {
        carro x = other.transform.root.GetComponent<carro>();   //procura no objeto principal pelo script carro

        foreach(DRSsys ponto in drs)
        {
            if (ponto.PassouCarro())
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    carro.maxTorque = 4000;
                }
                else if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    carro.maxTorque = 3500;
                }
            }
        }

        Resetdrs();

    }

    void Resetdrs()
    {
        foreach (DRSsys cha in drs)
        {
            cha.carros = null;
        }
    }
}
