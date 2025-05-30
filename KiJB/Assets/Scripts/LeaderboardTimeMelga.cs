using UnityEngine;
using TMPro;
using Dan.Main;

namespace LeaderboardCreatorDemo.Melga
{
    public class LeaderboardManagerMelga : MonoBehaviour
    {
        [SerializeField] private TMP_Text[] _entryTextObjects;
        [SerializeField] private TMP_InputField _usernameInputField;
        [SerializeField] private Cronometro _cronometro;

        private void Start()
        {
            LoadEntries();
        }

        private void LoadEntries()
        {
            Leaderboards.KiJBMelga.GetEntries(entries =>
            {
                ClearEntries();

                int length = Mathf.Min(_entryTextObjects.Length, entries.Length);
                for (int i = 0; i < length; i++)
                {
                    string tempoFormatado = FormatarTempo(entries[i].Score);
                    _entryTextObjects[i].text = $"{entries[i].Rank}. {entries[i].Username} - {tempoFormatado}";
                }
            });
        }

        private void ClearEntries()
        {
            foreach (var t in _entryTextObjects)
                t.text = "";
        }

        public void UploadEntry()
        {
            int tempoMs = Mathf.FloorToInt(_cronometro.MelhorTempo * 1000f);

            if (tempoMs <= 0 || float.IsInfinity(_cronometro.MelhorTempo))
                return;

            Leaderboards.KiJBMelga.UploadNewEntry(_usernameInputField.text, tempoMs, success =>
            {
                if (success) LoadEntries();
            });
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
