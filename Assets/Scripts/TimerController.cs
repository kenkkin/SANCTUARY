using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TimerController : MonoBehaviour
{
    static private float _timer;
    [SerializeField] TextMeshProUGUI Score;
    public const string SCORE_KEY = "highscore";

    private string currentScene;

    public float Timer
    {
        get => _timer;

        set
        {
            _timer = value;

            int minutes = Mathf.FloorToInt(Timer / 60.0f);
            int seconds = Mathf.FloorToInt(Timer % 60.0f);

            Score.text = $"TIME {minutes:00}:{seconds:00}";
        }
    }

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        Timer += Time.deltaTime / 2;
    }

    private void FinalScore()
    {
        if(currentScene == "LVL 2")
        {
            Destroy(Score);
        }
    }

    private void OnDestroy() 
    {
        saveScore();
    }

    void saveScore()
    {
        PlayerPrefs.SetString(SCORE_KEY, Score.text);
        PlayerPrefs.Save();
        Debug.Log("Score Saved:" + Score.text);
    }
}
