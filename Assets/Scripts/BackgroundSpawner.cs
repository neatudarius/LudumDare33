using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundSpawner : MonoBehaviour {

	public List<GameObject> backgroundTilePrefabs;
	public GameObject rightmostBuilding;
	
	private float spawnTimer = 0;
	private Vector3 cornerOfNewestBuilding;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (spawnTimer < 0) {
			// Choose a random building
			int randomIndex = Random.Range(0, backgroundTilePrefabs.Count);
			GameObject randomPrefab = backgroundTilePrefabs[randomIndex];

			// Spawn the building and position it
			GameObject newBuilding = Instantiate (randomPrefab);
			float newWidth = newBuilding.GetComponent<BackgroundTile>().GetWidth () - 0.1f;
			float oldWidth = rightmostBuilding.GetComponent<BackgroundTile>().GetWidth();
			newBuilding.transform.position = rightmostBuilding.transform.position + new Vector3( (newWidth + oldWidth) / 2, 0, 0 );
			newBuilding.transform.parent = GameObject.Find ("BackgroundHolder").transform;
			rightmostBuilding = newBuilding;

			// Calculate time until next spawn
			spawnTimer = newWidth / randomPrefab.GetComponent<Scroller>().scrollSpeed;
		}

		spawnTimer -= Time.deltaTime;
	}
}
