using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ObstacleSpawner : MonoBehaviour {

	public List<GameObject> obstaclePrefabList;

	private float spawnTime = 0;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		if (spawnTime <= 0) {
			int randomIndex = GlobalManager.rand ( 0, obstaclePrefabList.Count-1 );
			GameObject newObstacle = Instantiate (obstaclePrefabList[randomIndex]);
			newObstacle.transform.parent = GameObject.Find ("ObstacleHolder").transform;
			newObstacle.transform.position = transform.position;

			spawnTime = GlobalManager.rand ( 2.0f, 3.0f );
		}

		spawnTime = spawnTime - Time.deltaTime;
	}

}
