using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	void OnCollisionEnter2D( Collision2D coll ) {
		if (coll.gameObject.name == "Player") {
            GameObject.Find ( "_Canvas" ).GetComponent<MenuController> ( ).isGameOver = true;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
