using UnityEngine;
using System.Collections;

public class ScenePrefab : MonoBehaviour {

	private GameObject firstRoadPiece;
	private GameObject secondRoadPiece;
	private float prefabWidth;

	// Use this for initialization
	void Awake () {
		firstRoadPiece = transform.Find ("Road_1").gameObject;
		secondRoadPiece = transform.Find ("Road_2").gameObject;
		prefabWidth = secondRoadPiece.GetComponent<SpriteUtility>().GetRightXValue() - firstRoadPiece.GetComponent<SpriteUtility> ().GetLeftXValue();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public float GetRightXValue() {
		return secondRoadPiece.GetComponent<SpriteUtility> ().GetRightXValue ();
	}

	public float GetLeftXValue() {
		return firstRoadPiece.GetComponent<SpriteUtility> ().GetLeftXValue ();
	}

	public float GetPrefabWidth() {
		return prefabWidth;
	}
}
