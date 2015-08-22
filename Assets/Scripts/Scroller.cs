using UnityEngine;
using System.Collections;

public class Scroller : MonoBehaviour {
	
	public bool isBackgroundItem;

	private GameObject globalManager;

	// Use this for initialization
	void Start () {
		globalManager = GameObject.Find ("_GlobalManager");
	}

	void OnBecameInvisible() {
		if (transform.position.x < 0)
			Destroy (gameObject);
	}

	// Update is called once per frame
	void Update () {
		if (isBackgroundItem) {
			transform.position = transform.position + globalManager.GetComponent<GlobalManager>().backgroundSpeed * Vector3.left * Time.deltaTime;
		} else {
			transform.position = transform.position + globalManager.GetComponent<GlobalManager>().foregroundSpeed * Vector3.left * Time.deltaTime;
		}
	}
}
