using UnityEngine;
using System.Collections;

public class SpriteUtility : MonoBehaviour {

	private Vector2 leftCorner;
	private Vector2 rightCorner;
	private float width;

	// Use this for initialization
	void Awake () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	public float GetWidth() {
		leftCorner = GetComponent<SpriteRenderer> ().bounds.min;
		rightCorner = GetComponent<SpriteRenderer> ().bounds.max;
		float width = rightCorner.x - leftCorner.x;
		return width;
	}

	public float GetRightXValue() {
		return GetComponent<SpriteRenderer> ().bounds.max.x;
	}

	public float GetLeftXValue() {
		return GetComponent<SpriteRenderer> ().bounds.min.x;
	}
}
