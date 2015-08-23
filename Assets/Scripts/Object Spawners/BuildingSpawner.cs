using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingSpawner : MonoBehaviour {

	public List<GameObject> buildingPrefabs;
	public GameObject lastBuilding;
	public GameObject spawnBoundary;
	
	private float spawnTimer = 0;
	private Vector3 cornerOfNewestBuilding;


	// Update is called once per frame
	void Update () {
		if (spawnTimer < 0) {
			// Choose a random building
			int randomIndex = Random.Range(0, buildingPrefabs.Count);
			GameObject randomPrefab = buildingPrefabs[randomIndex];

			// Spawn the building and position it
			GameObject newBuilding = Instantiate (randomPrefab);
			float newWidth = newBuilding.GetComponent<SpriteUtility>().GetWidth () - 0.1f;
			float oldWidth = lastBuilding.GetComponent<SpriteUtility>().GetWidth();
			newBuilding.transform.position = lastBuilding.transform.position + new Vector3( (newWidth + oldWidth) / 2, 0, 0 );
			newBuilding.transform.parent = GameObject.Find ("BuildingHolder").transform;
			lastBuilding = newBuilding;

			// Calculate time until next spawn
			spawnTimer = newWidth / GlobalManager.foregroundSpeed;
		}

		spawnTimer -= Time.deltaTime;
	}
}
