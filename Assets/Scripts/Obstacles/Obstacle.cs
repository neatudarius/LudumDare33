using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Obstacle : MonoBehaviour {
    bool spin, fall;
    public float rotateSpeed = 100.0f;
    MenuController menu;
    void Start ( ) {
        spin = fall = false;
        menu = GameObject.FindObjectOfType<MenuController> ( );
    }

    public virtual void OnTriggerEnter2D ( Collider2D coll ) {
        if ( GlobalManager.rage && coll.gameObject.tag == "Player") {
            GlobalManager.rage.value = 0;
        }

        if ( GlobalManager.rage && GlobalManager.rage.activated ) {
            spin = true;
            
            gameObject.GetComponent<Scroller> ( ).enabled = false;
            StartCoroutine ( WaitUntilFall ( 0.2f ) );

            SpriteRenderer sr = GetComponent<SpriteRenderer> ( );
            Color c = sr.color;
            c.a = 0.75f;
            sr.color = c;
            return;
        }
		if (coll.gameObject.name == "Player") {
            GetComponent<Collider2D>().enabled = false;
            coll.gameObject.GetComponent<PlayerControl>().Die();
            menu.GameIsOver();
            GlobalManager.FreezeSpeed ( );
            
		}
	}


    public virtual void Update() {
        if ( spin ) {
            transform.Rotate ( Vector3.up, Time.deltaTime * rotateSpeed );
            transform.Rotate ( Vector3.forward, Time.deltaTime * rotateSpeed );
            transform.Translate ( 10*Vector2.right * GlobalManager.backgroundSpeed * Time.deltaTime);
            transform.Translate ( 10 * Vector2.up * GlobalManager.backgroundSpeed * Time.deltaTime );
        }
        if ( fall ) {
            transform.Translate ( -75*Vector2.up * GlobalManager.backgroundSpeed * Time.deltaTime );
        }
    }

    IEnumerator WaitUntilFall( float time ) {
        yield return new WaitForSeconds ( time );
        spin = false;
        fall = true;
        Destroy ( gameObject, 5f );
    }

    
}
