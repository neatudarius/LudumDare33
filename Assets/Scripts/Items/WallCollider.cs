using UnityEngine;
using System.Collections;

public class WallCollider : MonoBehaviour {
    void OnTriggerEnter2D ( Collider2D hit ) {
        //Destoy useless coin
        GlobalManager.coins.Remove ( hit.gameObject.name );
        Destroy ( hit.gameObject );  
    }
}
