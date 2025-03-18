using Assets.LaserDefender.Script;
using System.IO;
using UnityEngine;

public class Health : MonoBehaviour
{
	[SerializeField] bool isPlayer;
    [SerializeField] int health = 50;
	[SerializeField] int scoreValue = 50;
	[SerializeField] ParticleSystem hitEffect;

	[SerializeField] bool applyCameraShake;
	CameraShake cameraShake;
	[SerializeField] GameObject audioPlayerObject;
	[SerializeField] GameObject powerUpEffect;
	AudioPlayer audioPlayer;
	ScoreKeeper scoreKeeper;
	LevelManager levelManager;
	string filePath;
	BestGame bestGame;
	PowerUpEffect powerUpEffectScript;

	private void Awake()
	{
		cameraShake = Camera.main.GetComponent<CameraShake>();
		audioPlayer = audioPlayerObject.GetComponent<AudioPlayer>();
		scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
		levelManager = FindFirstObjectByType<LevelManager>();
		if (powerUpEffect != null)
		{
			powerUpEffectScript = powerUpEffect.GetComponent<PowerUpEffect>();
			Debug.Log(powerUpEffectScript);
			Debug.Log("Da bat script PowerUpEffect");
		}
		filePath = Path.Combine(Application.dataPath, "LaserDefender/best_game.json");
		LoadBestScore();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		DamageDealer damageDealer = other.GetComponent<DamageDealer>();
		if(damageDealer != null)
		{
			//take damage
			TakeDamage(damageDealer.GetDamage());
			Destroy(other.gameObject);
			PlayWithEffect();
			ShakeCamera();
		}
		if(other.tag == "PowerPillBlue")
		{
			health = health + 10 > 50 ? 50 : health + 10;
			Destroy(other.gameObject);
		}
		if (other.tag == "PowerPillGreen")
		{
			health = health + 20 > 50 ? 50 : health + 20;
			Destroy(other.gameObject);
		}
		if (other.tag == "PowerPillRed")
		{
			health = health + 30 > 50 ? 50 : health + 30;
			Destroy(other.gameObject);
		}
	}

	void TakeDamage(int damage)
	{
		health -= damage;
		audioPlayer.PlayDefendClip();
		if (health <= 0)
		{
			if (!isPlayer)
			{
				scoreKeeper.ModefyScore(scoreValue);
			}
			else
			{
				Debug.Log("Game Over!");
				SaveBestScore(scoreKeeper.GetScore());
				levelManager.LoadGameOver();
			}
				audioPlayer.PlayLoseClip();
			Destroy(gameObject);
		}
	}

	public int getHealth()
	{
		return health;
	}

	void PlayWithEffect()
	{
		if(hitEffect != null)
		{
			ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
			Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
		}
	}

	void ShakeCamera()
	{
		if(cameraShake != null && applyCameraShake)
		{
			cameraShake.Play();
		}
	}

	void LoadBestScore()
	{
		if (File.Exists(filePath))
		{
			string json = File.ReadAllText(filePath);
			bestGame = JsonUtility.FromJson<BestGame>(json);
			Debug.Log("Loaded best score: " + bestGame.Score);
		}
	}

	 void SaveBestScore(int newScore)
	{
		Debug.Log("Saving new best score: " + newScore);
		// Nếu điểm mới lớn hơn bestScore cũ, cập nhật và ghi vào file
		if (newScore > bestGame.Score)
		{
			bestGame.Score = newScore;
			Debug.Log("Saved best score: " + bestGame.Score);
			string json = JsonUtility.ToJson(bestGame, true);
			File.WriteAllText(filePath, json);
		}
	}
}
