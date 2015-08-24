using UnityEngine;
using System.Collections;

public class BringToForeground : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Renderer>().sortingLayerName = "Foreground";
	}
	
}
