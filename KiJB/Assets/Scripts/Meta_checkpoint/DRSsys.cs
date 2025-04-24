using UnityEngine;

public class DRSsys : MonoBehaviour
{
    public GameObject carroAutorizado = null;

    public turbo drs;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.GetComponent<carro>())
        {
            carroAutorizado = other.transform.root.gameObject;

            drs.podeUsarDRS = true;
        }
    }

    public bool PassouCarro(GameObject carro)
    {
        return carroAutorizado == carro;
    }

    public void Resetar()
    {
        carroAutorizado = null;
    }
}
