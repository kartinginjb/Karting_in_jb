using System.Collections;
using Dan.Main;
using Dan.Models;
using TMPro;
using UnityEngine;

namespace Dan.Demo
{
    public class LeaderboardShowcaseBasica : MonoBehaviour
    {
        [Header("Gameplay:")]
        [SerializeField] private TextMeshProUGUI _playerScoreText;
        [SerializeField] private Cronometro _cronometro;

        [Header("Leaderboard Essentials:")]
        [SerializeField] private TMP_InputField _playerUsernameInput;
        [SerializeField] private Transform _entryDisplayParent;
        [SerializeField] private EntryDisplay _entryDisplayPrefab;
        [SerializeField] private CanvasGroup _leaderboardLoadingPanel;

        [Header("Search Query Essentials:")]
        [SerializeField] private TMP_Dropdown _timePeriodDropdown;
        [SerializeField] private TMP_InputField _pageInput, _entriesToTakeInput;
        [SerializeField] private int _defaultPageNumber = 1, _defaultEntriesToTake = 100;

        [Header("Personal Entry:")]
        [SerializeField] private RectTransform _personalEntryPanel;
        [SerializeField] private TextMeshProUGUI _personalEntryText;

        [Header("UI Feedback:")]
        [SerializeField] private GameObject _noTimeWarningPanel; // ⚠️ Novo painel para feedback visual

        private int _playerScore;
        private Coroutine _personalEntryMoveCoroutine;

        public void AddPlayerScore()
        {
            float tempo = _cronometro.MelhorTempo;
            _playerScore = Mathf.RoundToInt(tempo * 1000);
            _playerScoreText.text = $"Melhor Tempo: {FormatScoreAsTime(_playerScore)}";
        }

        public void Load()
        {
            var timePeriod =
                _timePeriodDropdown.value == 1 ? Dan.Enums.TimePeriodType.Today :
                _timePeriodDropdown.value == 2 ? Dan.Enums.TimePeriodType.ThisWeek :
                _timePeriodDropdown.value == 3 ? Dan.Enums.TimePeriodType.ThisMonth :
                _timePeriodDropdown.value == 4 ? Dan.Enums.TimePeriodType.ThisYear : Dan.Enums.TimePeriodType.AllTime;

            var pageNumber = int.TryParse(_pageInput.text, out var pageValue) ? pageValue : _defaultPageNumber;
            pageNumber = Mathf.Max(1, pageNumber);
            _pageInput.text = pageNumber.ToString();

            var take = int.TryParse(_entriesToTakeInput.text, out var takeValue) ? takeValue : _defaultEntriesToTake;
            take = Mathf.Clamp(take, 1, 100);
            _entriesToTakeInput.text = take.ToString();

            var searchQuery = new LeaderboardSearchQuery
            {
                Skip = (pageNumber - 1) * take,
                Take = take,
                TimePeriod = timePeriod
            };

            _pageInput.image.color = Color.white;
            _entriesToTakeInput.image.color = Color.white;

            Leaderboards.KiJBBasica.GetEntries(searchQuery, OnLeaderboardLoaded, ErrorCallback);
            ToggleLoadingPanel(true);
        }

        public void ChangePageBy(int amount)
        {
            var pageNumber = int.TryParse(_pageInput.text, out var pageValue) ? pageValue : _defaultPageNumber;
            pageNumber += amount;
            if (pageNumber < 1) return;
            _pageInput.text = pageNumber.ToString();
        }

        private void OnLeaderboardLoaded(Entry[] entries)
        {
            foreach (Transform t in _entryDisplayParent)
                Destroy(t.gameObject);

            foreach (var entry in entries)
                CreateEntryDisplay(entry);

            ToggleLoadingPanel(false);
        }

        private void CreateEntryDisplay(Entry entry)
        {
            var entryDisplay = Instantiate(_entryDisplayPrefab.gameObject, _entryDisplayParent);
            entryDisplay.GetComponent<EntryDisplay>().SetEntry(entry);
        }

