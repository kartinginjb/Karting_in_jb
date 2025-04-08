using UnityEngine;

public class Meta : MonoBehaviour
{
    Checkpoint[] checkpoints;
    private void Awake()
    {
        //Carregar checkpoints
        checkpoints = FindObjectsOfType<Checkpoint>();
    }

    private void OnTriggerEnter(Collider other)         
    {
        carro x = other.transform.root.GetComponent<carro>();   //procura no objeto principal pelo script carro

        foreach (Checkpoint ch in checkpoints)
        {
            if (!ch.PassouCarro())
            {
                ResetCheckpoints();
                
                return;
            }
        }

        x.SomarVolta();
        ResetCheckpoints();
    }

    void ResetCheckpoints()
    {
        foreach (Checkpoint cha in checkpoints)
        {
            cha.carros = null;
        }
    }

}
