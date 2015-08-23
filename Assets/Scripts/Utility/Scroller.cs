using UnityEngine;
using System.Collections;

public class Scroller : MonoBehaviour {
	
	public bool isBackgroundItem;

	

	void OnBecameInvisible() {
        if ( transform.position.x < 0 ) {
            if (gameObject.tag == "bean")
                GameObject.Find ( "RagePanel" ).GetComponent<RagePanelController> ( ).IncreaseRage ( );
            Destroy ( gameObject );
        }
	}

	// Update is called once per frame
	void Update () {
		if (isBackgroundItem) {
			transform.position = transform.position + GlobalManager.backgroundSpeed * Vector3.left * Time.deltaTime;
		} else {
			transform.position = transform.position + GlobalManager.difficultyMultiplier * GlobalManager.foregroundSpeed * Vector3.left * Time.deltaTime;
		}
	}
}
