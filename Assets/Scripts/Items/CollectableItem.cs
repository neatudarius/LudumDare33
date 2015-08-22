using UnityEngine;
using System.Collections;

public class CollectableItem : MonoBehaviour {
    public GameObject rangePanel;

    public float rotateSpeed = 100.0f;
    private bool effectIsOn = false;
    public float timeUntilDestroy = 0.7f;


    void Start ( ) {
        effectIsOn = false;
    }
    void OnTriggerEnter2D (Collider2D hit ) {
        if ( !effectIsOn ) {
            gameObject.GetComponent<Collider2D> ( ).enabled = false;
            effectIsOn = true;
            GameObject.Find ( "RangePanel" ).GetComponent<RangePanelController> ( ).IncreaseRange ( );
            Destroy ( gameObject, timeUntilDestroy );
        }
    }

    void Update ( ) {
        if ( effectIsOn ) {
            gameObject.transform.Rotate ( Vector3.up, Time.deltaTime * rotateSpeed );
            gameObject.transform.Rotate(    Vector3.forward, Time.deltaTime * rotateSpeed  );
            
        }
    }
}