        private string FormatScoreAsTime(float tempo)
        {
            float tempoSegundos = tempo / 1000f;
            int minutos = Mathf.FloorToInt(tempoSegundos / 60f);
            int segundos = Mathf.FloorToInt(tempoSegundos % 60f);
            int milissegundos = Mathf.FloorToInt((tempoSegundos * 1000f) % 1000f);
            return $"{minutos:00}:{segundos:00}:{milissegundos:000}";
        }

        private void ToggleLoadingPanel(bool isOn)
        {
            _leaderboardLoadingPanel.alpha = isOn ? 1f : 0f;
            _leaderboardLoadingPanel.interactable = isOn;
            _leaderboardLoadingPanel.blocksRaycasts = isOn;
        }

        public void MovePersonalEntryMenu(float xPos)
        {
            if (_personalEntryMoveCoroutine != null)
                StopCoroutine(_personalEntryMoveCoroutine);
            _personalEntryMoveCoroutine = StartCoroutine(MoveMenuCoroutine(_personalEntryPanel,
                new Vector2(xPos, _personalEntryPanel.anchoredPosition.y)));
        }

        private IEnumerator MoveMenuCoroutine(RectTransform rectTransform, Vector2 anchoredPosition)
        {
            const float duration = 0.25f;
            var time = 0f;
            var startPosition = rectTransform.anchoredPosition;
            while (time < duration)
            {
                time += Time.deltaTime;
                rectTransform.anchoredPosition = Vector2.Lerp(startPosition, anchoredPosition, time / duration);
                yield return null;
            }

            rectTransform.anchoredPosition = anchoredPosition;
            _personalEntryMoveCoroutine = null;
        }

        private IEnumerator LoadingTextCoroutine(TMP_Text text)
        {
            var loadingText = "Loading";
            for (int i = 0; i < 3; i++)
            {
                loadingText += ".";
                text.text = loadingText;
                yield return new WaitForSeconds(0.25f);
            }

            StartCoroutine(LoadingTextCoroutine(text));
        }

        private void InitializeComponents()
        {
            StartCoroutine(LoadingTextCoroutine(_leaderboardLoadingPanel.GetComponentInChildren<TextMeshProUGUI>()));

            _pageInput.onValueChanged.AddListener(_ => _pageInput.image.color = Color.yellow);
            _entriesToTakeInput.onValueChanged.AddListener(_ => _entriesToTakeInput.image.color = Color.yellow);

            _pageInput.placeholder.GetComponent<TextMeshProUGUI>().text = _defaultPageNumber.ToString();
            _entriesToTakeInput.placeholder.GetComponent<TextMeshProUGUI>().text = _defaultEntriesToTake.ToString();
        }

        private void Start()
        {
            InitializeComponents();
            Load();
        }

        public void Submit()
        {
            // ✅ Validação de tempo antes do envio
            if (_cronometro.MelhorTempo <= 0f || float.IsNaN(_cronometro.MelhorTempo) || _playerScore <= 0)
            {
                Debug.LogWarning("Tentativa de envio sem tempo válido.");
                if (_noTimeWarningPanel != null)
                {
                    _noTimeWarningPanel.SetActive(true);
                }
                return;
            }

            Leaderboards.KiJBBasica.UploadNewEntry(_playerUsernameInput.text, _playerScore, Callback, ErrorCallback);
        }

        public void DeleteEntry()
        {
            // ✅ Permite apagar entrada mesmo que não haja tempo
            Leaderboards.KiJBBasica.DeleteEntry(Callback, ErrorCallback);
        }

        public void ResetPlayer()
        {
            LeaderboardCreator.ResetPlayer();
        }

        public void GetPersonalEntry()
        {
            Leaderboards.KiJBBasica.GetPersonalEntry(OnPersonalEntryLoaded, ErrorCallback);
        }

        private void OnPersonalEntryLoaded(Entry entry)
        {
            _personalEntryText.text = $"{entry.RankSuffix()}. {entry.Username} : {FormatScoreAsTime(entry.Score)}";
            MovePersonalEntryMenu(0f);
        }

        private void Callback(bool success)
        {
            if (success)
                Load();
        }

        private void ErrorCallback(string error)
        {
            Debug.LogError(error);
        }
    }
}
