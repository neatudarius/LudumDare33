using UnityEngine;
using System.Collections;

public class CollectableItem : MonoBehaviour {
    public GameObject ragePanel;

    public float rotateSpeed = 100.0f;
    private bool effectIsOn = false;
    public float timeUntilDestroy = 0.7f;
    public AudioClip collectedSound;

    private Animator anim;
    private AudioSource audioSource;

    void Start ( ) {
        effectIsOn = false;
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        anim.SetBool("effectIsOn", false);
    }
    void OnTriggerEnter2D ( Collider2D hit ) {
        if ( !effectIsOn ) {
            audioSource.PlayOneShot(collectedSound);
            anim.SetBool("effectIsOn", true);
            gameObject.GetComponent<Collider2D> ( ).enabled = false;
            effectIsOn = true;
            GameObject.Find ( "RagePanel" ).GetComponent<RagePanelController> ( ).IncreaseRage ( );
            Destroy ( gameObject, timeUntilDestroy );
        }
    }

    void Update ( ) {
        /*
        if ( effectIsOn ) {
            gameObject.transform.Rotate ( Vector3.up, Time.deltaTime * rotateSpeed );
            gameObject.transform.Rotate ( Vector3.forward, Time.deltaTime * rotateSpeed );
        }
        */
    }
}
