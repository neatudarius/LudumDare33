using UnityEngine;
using System.Collections;

public class RoadSpawner : MonoBehaviour {

	public GameObject roadTilePrefab;
	private float spawnTimer = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (spawnTimer < 0) {
			// Spawn a new road tile
			GameObject newRoadTile = Instantiate (roadTilePrefab);
			newRoadTile.transform.position = transform.position;
			newRoadTile.transform.parent = GameObject.Find ("RoadHolder").transform;
            gameObject.GetComponent<CoffeBeansController> ( ).coffeeParent = newRoadTile.transform;

			// Calculate time until next spawn
			Vector3 pos = newRoadTile.transform.position;
			float leftXvalue = (pos + newRoadTile.GetComponent<SpriteRenderer>().bounds.min).x;
			float rightXvalue = (pos + newRoadTile.GetComponent<SpriteRenderer>().bounds.max).x;
			float distance = rightXvalue - leftXvalue - 0.1f;
			spawnTimer = distance / roadTilePrefab.GetComponent<Scroller>().scrollSpeed;
		}

		spawnTimer -= Time.deltaTime;
	}
}
