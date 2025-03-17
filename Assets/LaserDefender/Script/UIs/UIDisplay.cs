using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
	[Header("Health")]
	[SerializeField] Slider healthSlider;
	[SerializeField] Health playerHealth;

	[Header("Score")]
	[SerializeField] TextMeshProUGUI scoreText;
	ScoreKeeper scoreKeeper;

	private void Awake()
	{
		scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		healthSlider.maxValue = playerHealth.getHealth();
		scoreKeeper.ResetScore();
	}

	// Update is called once per frame
	void Update()
	{
		healthSlider.value = playerHealth.getHealth();
		scoreText.text = scoreKeeper.GetScore().ToString("Score:0");
	}
}
