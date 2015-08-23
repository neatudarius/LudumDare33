using UnityEngine;
using System.Collections;

public class ObstacleHole : MonoBehaviour {

	private bool playerDead = false;
	private float deathTimer = 0.5f;

	// Use this for initialization
	void Start () {
		transform.Translate (0.17f * Vector3.down);
	}
	
	// Update is called once per frame
	void Update () {
		if (playerDead) {
			deathTimer -= Time.deltaTime;

			if( deathTimer <= 0 ) {
				GameObject.Find ( "_Canvas" ).GetComponent<MenuController> ( ).isGameOver = true;
				GlobalManager.ResetDifficulty();
			}
		}
	}

	void OnCollisionEnter2D( Collision2D coll ) {
		if (coll.gameObject.name == "Player") {
			playerDead = true;
			coll.gameObject.GetComponent<BoxCollider2D>().enabled = false;
			coll.gameObject.GetComponent<CircleCollider2D>().enabled = false;
			GlobalManager.difficultyMultiplier = 1.0f;
		}
	}
	
}
