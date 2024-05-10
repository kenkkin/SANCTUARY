using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScore;

    void OnEnable() 
    {
        if (PlayerPrefs.HasKey(TimerController.SCORE_KEY))
        {
            finalScore.text = PlayerPrefs.GetString(TimerController.SCORE_KEY);
        }
    }
}
