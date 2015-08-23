using UnityEngine;
using System.Collections;

public class ObstacleTire : MonoBehaviour {

	public float movementSpeed;

    private CircleCollider2D col;
    private float rotationSpeed;
	
	void Start () {
        col = GetComponent<CircleCollider2D>();
        rotationSpeed = movementSpeed * col.radius * col.radius * Mathf.PI * 2;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate ( rotationSpeed * Time.deltaTime * Vector3.forward);
		transform.position += GlobalManager.difficultyMultiplier * movementSpeed * Time.deltaTime * Vector3.left;
	}
}
