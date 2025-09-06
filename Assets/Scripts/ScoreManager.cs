using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;        
    [SerializeField] GameObject winText;               
    [SerializeField] float restartDelaySeconds = 2f;   

    int currentScore = 0;
    int targetScore;

    void Start()
    {
        Time.timeScale = 1f;

        targetScore = Random.Range(1, 6);

        if (scoreText != null)
            scoreText.text = $"Score: {currentScore}/{targetScore}";

        if (winText != null)
            winText.SetActive(false);
    }

    public void AddPoint()
    {
        currentScore++;

        if (scoreText != null)
            scoreText.text = $"Score: {currentScore}/{targetScore}";

        if (currentScore >= targetScore)
        {
            if (winText != null)
                winText.SetActive(true);

            Time.timeScale = 0f;

            var reloader = FindFirstObjectByType<SceneReloader>();
            if (reloader != null)
                reloader.RestartAfterDelay(restartDelaySeconds);
        }
    }
}
