using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    public void RestartAfterDelay(float seconds) => StartCoroutine(Co(seconds));

    private System.Collections.IEnumerator Co(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds); 
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
