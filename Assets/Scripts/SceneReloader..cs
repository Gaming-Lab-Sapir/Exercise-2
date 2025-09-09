using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    [SerializeField] float restartDelaySeconds = 2f;
    bool running;

    void OnEnable()
    {
        GameEvents.GameWon += HandleRestart;
        GameEvents.GameLost += HandleRestart;
    }

    void OnDisable()
    {
        GameEvents.GameWon -= HandleRestart;
        GameEvents.GameLost -= HandleRestart;
    }

    void HandleRestart()
    {
        if (running) return;      
        running = true;
        StartCoroutine(RestartGame());
    }

    System.Collections.IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(restartDelaySeconds);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
