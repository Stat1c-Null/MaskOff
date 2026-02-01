using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject tutorialScreen;
    void Start()
    {
        
    }

    public void startGame()
    {
        SceneManager.LoadScene("IntroScene");
    }
    public void nextLevel()
    {
        //SceneManager.LoadScene("Level2");
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void showTutorial()
    {
        tutorialScreen.SetActive(true);
    }

    public void hideTutorial()
    {
        tutorialScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
