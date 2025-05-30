using UnityEngine;
using TMPro;
using Dan.Main;

namespace LeaderboardCreatorDemo
{
    public class LeaderboardManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text[] _entryTextObjects;
        [SerializeField] private TMP_InputField _usernameInputField;

        [SerializeField] private Cronometro _cronometro; // Referência ao Cronômetro

        private void Start()
        {
            LoadEntries();
        }

        private void LoadEntries()
        {
            Leaderboards.KiJBBasic.GetEntries(entries =>
            {
                foreach (var t in _entryTextObjects)
                    t.text = "";

                int length = Mathf.Min(_entryTextObjects.Length, entries.Length);
                for (int i = 0; i < length; i++)
                {
                    int tempoMs = entries[i].Score;
                    string tempoFormatado = FormatarTempo(tempoMs);
                    _entryTextObjects[i].text = $"{entries[i].Rank}. {entries[i].Username} - {tempoFormatado}";
                }
            });
        }

        public void UploadEntry()
        {
            // Obtém o tempo do cronômetro em milissegundos
            int tempoEmMilissegundos = Mathf.FloorToInt(_cronometro.MelhorTempo * 1000f);

            // Garante que o tempo seja válido
            if (tempoEmMilissegundos <= 0 || float.IsInfinity(_cronometro.MelhorTempo))
                return;

            // Envia o tempo para o leaderboard
            Leaderboards.KiJBBasic.UploadNewEntry(
                _usernameInputField.text,
                tempoEmMilissegundos,
                isSuccessful =>
                {
                    if (isSuccessful)
                        LoadEntries();
                }
            );
        }

        private string FormatarTempo(int tempoMs)
        {
            int minutos = tempoMs / 60000;
            int segundos = (tempoMs % 60000) / 1000;
            int milissegundos = tempoMs % 1000;
            return $"{minutos:00}m{segundos:00}s{milissegundos:000}ms";
        }
    }
}
