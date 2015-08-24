using UnityEngine;
using System.Collections;

public class PlayerShadow : MonoBehaviour {

    public Transform player;
    public float maxDistance = 5.0f;

    private SpriteRenderer rend;
    private Color c;
    private float dist;
    private float dist_t;

    void Start() {
        rend = GetComponent<SpriteRenderer>();
        c = rend.color;
    }

	void Update () {
        dist = Vector3.Distance(transform.position, player.position);
        if(dist > maxDistance) {
            dist_t = 1;
        }
        else {
            dist_t = dist / maxDistance;
        }

        c.a = Mathf.Lerp(0.65f, 0.1f, dist_t);
        rend.color = c;
	}
}
