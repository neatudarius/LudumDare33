using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {
    public GameObject ragePanel;

    public float rotateSpeed = 100.0f;
    private bool effectIsOn = false;
    public float timeUntilDestroy = 0.7f;
    public AudioClip collectedSound;

    private Animator anim;
    private AudioSource audioSource;


    CoinsController parent;
    void Start ( ) {
        effectIsOn = false;
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        anim.SetBool("effectIsOn", false);
    }

    void OnTriggerEnter2D ( Collider2D hit ) {
        if ( !effectIsOn ) {
            //Animation
            audioSource.PlayOneShot(collectedSound);
            anim.SetBool("effectIsOn", true);
            gameObject.GetComponent<Collider2D> ( ).enabled = false;
            effectIsOn = true;

            //Rangeincrease
            GlobalManager.IncreaseRage ( );
            parent = transform.parent.parent.gameObject.GetComponent<CoinsController> ( );

            // Selfdestroy
            parent.Remove ( transform.parent.gameObject.name );
            Destroy ( transform.parent.gameObject, timeUntilDestroy );
        }
    }


}
