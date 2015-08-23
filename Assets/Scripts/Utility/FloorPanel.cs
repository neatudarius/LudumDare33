using UnityEngine;
using System.Collections;

public class FloorPanel : MonoBehaviour {

	private GameObject floorHolder;

	// Use this for initialization
	void Start () {
		floorHolder = GameObject.Find ("FloorHolder");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDestroy() {
		foreach (Transform child in floorHolder.transform) {
			child.gameObject.GetComponent<SpriteRenderer>().sortingOrder--;
		}
	}
}
