using System.Diagnostics;
using UnityEngine;

public class turbo : MonoBehaviour
{
    private DRSsys[] zonasDRS;
    public bool podeUsarDRS = false;

    private void Awake()
    {
        zonasDRS = FindObjectsByType<DRSsys>(FindObjectsSortMode.None);
    }

    private void OnTriggerStay(Collider other)
    {
        carro carroObj = other.transform.root.GetComponent<carro>();

        if (carroObj == null)
            return;

        GameObject objCarro = other.transform.root.gameObject;

        foreach (DRSsys ponto in zonasDRS)
        {
            if (ponto.PassouCarro(objCarro))
            {
                podeUsarDRS = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        carro carroObj = other.transform.root.GetComponent<carro>();
        if (carroObj != null)
        {
            carroObj.maxTorque = 3500;
        }

        foreach (DRSsys ponto in zonasDRS)
        {
            ponto.Resetar();
        }
    }
}
