using UnityEngine;
using System.Collections;

public class Gizmo : MonoBehaviour 
{
    public Color gizmoColor = Color.red;
    public float gizmoSize = 0.5f;

	void OnDrawGizmos() 
    {
		Gizmos.color = gizmoColor;
		Gizmos.DrawWireSphere(transform.position, gizmoSize);
	}
}