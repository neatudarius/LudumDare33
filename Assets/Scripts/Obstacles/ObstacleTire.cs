using UnityEngine;
using System.Collections;

public class ObstacleTire : MonoBehaviour {

	public float rotationSpeed;
	public float movementSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate ( rotationSpeed * Time.deltaTime * Vector3.forward);
		transform.position += GlobalManager.difficultyMultiplier * movementSpeed * Time.deltaTime * Vector3.left;
	}
}
