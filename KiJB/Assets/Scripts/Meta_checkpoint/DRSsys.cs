using UnityEngine;

public class DRSsys : MonoBehaviour
{
    public GameObject carros = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.GetComponent<carro>())
        {
            carros = other.transform.root.gameObject;  //procura no objeto principal pelo script carro
        }
    }

    public bool PassouCarro()   //confirma se o carro passou na meta
    {
        if (carros != null)
        {
            return true;
        }
        return false;
    }
}
