using UnityEngine;
using System.Collections;

public class TNTExplosion : MonoBehaviour {

    public GameObject explosion;

	void Start() {
        explosion.GetComponent<Renderer>().sortingLayerName = "Foreground";
        Destroy(gameObject, 2.0f);
    }
	
}
