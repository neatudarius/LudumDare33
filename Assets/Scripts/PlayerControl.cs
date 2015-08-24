using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    public CameraControl cameraControl;
    public Transform leftBound;
    public Transform rightBound;
    public float forwardSpeed = 5.0f;
    public float backwardSpeed = 10.0f;
    public float jumpSpeed = 15.0f;
    public float speedSmooth = 0.2f;
    public Transform groundCheck;

    public string state;

    private Rigidbody2D rigid2D;
    private Animator anim;

    private float currentSpeed;
    private bool isGrounded;
    private float speedDampVelocity;


    public float distance = 0f;
    public Text distanceObj;

    public AudioClip groundShake;
    private AudioSource audioSource;

    void Start ( ) {
        currentSpeed = 0;
        isGrounded = true;
        distance = 0f;
        rigid2D = GetComponent<Rigidbody2D> ( );
        anim = GetComponent<Animator> ( );
        audioSource = GetComponent<AudioSource>();
        state = "walk_idle";
        anim.SetInteger ( "state", 0 );
    }

    void Update() {
        //check if is grounded
        isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        distance += GlobalManager.foregroundSpeed * Time.deltaTime;
        distanceObj.text = ( ( int ) distance ).ToString ( );

        //make sure it stays in bounds
        if ( transform.position.x < leftBound.position.x ) {
            transform.position = new Vector3 ( leftBound.position.x, transform.position.y, transform.position.z );
            currentSpeed = 0.0f;
        } else if ( transform.position.x > rightBound.position.x ) {
            transform.position = new Vector3 ( rightBound.position.x, transform.position.y, transform.position.z );
            currentSpeed = 0.0f;
        }

        if (isGrounded && state == "landed") {
            state = "walk_idle";
            anim.SetInteger("state", 0);
        }

        //jump
        if (isGrounded && (Input.GetButton("Jump") || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))) {
            state = "jump";
            anim.SetInteger("state", 3);
            rigid2D.velocity = new Vector2(rigid2D.velocity.x, jumpSpeed);
            GlobalManager.backgroundSpeed = GlobalManager.backgroundSpeed_Jumping;
            GlobalManager.foregroundSpeed = GlobalManager.foregroundSpeed_Jumping;
        }

        //get input for forward/backward movement
        if ( Input.GetAxis ( "Horizontal" ) > 0.1 ) {
            if ( isGrounded && state != "jump" ) {
                state = "walk_forward";
                anim.SetInteger ( "state", 1 );
            }
            GlobalManager.backgroundSpeed = GlobalManager.backgroundSpeed_Accelerated;
            GlobalManager.foregroundSpeed = GlobalManager.foregroundSpeed_Accelerated;
            //currentSpeed = 
        } else if ( Input.GetAxis ( "Horizontal" ) < -0.1 ) {
            if (isGrounded && state != "jump") {
                state = "walk_backward";
                anim.SetInteger ( "state", 2 );
            }
            GlobalManager.backgroundSpeed = GlobalManager.backgroundSpeed_Normal;
            GlobalManager.foregroundSpeed = GlobalManager.foregroundSpeed_Normal;
            //currentSpeed = Mathf.SmoothDamp(currentSpeed, -backwardSpeed, ref speedDampVelocity, speedSmooth);
        } else {
            if ( isGrounded && state != "jump") {
                state = "walk_idle";
                anim.SetInteger ( "state", 0 );
            }
            GlobalManager.backgroundSpeed = GlobalManager.backgroundSpeed_Normal;
            GlobalManager.foregroundSpeed = GlobalManager.foregroundSpeed_Normal;
            //currentSpeed = Mathf.SmoothDamp(currentSpeed, 0f, ref speedDampVelocity, speedSmooth);
        }

        if ( rigid2D.velocity.normalized.y < 0.2 && state == "jump" ) {
            state = "falling";
            anim.SetInteger ( "state", 4 );
            GlobalManager.backgroundSpeed = GlobalManager.backgroundSpeed_Accelerated;
            GlobalManager.foregroundSpeed = GlobalManager.foregroundSpeed_Accelerated;
        }
    }

    void FixedUpdate ( ) {
        //apply the force
        rigid2D.velocity = new Vector2 ( currentSpeed, rigid2D.velocity.y );
    }

    void OnCollisionEnter2D ( Collision2D hit ) {
        if ( hit.gameObject.layer == LayerMask.NameToLayer ( "Ground" ) ) {
            state = "landed";
            anim.SetInteger ( "state", 2 );
            cameraControl.PositionShake ( new Vector3 ( 0, 0.5f, 0 ), new Vector3 ( 0, 20.0f, 0 ), 0.5f );
            cameraControl.TiltShake ( 2.0f, 25.0f, 0.5f );
            audioSource.PlayOneShot(groundShake);
        }
    }
}
