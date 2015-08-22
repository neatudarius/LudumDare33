using UnityEngine;
using System.Collections;

public class Scroller : MonoBehaviour {

	public float scrollSpeed = 0.5f;

	// Use this for initialization
	void Start () {
	
	}

	void OnBecameInvisible() {
		Destroy (gameObject);
	}

	// Update is called once per frame
	void Update () {
		transform.position = transform.position + scrollSpeed * Vector3.left * Time.deltaTime;
	}
}
