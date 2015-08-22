using UnityEngine;
using System.Collections;

public class BackgroundTile : MonoBehaviour {

	private Vector2 leftCorner;
	private Vector2 rightCorner;
	private float width;

	// Use this for initialization
	void Awake () {
		leftCorner = GetComponent<SpriteRenderer> ().bounds.min;
		rightCorner = GetComponent<SpriteRenderer> ().bounds.max;
		width = rightCorner.x - leftCorner.x;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public float GetWidth() {
		return width;
	}

	public Vector2 GetCorner() {
		return rightCorner;
	}
}
