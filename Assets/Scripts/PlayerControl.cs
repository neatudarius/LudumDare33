using UnityEngine;
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

    void Start () {
        currentSpeed = 0;
        rigid2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        state = "walk_idle";
        anim.SetInteger("state", 0);
	}

    void Update() {
        //make sure it stays in bounds
        if (transform.position.x < leftBound.position.x) {
            transform.position = new Vector3(leftBound.position.x, transform.position.y, transform.position.z);
            currentSpeed = 0.0f;
        }
        else if (transform.position.x > rightBound.position.x) {
            transform.position = new Vector3(rightBound.position.x, transform.position.y, transform.position.z);
            currentSpeed = 0.0f;
        }

        //get input for forward/backward movement
        if (Input.GetAxis("Horizontal") > 0.1) {
            if (isGrounded) {
                state = "walk_forward";
                anim.SetInteger("state", 1);
            }
            currentSpeed = Mathf.SmoothDamp(currentSpeed, forwardSpeed, ref speedDampVelocity, speedSmooth);
        }
        else if (Input.GetAxis("Horizontal") < -0.1) {
            if (isGrounded) {
                state = "walk_backward";
                anim.SetInteger("state", 2);
            }
            currentSpeed = Mathf.SmoothDamp(currentSpeed, -backwardSpeed, ref speedDampVelocity, speedSmooth);
        }
        else {
            if (isGrounded) {
                state = "walk_idle";
                anim.SetInteger("state", 0);
            }
            currentSpeed = Mathf.SmoothDamp(currentSpeed, 0f, ref speedDampVelocity, speedSmooth);
        }

        //jump
        if (isGrounded && (Input.GetButton("Jump") || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) ) {
            state = "jump";
            anim.SetInteger("state", 3);
            rigid2D.velocity = new Vector2(rigid2D.velocity.x, jumpSpeed);
        }

        if (rigid2D.velocity.normalized.y < 0.2 && state == "jump") {
            state = "falling";
            anim.SetInteger("state", 4);
        }
    }

	void FixedUpdate () {
        //apply the force
        rigid2D.velocity = new Vector2(currentSpeed, rigid2D.velocity.y);
        //check if is grounded
        isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    void OnCollisionEnter2D(Collision2D hit) {
        if(hit.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            state = "landed";
            anim.SetInteger("state", 5);
            cameraControl.PositionShake(new Vector3(0, 0.5f, 0), new Vector3(0, 20.0f, 0), 0.5f);
            cameraControl.TiltShake(2.0f, 25.0f, 0.5f);
        }
    }
}
