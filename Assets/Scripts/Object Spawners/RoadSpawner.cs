using UnityEngine;
using System.Collections;

public class RoadSpawner : MonoBehaviour {

	public GameObject roadPrefab;
	public GameObject lastRoad;
	public GameObject spawnBoundary;

	// Late update because it doesn't align properly otherwise!
	void LateUpdate () {

		// If the last road piece has reached the spawn boundary, we need to spawn a new one and align it.
		if (lastRoad.GetComponent<SpriteUtility> ().GetRightXValue () <= spawnBoundary.GetComponent<SpawnBoundary> ().GetXValue ()) {

			// Spawn a new road tile
			GameObject newRoad = Instantiate (roadPrefab);

			// Align it to the last one
			newRoad.transform.position = transform.position; 			// Align it to the spawner
			float lastRoadRight = lastRoad.GetComponent<SpriteUtility> ().GetRightXValue();
			float newRoadLeft = newRoad.GetComponent<SpriteUtility> ().GetLeftXValue();
			float distance = newRoadLeft - lastRoadRight;				// Get the distance between the last road and the new road
			newRoad.transform.Translate ( distance * Vector3.left );	// Align the new road tile

			// Set its parent
			newRoad.transform.parent = GameObject.Find ("RoadHolder").transform;

			// Set it as the new last road piece
			lastRoad = newRoad;
		}

	}
}
