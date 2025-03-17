using Assets.LaserDefender.Script;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
	[SerializeField] float sceneLoadDelay = 1.0f;
	ScoreKeeper scoreKeeper;
	string filePath;
	BestGame bestGame;

	void Awake()
	{
		scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
	Debug.Log(scoreKeeper);
		filePath = Path.Combine(Application.dataPath, "LaserDefender/best_game.json");
	}
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
	public void LoadMainScene()
	{

		if (scoreKeeper != null)
		{
			scoreKeeper.ResetScore();
		}
		SceneManager.LoadScene("MainScene");
	}

	public void LoadGameOver()
	{
		StartCoroutine(WaitAndLoad("GameOverScene", sceneLoadDelay));
	}

	public void QuitGame()
    {
		Application.Quit();
    }

	IEnumerator WaitAndLoad(string sceneName, float delay)
	{
		yield return new WaitForSeconds(delay);
		SceneManager.LoadScene(sceneName);
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
