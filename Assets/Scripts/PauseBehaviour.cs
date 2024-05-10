using UnityEngine;
using UnityEngine.SceneManagement;

// make sure scene exists in build settings
public class PauseBehaviour : MonoBehaviour
{
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] GameObject _pauseButton;
    [SerializeField] GameObject _restartButton;
    [SerializeField] GameObject player;
    private string currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    } 

    public void Pause()
    {
        _pauseMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Menu()
    {
        SceneManager.LoadScene("TITLE");
    }

    public void PauseButton()
    {
        Debug.Log("Time Scale: " + Time.timeScale);
        
        Time.timeScale = 0.0f;
        _pauseMenu.SetActive(true);
        _pauseButton.SetActive(false);

        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        if(playerMovement != null)
        {
            playerMovement.enabled = false;
        }
    }

    public void ResumeButton()
    {
        Debug.Log("Time Scale: " + Time.timeScale);
        Time.timeScale = 1.0f;
        _pauseMenu.SetActive(false);
        _pauseButton.SetActive(true);

        player.GetComponent<PlayerMovement>().enabled = true;
    }

    public void RestartButton()
    {
        Debug.Log("Time Scale: " + Time.timeScale);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(currentScene);
    }
}
