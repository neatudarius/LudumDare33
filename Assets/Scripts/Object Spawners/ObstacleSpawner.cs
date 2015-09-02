using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ObstacleSpawner : MonoBehaviour {

	public List<GameObject> obstaclePrefabList;
    public Transform obstacleHolder;
    private float spawnTime = 0;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		if (spawnTime <= 0) {
			int randomIndex = GlobalManager.rand ( 0, obstaclePrefabList.Count-1 );
			GameObject newObstacle = Instantiate (obstaclePrefabList[randomIndex]);
			newObstacle.transform.parent = obstacleHolder;
            newObstacle.transform.position = transform.position;
            if ( newObstacle.tag == "trash" )
                newObstacle.transform.localPosition = new Vector3( transform.position.x, 0.25f, transform.position.z );
			spawnTime = GlobalManager.rand ( 2.0f, 3.0f );
		}

		spawnTime = spawnTime - Time.deltaTime;
	}

}
