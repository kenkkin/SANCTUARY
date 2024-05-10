using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] float loadTime;
    [SerializeField] AudioSource levelMusic; 
    [SerializeField] AudioSource stinger; 

    [Header("Movement")]
    [SerializeField] NavMeshAgent player;

    [Space]

    [Header("UI")]
    [SerializeField] GameObject tut;

    void Start()
    {
        player = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (stinger != null)
            {
                stinger.Play();
            }

            FadeOutLevelMusic();

            // tut.SetActive(false);

            Walk();
            Invoke("LoadLevel", loadTime);
        }
    }

    void Walk()
    {
        Debug.Log("Walk");

        Vector3 targetPosition = new Vector3(0f, -5.5f, 1.5f);
        player.SetDestination(targetPosition);
    }

    void LoadLevel()
    {
        SceneManager.LoadScene("LVL 1");
    }

    void FadeOutLevelMusic()
    {
        StartCoroutine(FadeOut(levelMusic, 1f)); // adjust float to desired fade duration
    }

    IEnumerator FadeOut(AudioSource audioSource, float fadeDuration)
    {
        
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}
