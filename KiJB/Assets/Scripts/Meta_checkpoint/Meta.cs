using UnityEngine;

public class Meta : MonoBehaviour
{
    public Cronometro cronometro;
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

        cronometro.PararCronometro();
        Debug.Log("Tempo final: " + cronometro.ObterTempoFinal().ToString("F2"));
        cronometro.ComecarCronometro();
    }

    void ResetCheckpoints()
    {
        foreach (Checkpoint cha in checkpoints)
        {
            cha.carros = null;
        }
    }

}
