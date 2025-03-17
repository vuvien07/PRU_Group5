using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Shooter : MonoBehaviour
{
	[Header("General")]
	[SerializeField] GameObject projectilePrefab;
	[SerializeField] float projectileSpeed = 10f;
	[SerializeField] float projectileLifetime = 5f;
	[HideInInspector] public bool isFiring;
	Coroutine firingCoroutine;
	[SerializeField] float baseFiringRate = 0.2f;
	[SerializeField] float firingRateVariance = 0f;
	[SerializeField] float minimumFiringRate = 0.1f;
	[SerializeField] bool isEnemy;


	[SerializeField] GameObject audioPlayerObject;
	AudioPlayer audioPlayer;

	private void Awake()
	{
		audioPlayer = audioPlayerObject.GetComponent<AudioPlayer>();
	}
	private void Start()
	{
		if (isEnemy)
		{
			isFiring = true;
		}
	}
	private void Update()
	{
		Fire();
	}

	public float GetFiringRate()
	{
		return baseFiringRate;
	}

	public float GetProjectileSpeed()
	{
		return projectileSpeed;
	}

	public void SetBaseFiringRate(float newBaseFiringRate)
	{
		baseFiringRate = newBaseFiringRate;
	}

	public void SetProjectileSpeed(float newProjectileSpeed)
	{
		projectileSpeed = newProjectileSpeed;
	}

	void Fire()
	{
		if (isFiring && firingCoroutine == null)
		{
			firingCoroutine = StartCoroutine(FileContiniously());
		}
		else if (!isFiring && firingCoroutine != null)
		{
			StopCoroutine(firingCoroutine);
			firingCoroutine = null;
		}
	}

	IEnumerator FileContiniously()
	{
		while (true)
		{
			GameObject instance = Instantiate(projectilePrefab,
				transform.position,
				Quaternion.identity);
			Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
			if (rb != null)
			{
				rb.linearVelocity = transform.up * projectileSpeed;
			}
			Destroy(instance, projectileLifetime);
			float timeToNextProjectile = Random.Range(baseFiringRate - firingRateVariance, baseFiringRate + firingRateVariance);
			timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);
			audioPlayer.PlayShootingClip();
			yield return new WaitForSeconds(timeToNextProjectile);
		}
	}
}
