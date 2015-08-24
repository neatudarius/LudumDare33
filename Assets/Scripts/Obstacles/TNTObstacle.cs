using UnityEngine;
using System.Collections;

public class TNTObstacle : Obstacle {

    public Transform explosionPrefab;

    public override void OnTriggerEnter2D(Collider2D coll) {
        base.OnTriggerEnter2D(coll);
        if (explosionPrefab != null) {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            transform.GetComponent<Renderer>().enabled = false;
        }
    }
}
