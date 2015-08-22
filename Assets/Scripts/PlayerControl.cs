using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    public float forwardSpeed = 5.0f;
    public float backwardSpeed = 10.0f;
    public float jumpSpeed = 15.0f;
    public float speedSmooth = 0.2f;
    public Transform groundCheck;

    private Rigidbody2D rigid2D;
    private Animator anim;

    private float currentSpeed;
    private bool isGrounded;

    private float speedDampVelocity;

    void Start () {
        currentSpeed = 0;
        rigid2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}

    void Update() {
        if (Input.GetAxis("Horizontal") > 0.1) {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, forwardSpeed, ref speedDampVelocity, speedSmooth);
        }
        else if (Input.GetAxis("Horizontal") < -0.1) {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, -backwardSpeed, ref speedDampVelocity, speedSmooth);
        }
        else {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, 0f, ref speedDampVelocity, speedSmooth);
        }

        if (isGrounded && (Input.GetButton("Jump") || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) ) {
            rigid2D.velocity = new Vector2(rigid2D.velocity.x, jumpSpeed);
        }
    }

	void FixedUpdate () {
        //apply the force
        rigid2D.velocity = new Vector2(currentSpeed, rigid2D.velocity.y);
        isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    }
}
