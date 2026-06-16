using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace DIStudy.Mogura
{
    public class MoguraHUDController : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_TimerText;
        [SerializeField] private TMP_Text m_ScoreText;
        [SerializeField] private TMP_Text m_MaxScoreText;
        [SerializeField] private Button m_RestartButton;

        private IScoreService m_Score;
        private ISaveService m_Save;
        private IGameService m_Game;
        private int m_MaxScore;
        private float m_TimeRemaining;
        private const float GameDuration = 30f;

        [Inject]
        public void Construct(IScoreService score, ISaveService save, IGameService game)
        {
            m_Score = score;
            m_Save = save;
            m_Game = game;
            m_Game.GameRestarted += OnGameRestarted;
        }

        private void Start()
        {
            m_MaxScore = m_Save.LoadScore();
            m_MaxScoreText.text = $"최고: {m_MaxScore}";
            m_Score.ScoreChanged += OnScoreChanged;
            if (m_RestartButton != null)
                m_RestartButton.onClick.AddListener(OnRestartClicked);
            OnScoreChanged(m_Score.CurrentScore);
            m_Game.StartGame();
        }

        private void Update()
        {
            if (!m_Game.IsRunning) return;

            m_TimeRemaining -= Time.deltaTime;
            if (m_TimeRemaining <= 0f)
            {
                m_TimeRemaining = 0f;
                m_TimerText.text = $"시간 : {0:00}";
                m_Game.EndGame();
                return;
            }
            m_TimerText.text = $"시간 : {Mathf.CeilToInt(m_TimeRemaining):00}";
        }

        private void OnGameRestarted()
        {
            m_TimeRemaining = GameDuration;
            m_Score.Restore(0);
        }

        private void OnScoreChanged(int score)
        {
            m_ScoreText.text = $"점수: {score}";

            if (score > m_MaxScore)
            {
                m_MaxScore = score;
                m_Save.SaveScore(m_MaxScore);
                m_MaxScoreText.text = $"최고: {m_MaxScore}";
            }
        }

        private void OnRestartClicked()
        {
            m_Game.StartGame();
        }
    }
}
