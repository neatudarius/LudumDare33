﻿using UnityEngine;
using System.Collections;

public class Scroller : MonoBehaviour {
	
	public bool isBackgroundItem;

	// TO ASK
    /*
	void OnBecameInvisible() {
        Debug.Log ( "inv" );
		if ( transform.position.x < 0 && gameObject.tag == "coin" ) {
        	GameObject.Find ( "RagePanel" ).GetComponent<RagePanelController> ( ).IncreaseRage ( );
        }
	}
    */

	// Update is called once per frame
	void Update () {
		if (transform.position.x < -50) {
			Destroy( gameObject );
		}
		if (isBackgroundItem) {
			transform.position = transform.position + GlobalManager.backgroundSpeed * Vector3.left * Time.deltaTime;
		} else {
			transform.position = transform.position + GlobalManager.difficultyMultiplier * GlobalManager.foregroundSpeed * Vector3.left * Time.deltaTime;
		}
	}
}
