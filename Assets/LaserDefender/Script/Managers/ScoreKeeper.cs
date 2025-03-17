using Unity.VisualScripting;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int score;

	public static ScoreKeeper instance;

	void Awake() { 
		ManageSingleton();
	}

	void ManageSingleton()
	{
		if (instance != null)
		{
			gameObject.SetActive(false);
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

    public int GetScore()
	{
		return score;
	}

	public void ModefyScore(int value)
	{
		score += value;
		score = Mathf.Clamp(score, 0, int.MaxValue);
	}

	public void ResetScore()
	{
		score = 0;
	}
}
