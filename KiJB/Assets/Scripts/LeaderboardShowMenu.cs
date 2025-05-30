using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject leaderboardPanel; // arrastar o painel Leaderboard aqui no Inspector

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleLeaderboard();
        }
    }

    public void ToggleLeaderboard()
    {
        bool isActive = leaderboardPanel.activeSelf;
        leaderboardPanel.SetActive(!isActive);

        // (opcional) Pausar/despausar o jogo
        Time.timeScale = isActive ? 1f : 0f;
    }
}
