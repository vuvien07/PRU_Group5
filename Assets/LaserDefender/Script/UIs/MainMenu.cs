using Assets.LaserDefender.Script;
using System.IO;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
	string filePath;
	BestGame bestGame;
	TextMeshProUGUI scoreText;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        LoadBestScore();
		scoreText = GetComponent<TextMeshProUGUI>();
    }

	void LoadBestScore()
	{
		if (File.Exists(filePath))
		{
			string json = File.ReadAllText(filePath);
			bestGame = JsonUtility.FromJson<BestGame>(json);
		}
	}
}
