using UnityEngine;
using System.Collections;

public class TNTExplosion : MonoBehaviour {

	void Start() {
        Destroy(gameObject, 2.0f);
    }
	
}
