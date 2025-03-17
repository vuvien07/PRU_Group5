using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
	[SerializeField] List<GameObject> powerUpPrefabs;
	[SerializeField] bool isLooping;
	[SerializeField] float timeToNextPowerUp;
	[SerializeField] float speed;
	[SerializeField] float powerUpLifetime;

	public float minX = -4f, maxX = 4f;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		StartCoroutine(SpawnPowerup());
	}

	IEnumerator SpawnPowerup()
	{
		do
		{
			float randomX = Random.Range(minX, maxX);
			float spawnY = Camera.main.orthographicSize + 1f;
			Vector2 spawnPosition = new Vector2(randomX, spawnY);

			GameObject instance = Instantiate(GetPowerupPrefabByRandom(powerUpPrefabs),
											 spawnPosition,
											 Quaternion.identity);

			Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
			if (rb != null)
			{
				rb.linearVelocity = Vector2.down * speed; // Di chuyển thẳng xuống
			}

			Destroy(instance, powerUpLifetime);
			yield return new WaitForSeconds(timeToNextPowerUp);
		} while (isLooping);
	}


	GameObject GetPowerupPrefabByRandom(List<GameObject> powerUps)
	{
		int randomValue = Random.Range(0, powerUps.Count);
		return powerUps[randomValue];
	}

}
