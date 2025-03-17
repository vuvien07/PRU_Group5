using Assets.LaserDefender.Script;
using System.IO;
using TMPro;
using UnityEngine;

public class UIGameMenu : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI scoreText;
	string filePath;
	BestGame bestGame;
	private void Awake()
	{
		filePath = Path.Combine(Application.dataPath, "LaserDefender/best_game.json");
		LoadBestScore();
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		scoreText.text = "Best Score:\n" + bestGame.Score;
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
