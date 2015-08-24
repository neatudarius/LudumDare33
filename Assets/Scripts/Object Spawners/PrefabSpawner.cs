using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrefabSpawner : MonoBehaviour {

	public List<GameObject> prefabs;
	public GameObject lastPrefab;
	public GameObject spawnBoundary;

	// Late update because it doesn't align properly otherwise!
	void LateUpdate () {
		
		// If the last prefab has reached the spawn boundary, we need to spawn a new one and align it.
		if (lastPrefab.GetComponent<ScenePrefab> ().GetRightXValue () <= spawnBoundary.GetComponent<SpawnBoundary> ().GetXValue ()) {
			
			// Spawn a new prefab randomly from the list
			int index = GlobalManager.rand ( 0, prefabs.Count-1);
			GameObject newPrefab = Instantiate (prefabs[index]);
			
			// Align it to the last one
			newPrefab.transform.position = transform.position; 			// Align it to the spawner
			float lastPrefabRight = lastPrefab.GetComponent<ScenePrefab> ().GetRightXValue();
			float newPrefabLeft = newPrefab.GetComponent<ScenePrefab> ().GetLeftXValue();
			float distance = newPrefabLeft - lastPrefabRight;			// Get the distance between the last prefab and the new prefab
			newPrefab.transform.Translate ( ( distance + 0.21f * newPrefab.GetComponent<ScenePrefab>().GetPrefabWidth() ) * Vector3.left );	// Align the new prefab
			
			// Set its parent
			newPrefab.transform.parent = GameObject.Find ("PrefabHolder").transform;
			
			// Set it as the new last prefab piece
			lastPrefab = newPrefab;
		}
		
	}
}
