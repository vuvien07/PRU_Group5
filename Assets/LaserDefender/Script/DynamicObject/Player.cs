using Assets.LaserDefender.Script.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	[SerializeField] float moveSpeed = 5f;
	[SerializeField] float paddingLeft;
	[SerializeField] float paddingRight;
	[SerializeField] float paddingTop;
	[SerializeField] float paddingBottom;
	Vector2 rawInput;
	Shooter shooter;
	[SerializeField] GameObject powerUpUI; // GameObject chứa UI

	Vector2 minBounds;
	Vector2 maxBounds;
	float initialFiringRate;
	float projectileSpeed;
	Slider slider;
	[SerializeField] private Image iconImage;
	[SerializeField] private List<PowerUpData> powerUpDataList;
	private Dictionary<string, Sprite> powerUpIcons;

	void Awake()
	{
		shooter = GetComponent<Shooter>();
		initialFiringRate = shooter.GetFiringRate();
		projectileSpeed = shooter.GetProjectileSpeed();
		powerUpUI.SetActive(false);
		slider = powerUpUI.GetComponentInChildren<Slider>();
		powerUpIcons = new Dictionary<string, Sprite>();

		foreach (var data in powerUpDataList)
		{
			powerUpIcons[data.tag] = data.icon;
			Debug.Log("Tag: " + data.tag + ", Icon: " + data.icon);
		}

		powerUpUI.SetActive(false);
	}
	void Start()
	{
		InitBounds();
	}
	void InitBounds()
	{
		Camera mainCamera = Camera.main;
		minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
		maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "PowerBoldBronze")
		{
			Destroy(collision.gameObject);
			ApplyPowerUp(0.5f, powerUpIcons["PowerBoldBronze"]);
		}
		if (collision.tag == "PowerBoldSilver")
		{
			Destroy(collision.gameObject);
			ApplyPowerUp(0.75f, powerUpIcons["PowerBoldSilver"]);
		}
		if(collision.tag == "PowerBoldGold")
		{
			Destroy(collision.gameObject);
			ApplyPowerUp(1f, powerUpIcons["PowerBoldGold"]);
		}
	}

	void Update()
	{
		Move();
	}
	void OnMove(InputValue value)
	{
		rawInput = value.Get<Vector2>();
	}

	void OnFire(InputValue value)
	{
		if(shooter != null)
		{
			shooter.isFiring = value.isPressed;
		}
	}

	void Move()
	{
		Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
		Vector2 newPos = new Vector2();
		newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
		newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
		transform.position = newPos;
	}

	private void ApplyPowerUp(float multiplier, Sprite icon = null)
	{
		iconImage.sprite = icon;
		powerUpUI.SetActive(true);
		shooter.SetBaseFiringRate(initialFiringRate * multiplier);
		shooter.SetProjectileSpeed(projectileSpeed * multiplier);
		StartCoroutine(ResetPowerUpAfterDelay(5f));
	}

	private IEnumerator ResetPowerUpAfterDelay(float delay, Sprite icon = null)
	{
		slider.maxValue = delay;
		slider.value = delay;

		float elapsedTime = 0f;
		while (elapsedTime < delay)
		{
			elapsedTime += Time.deltaTime;
			slider.value = delay - elapsedTime;  // Giảm giá trị slider
			yield return null;  // Đợi frame tiếp theo
		}

		shooter.SetBaseFiringRate(initialFiringRate);
		shooter.SetProjectileSpeed(projectileSpeed);
		powerUpUI.SetActive(false);  // Ẩn UI sau khi hết thời gian
	}

}
