using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;

    void Awake()
    {
        Time.timeScale = 1f;
    }

    public void startGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOver.SetActive(true);
    }
}
