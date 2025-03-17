using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpEffect : MonoBehaviour
{
	[SerializeField] private Image image; // Kéo Image UI vào đây
	[SerializeField] private float blinkSpeed = 0.5f; // Tốc độ nhấp nháy
	[SerializeField] private int blinkTimes = 3; // Số lần nhấp nháy
	[SerializeField] private float duration = 1f;
	[SerializeField] private float alpha;
	[SerializeField] private bool isLooping;
	Coroutine coroutine;

	float timer = 0f;
	private void Start()
	{
		StartCoroutine(BlinkEffect());
	}
	public void StartEffect()
	{
		if (coroutine != null)
			StopCoroutine(coroutine);
		coroutine = StartCoroutine(BlinkEffect());
	}
	IEnumerator BlinkEffect()
	{
		while (timer < duration)
		{
			for (int i = 0; i < blinkTimes; i++)
			{
				yield return StartCoroutine(FadeAlpha(0.02f, 0f, blinkSpeed / 2)); // Giảm alpha
				yield return StartCoroutine(FadeAlpha(0f, 0.02f, blinkSpeed / 2)); // Tăng alpha
			}
			timer += blinkSpeed * blinkTimes;
			yield return null;
		}
	}

	IEnumerator FadeAlpha(float startAlpha, float endAlpha, float duration)
	{
		float elapsedTime = 0f;
		while (elapsedTime < duration)
		{
			float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
			SetImageAlpha(newAlpha);
			elapsedTime += blinkSpeed * blinkTimes;
			yield return null;
		}
		SetImageAlpha(endAlpha); // Đảm bảo alpha đạt đúng giá trị cuối cùng
	}

	void SetImageAlpha(float alpha)
	{
		if (image != null)
		{
			Color color = image.color;
			color.a = alpha;
			image.color = color;
		}
	}
}
