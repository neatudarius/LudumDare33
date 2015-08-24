using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FloorSpawner : MonoBehaviour {

	public List<GameObject> floorPrefabs;
	public GameObject lastFloor;
	public GameObject spawnBoundary;

	private float floorTileWidth;

	// Use this for initialization
	void Start () {
		floorTileWidth = floorPrefabs [0].GetComponent<SpriteUtility> ().GetWidth ();
	}
	
	// Late update because it doesn't align properly otherwise!
	void LateUpdate () {
	
		// If the last floor piece has reached the spawn boundary, we need to spawn a new one and align it.
		if (lastFloor.GetComponent<SpriteUtility> ().GetRightXValue () <= spawnBoundary.GetComponent<SpawnBoundary> ().GetXValue ()) {
			
			// Spawn a new floor tile randomly from the list
			int index = GlobalManager.rand ( 0, floorPrefabs.Count-1);
			GameObject newFloor = Instantiate (floorPrefabs[index]);
			
			// Align it to the last one
			newFloor.transform.position = transform.position; 			// Align it to the spawner
			float lastFloorRight = lastFloor.GetComponent<SpriteUtility> ().GetRightXValue();
			float newFloorLeft = newFloor.GetComponent<SpriteUtility> ().GetLeftXValue();
			float distance = newFloorLeft - lastFloorRight;				// Get the distance between the last floor and the new floor
			newFloor.transform.Translate ( ( distance + 0.48f * floorTileWidth  ) * Vector3.left );	// Align the new floor tile

			// Put the new floor on top of the old one for correct showing
			int lastFloorLayer = lastFloor.GetComponent<SpriteRenderer>().sortingOrder;
			newFloor.GetComponent<SpriteRenderer>().sortingOrder = lastFloorLayer + 1;

			// Set its parent
			newFloor.transform.parent = GameObject.Find ("FloorHolder").transform;
			
			// Set it as the new last floor piece
			lastFloor = newFloor;
		}

	}
}
