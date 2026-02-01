using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject tutorialScreen;
    [SerializeField] private GameObject creditsScreen;
    public GameObject MenuButtons;

    void Start()
    {
        
    }

    public void startGame()
    {
        SceneManager.LoadScene("Main");
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
        MenuButtons.SetActive(false);
    }

    public void hideTutorial()
    {
        tutorialScreen.SetActive(false);
        MenuButtons.SetActive(true);
    }

    public void showCredits()
    {
        creditsScreen.SetActive(true); 
        MenuButtons.SetActive(false);
    }

    public void hideCredits()
    {
        creditsScreen.SetActive(false);
        MenuButtons.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
