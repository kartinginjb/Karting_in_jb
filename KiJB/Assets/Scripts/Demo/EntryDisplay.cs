using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Dan.Models;

namespace Dan.Demo
{
    public class EntryDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _rankText, _usernameText, _timeText, _dateText;

        public void SetEntry(Entry entry)
        {
            // Rank
            _rankText.text = entry.RankSuffix();

            // Nome do jogador
            _usernameText.text = entry.Username;

            // Tempo (conversão de milissegundos para formato mm:ss:SSS)
            float tempoSegundos = entry.Score / 1000f;
            int minutos = Mathf.FloorToInt(tempoSegundos / 60f);
            int segundos = Mathf.FloorToInt(tempoSegundos % 60f);
            int milissegundos = Mathf.FloorToInt((tempoSegundos * 1000f) % 1000f);
            _timeText.text = $"{minutos:00}:{segundos:00}:{milissegundos:000}";

            // Data (UTC)
            var dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(entry.Date);
            _dateText.text = $"{dateTime:dd/MM/yyyy}";

            // Destaque se for a entrada pessoal
            GetComponent<Image>().color = entry.IsMine() ? Color.yellow : Color.white;
        }
    }
}
