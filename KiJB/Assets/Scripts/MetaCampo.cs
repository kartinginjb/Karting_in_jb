using UnityEngine;
using Dan.Demo;

public class MetaCampo : MonoBehaviour
{
    public Cronometro cronometro;
    public LeaderboardShowcaseCampo leaderboardShowcase;
    private Checkpoint[] checkpoints;

    private void Awake()
    {
        checkpoints = FindObjectsByType<Checkpoint>(FindObjectsSortMode.None);
    }

    private void OnTriggerEnter(Collider other)
    {
        carro x = other.transform.root.GetComponent<carro>();

        foreach (Checkpoint ch in checkpoints)
        {
            if (!ch.PassouCarro())
            {
                ResetCheckpoints();
                return;
            }
        }

        cronometro.PararCronometro();

        float tempoFinal = cronometro.ObterTempoFinal();
        Debug.Log("Tempo final: " + tempoFinal.ToString("F2"));

        x.SomarVolta();

        leaderboardShowcase.AddPlayerScore(); // Atualiza o tempo no script da leaderboard

        cronometro.ComecarCronometro();
        ResetCheckpoints();
    }

    private void ResetCheckpoints()
    {
        foreach (Checkpoint cha in checkpoints)
        {
            cha.carros = null;
        }
    }
}
