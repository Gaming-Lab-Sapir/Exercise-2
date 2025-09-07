using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    [SerializeField] float restartDelaySeconds = 2f;
    bool _running;

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
        if (_running) return;      
        _running = true;
        StartCoroutine(Co());
    }

    System.Collections.IEnumerator Co()
    {
        yield return new WaitForSecondsRealtime(restartDelaySeconds);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
