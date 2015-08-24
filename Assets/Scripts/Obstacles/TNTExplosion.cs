using UnityEngine;
using System.Collections;

public class TNTExplosion : MonoBehaviour {

	void Start() {
        transform.GetComponent<Renderer>().sortingLayerName = "Foreground";
        Destroy(gameObject, 2.0f);
    }
	
}
