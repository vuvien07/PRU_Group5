using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.LaserDefender.Script.DynamicObject
{
    public class ObstacleSpawner : MonoBehaviour
    {
		[SerializeField] List<GameObject> obstaclePrefabs;
		[SerializeField] bool isLooping;
		[SerializeField] float timeToNextObstacle;
		[SerializeField] float speed;
		[SerializeField] float obstacleLifetime;

		public float minX = -4f, maxX = 4f;
		public float minAngle = -30f, maxAngle = 30f; // Góc lệch ngẫu nhiên
		public float sideSpeed = 2f; // Tốc độ di chuyển ngang
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

				GameObject instance = Instantiate(GetPowerupPrefabByRandom(obstaclePrefabs),
												 spawnPosition,
												 Quaternion.identity);

				Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
				if (rb != null)
				{
					// Chọn hướng ngẫu nhiên: thẳng, chéo trái hoặc chéo phải
					float angle = Random.Range(minAngle, maxAngle);
					Vector2 moveDirection = Quaternion.Euler(0, 0, angle) * Vector2.down;
					rb.linearVelocity = moveDirection * speed + Vector2.right * Random.Range(-sideSpeed, sideSpeed);
					rb.angularVelocity = 200f;
				}

				Destroy(instance, obstacleLifetime);
				yield return new WaitForSeconds(timeToNextObstacle);
			} while (isLooping);
		}


		GameObject GetPowerupPrefabByRandom(List<GameObject> powerUps)
		{
			int randomValue = Random.Range(0, powerUps.Count);
			return powerUps[randomValue];
		}
	}
}
