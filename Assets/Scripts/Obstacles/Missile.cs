using UnityEngine;
using System.Collections;

public class Missile : Obstacle {

    public Transform explosionPrefab;
    public float speed;

    public override void OnCollisionEnter2D(Collision2D coll) {
        base.OnCollisionEnter2D(coll);
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
