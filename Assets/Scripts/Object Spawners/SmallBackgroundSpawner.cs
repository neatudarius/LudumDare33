using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SmallBackgroundSpawner : MonoBehaviour {

	public List<GameObject> smallBuildingPrefabs;
	public GameObject lastSmallBuilding;
	public GameObject spawnBoundary;
	
	private float spawnTimer = 0;
	private Vector3 cornerOfNewestBuilding;


	// Update is called once per frame
	void Update () {
		if (spawnTimer < 0) {
			// Choose a random building
			int randomIndex = Random.Range(0, smallBuildingPrefabs.Count);
			GameObject randomPrefab = smallBuildingPrefabs[randomIndex];
			
			// Spawn the building and position it
			GameObject newBuilding = Instantiate (randomPrefab);
			float newWidth = newBuilding.GetComponent<SpriteUtility>().GetWidth () - 0.1f;
			float oldWidth = lastSmallBuilding.GetComponent<SpriteUtility>().GetWidth();
			newBuilding.transform.position = lastSmallBuilding.transform.position + new Vector3( (newWidth + oldWidth) / 2, 0, 0 );
			newBuilding.transform.parent = GameObject.Find ("SmallBuildingHolder").transform;
			lastSmallBuilding = newBuilding;
			
			// Calculate time until next spawn
			spawnTimer = newWidth / GlobalManager.backgroundSpeed;
		}
		
		spawnTimer -= Time.deltaTime;
	}
}
