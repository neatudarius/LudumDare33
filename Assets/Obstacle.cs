using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	void OnCollisionEnter2D( Collision2D coll ) {
		if (coll.gameObject.name == "Player") {
			Application.LoadLevel ("Menu");
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
