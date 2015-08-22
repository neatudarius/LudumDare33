using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour {

	public List<GameObject> obstaclePrefabList;

	private float spawnTime = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (spawnTime <= 0) {
			int randomIndex = Random.Range ( 0, obstaclePrefabList.Count );
			GameObject newObstacle = Instantiate (obstaclePrefabList[randomIndex]);
			newObstacle.transform.parent = GameObject.Find ("ObstacleHolder").transform;
			newObstacle.transform.position = transform.position;

			spawnTime = Random.Range( 2.0f, 3.0f );
		}

		spawnTime = spawnTime - Time.deltaTime;
	}
}
