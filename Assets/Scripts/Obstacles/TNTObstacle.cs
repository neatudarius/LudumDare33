using UnityEngine;
using System.Collections;

public class TNTObstacle : Obstacle {

    public Transform explosionPrefab;

    public override void OnCollisionEnter2D(Collision2D coll) {
        base.OnCollisionEnter2D(coll);
        if(explosionPrefab != null)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }
}
