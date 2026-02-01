using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject pauseMenu;

    private GameObject player;

    InputAction pause;
    bool isPaused = false;

    void Awake()
    {
        Time.timeScale = 1f;
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player object not found in the scene. Please ensure there is a GameObject with the tag 'Player'.");
        }
    }

    void Start()
    {
        pause = InputSystem.actions.FindAction("Pause");
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

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void UnPause()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (player != null && player.GetComponent<PlayerController>().GetHealth() <= 0)
        {
            Invoke("GameOver", 1.0f);
        }

        if(pause.WasPressedThisFrame() && isPaused == false)
        {
            Pause();
        }
        else if (pause.WasPressedThisFrame() && isPaused == true)
        {
            UnPause();
        }
    }
}
