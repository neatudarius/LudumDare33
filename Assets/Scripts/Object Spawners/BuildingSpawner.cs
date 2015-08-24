using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingSpawner : MonoBehaviour {

	public List<GameObject> buildingPrefabs;
	public GameObject lastBuilding;
	public GameObject spawnBoundary;
	
	// Late update because it doesn't align properly otherwise!
	void LateUpdate () {
		
		// If the last building piece has reached the spawn boundary, we need to spawn a new one and align it.
		if (lastBuilding.GetComponent<SpriteUtility> ().GetRightXValue () <= spawnBoundary.GetComponent<SpawnBoundary> ().GetXValue ()) {
			
			// Spawn a new building randomly from the list
			int index = GlobalManager.rand ( 0, buildingPrefabs.Count-1);
			GameObject newBuilding = Instantiate (buildingPrefabs[index]);
			
			// Align it to the last one
			newBuilding.transform.position = transform.position; 			// Align it to the spawner
			float lastBuildingRight = lastBuilding.GetComponent<ScenePrefab> ().GetRightXValue();
			float newBuildingLeft = newBuilding.GetComponent<ScenePrefab> ().GetLeftXValue();
			float distance = newBuildingLeft - lastBuildingRight;			// Get the distance between the last building and the new building
			newBuilding.transform.Translate ( distance * Vector3.left );	// Align the new building
			
			// Set its parent
			newBuilding.transform.parent = GameObject.Find ("BuildingHolder").transform;
			
			// Set it as the new last building piece
			lastBuilding = newBuilding;
		}

	}
}
