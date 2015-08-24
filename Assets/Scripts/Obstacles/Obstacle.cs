using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Obstacle : MonoBehaviour {
    bool spin, fall;
    public float rotateSpeed = 100.0f;

    void Start ( ) {
        spin = fall = false;
    }

	public virtual void  OnCollisionEnter2D( Collision2D coll ) {
        if ( GlobalManager.rage.activated ) {
            spin = true;
            StartCoroutine ( WaitUntilFall ( 0.2f ) );
            return;
        }
		if (coll.gameObject.name == "Player") {
            GameObject.Find("_Canvas").GetComponent<MenuController>().GameIsOver();
            GlobalManager.ResetDifficulty();
		}
	}


    public virtual void Update() {
        if ( spin ) {
            gameObject.transform.Rotate ( Vector3.up, Time.deltaTime * rotateSpeed );
            gameObject.transform.Rotate ( Vector3.forward, Time.deltaTime * rotateSpeed );
        }
        if ( fall ) {
            gameObject.transform.position += GlobalManager.backgroundSpeed * Vector3.down * Time.deltaTime;
        }
    }

    IEnumerator WaitUntilFall( float time ) {
        yield return new WaitForSeconds ( time );
        spin = false;
        fall = true;
        StartCoroutine ( WaitUntilDestroy ( 0.2f ) );
    }

    IEnumerator WaitUntilDestroy ( float time) {
        yield return new WaitForSeconds ( time );
        Destroy ( gameObject );
    }
    
}
