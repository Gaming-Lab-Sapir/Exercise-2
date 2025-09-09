using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject winText;
    [SerializeField] float restartDelaySeconds = 2f;  

    int currentScore = 0;
    int targetScore;

    void OnEnable() => GameEvents.EnemyKilledByBullet += OnEnemyKilledByBullet;
    void OnDisable() => GameEvents.EnemyKilledByBullet -= OnEnemyKilledByBullet;

    void Start()
    {
        Time.timeScale = 1f;

        targetScore = Random.Range(10, 20);

        if (scoreText != null)
            scoreText.text = $"Score: {currentScore}/{targetScore}";

        if (winText != null)
            winText.SetActive(false);
    }

    public void AddPoint() => OnEnemyKilledByBullet();

    private void OnEnemyKilledByBullet()
    {
        currentScore++;

        if (scoreText != null)
            scoreText.text = $"Score: {currentScore}/{targetScore}";

        if (currentScore >= targetScore)
        {
            if (winText != null)
                winText.SetActive(true);

            Time.timeScale = 0f;

            GameEvents.RaiseGameWon();
        }
    }
}
