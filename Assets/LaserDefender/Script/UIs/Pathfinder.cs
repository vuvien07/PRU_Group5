using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
	EnemySpawner enemySpawner;
	 WaveConfigSO waveConfig;
	List<Transform> waypoints;
	int waypointIndex = 0;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		waveConfig = enemySpawner.GetCurrentWave();
		waypoints = waveConfig.GetWaypoints();
		transform.position = waypoints[waypointIndex].position;
	}

	[System.Obsolete]
	void Awake()
	{
		enemySpawner = FindObjectOfType<EnemySpawner>();
	}

	// Update is called once per frame
	void Update()
	{
		FollowPath();
	}

	void FollowPath()
	{
		if (waypointIndex < waypoints.Count)
		{
			Vector3 targetPosition = waypoints[waypointIndex].position;
			float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
			transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);

			if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
			{
				waypointIndex++;
			}
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
