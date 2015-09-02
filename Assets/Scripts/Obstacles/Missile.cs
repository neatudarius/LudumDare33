using UnityEngine;
using System.Collections;

public class Missile : Obstacle {

    public Transform explosionPrefab;
    public float speed;

    public override void OnTriggerEnter2D(Collider2D coll) {
        base.OnTriggerEnter2D(coll);
        if ( coll.tag == "wall" ) {
            Destroy ( gameObject );
            return;
        }
        if (explosionPrefab != null) {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            transform.GetComponent<Renderer>().enabled = false;
        }
    }

    public override void Update() {
        base.Update();
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
