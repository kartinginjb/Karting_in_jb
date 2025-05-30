using UnityEngine;
using Dan.Demo;
public class Meta : MonoBehaviour
{
    public Cronometro cronometro;
    public LeaderboardShowcaseMonza leaderboardShowcase;
    private Checkpoint[] checkpoints;

    private void Awake()
    {
        // Carregar checkpoints
        checkpoints = FindObjectsByType<Checkpoint>(FindObjectsSortMode.None);
    }

    private void OnTriggerEnter(Collider other)
    {
        carro x = other.transform.root.GetComponent<carro>();   // procura o script carro

        // Verifica se passou por todos os checkpoints
        foreach (Checkpoint ch in checkpoints)
        {
            if (!ch.PassouCarro())
            {
                ResetCheckpoints();
                return; // NÃO conta a volta se falhou checkpoints
            }
        }

        // ✅ Volta válida
        cronometro.PararCronometro(); // agora sim, pára o cronómetro
        UnityEngine.Debug.Log("Tempo final: " + cronometro.ObterTempoFinal().ToString("F2"));

        x.SomarVolta();
        leaderboardShowcase.AddPlayerScore(); // ✅ Atualiza o score mostrado no leaderboard
        cronometro.ComecarCronometro(); // Começa nova volta

        ResetCheckpoints(); // Limpa checkpoints
    }

    void ResetCheckpoints()
    {
        foreach (Checkpoint cha in checkpoints)
        {
            cha.carros = null;
        }
    }
}
