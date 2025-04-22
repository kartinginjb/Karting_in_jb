using UnityEngine;

public class turbo : MonoBehaviour
{

    Checkpoint drs;

    private void Awake()
    {
        //Carregar checkpoints
        drs = FindObjectsOfType<ponto>();
    }

    private void OnTriggerEnter(Collider other)
    {
        carro x = other.transform.root.GetComponent<carro>();   //procura no objeto principal pelo script carro

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            carro.maxTorque = 2000;
        }
        else if (Input.GetKeyUp(KeyCode.X))
        {
            carro.maxTorque = 1500;
        }

    }
}
