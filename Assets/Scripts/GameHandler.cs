using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
	public TextMeshProUGUI CollectibleText;
	public int collectibles;
	public int win;
	public TextMeshProUGUI WinText;

	void Start()
	{
		collectibles = 0;
		WinText.text = "";	
	}
	// Update is called once per frame
	void Update()
	{
		CollectibleText.text = "PRAYERS: " + collectibles;

		{

			if (collectibles >= win)
			{
				WinText.text = "ALL PRAYERS COLLECTED";
				Invoke("KillMessage", 2.0f);
			}
		}
	}

	void KillMessage()
	{
		WinText.enabled = false;
	}
}