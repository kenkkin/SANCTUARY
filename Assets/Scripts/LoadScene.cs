using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private string currentScene;
    
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    } 

    private void OnTriggerEnter(Collider ChangeScene)
    {
        if(ChangeScene.gameObject.CompareTag("Player"))
        {
            Invoke("LoadNextScene", 0.5f);
            Debug.Log("Trigger Entered");
        } 
    }

    void LoadNextScene()
    {
        if(currentScene == "LVL 1")
        {
            SceneManager.LoadScene("LVL 2");
        }

        if(currentScene == "LVL 2")
        {
            SceneManager.LoadScene("LVL 3");
        }

        if(currentScene == "LVL 3")
        {
            SceneManager.LoadScene("WIN");
        }
    }
}
